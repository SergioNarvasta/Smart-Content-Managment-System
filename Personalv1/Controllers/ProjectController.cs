﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personal.Interfaces;
using Personal.Models;
using Personal.Repositories;

namespace Personal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectCollection _serviceProject = new ProjectCollection();

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            return Ok(await _serviceProject.GetAllProjects());
        }
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] Project project)
        {   
            if (project == null)
                return BadRequest();

            if (project.Name == string.Empty)
            {
                ModelState.AddModelError("Name", "El proyecto no debe ser vacio");
            }
            project.RutaArchivo = await GuardarArchivo(project.Archivo);
            project.FechaRegistro = DateTime.Now;

            await _serviceProject.InsertProject(project);
            return Created("Created", true);
        }
        public async Task<string> GuardarArchivo(IFormFile Archivo) {
            var ruta = String.Empty;
            string extension = ".jpg";
            if (Archivo.Length>0)
            {
                var nombreArchivo= Guid.NewGuid().ToString()+ extension;
                ruta = $"Files/{nombreArchivo}";
                using (var stream = new FileStream(ruta,FileMode.Create))
                {
                    await Archivo.CopyToAsync(stream);
                }
            }
            return ruta;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> CreateProject([FromBody] Project project,string id)
        {
            if (project == null)
                return BadRequest();

            if (project.Name == string.Empty)
            {
                ModelState.AddModelError("Name", "El proyecto no debe ser vacio");
            }
            project.Id = new MongoDB.Bson.ObjectId(id);
            await _serviceProject.UpdateProject(project);
            return Created("Update", true);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProject(string id) 
        {
            await _serviceProject.DeleteProject(id);
            return NoContent();
        }
    }
}
