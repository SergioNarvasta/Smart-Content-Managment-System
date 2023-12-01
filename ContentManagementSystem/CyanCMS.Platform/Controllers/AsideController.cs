﻿
using CyanCMS.Application.Interfaces;
using CyanCMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsideController : ControllerBase
    {
        private readonly IAsideAppService _asideAppService;

        public AsideController(IAsideAppService asideAppService) 
        {
            _asideAppService = asideAppService;
        }

        [Route("Aside/GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _asideAppService.GetAll());
        }

        [Route("Aside/Create")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Aside model)
        {
            if (model == null)
                return BadRequest();

            model.Aside_Estado = 1;
            model.Audit_FecCre = DateTime.Now.ToString("dd/MM/yyyy");
            model.Aside_Pk = Guid.NewGuid().ToString();

            await _asideAppService.Insert(model);
            return Created("Created", true);
        }

		[Route("Aside/Update")]
		[HttpPut("{id}")]
		public async Task<IActionResult> Update([FromBody] Aside model, string id)
		{
			if (model == null)
				return BadRequest();

			model.Aside_Id = new MongoDB.Bson.ObjectId(id);
            await _asideAppService.Update(model);
			return Created("Update", true);
		}

		[Route("Aside/Delete")]
		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] string id)
		{
			await _asideAppService.Delete(id);
			return NoContent();
		}
	}
}
