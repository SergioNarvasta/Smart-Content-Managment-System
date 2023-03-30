import { useEffect, useState } from "react";
import { Table,  } from "reactstrap";
import './Style.css';

const ContentMainListado = () => {
  const [modelList, setmodelList] = useState([]);

  const Listar = async () => {
    const response = await fetch("/api/contentmain/listatodos");
    if (response.ok) {
      const data = await response.json();
      setmodelList(data);      
    } else {
        console.log("Error : (api/contentmain/listatodos)")
    }
    }
  useEffect(() => {
    Listar()
  }, [])

 return (
 <div>
    <Table responsive className="table-bordered table-hover">
        <thead className="table-warning">
                <tr>
                    <th>Titulo</th>
                    <th>Descripcion</th>
                    <th>Contenido</th>
                    <th>Estado</th>    
                    <th>Orden</th>             
                    <th>Imagen</th>                  
                    <th>Usuario Creacion</th>
                    <th>Fecha Creacion</th>
                    <th>Usuario Actualiza</th>  
                    <th>Fecha Actualiza</th>    
                    <th></th>
                </tr>
            </thead>
        <tbody >
          {
           (modelList.length < 1) ?(
               <tr>
                   <td colSpan="8">Sin registros</td>
               </tr>
           ):(
            modelList.map((item) => (
             <tr key={item.contentMain_Pk} >
                 
                 <td>{item.contentMain_Titulo}</td>
                 <td>{item.contentMain_Descripcion}</td>
                 <td>{item.contentMain_Contenido}</td>
                 <td>{item.contentMain_Estado = 1 ? "Activo" : "No disponible"}</td>                    
                 <td>{item.contentMain_Orden}</td>                         
                 <td><img src={"data:image/jpg;base64," + item.file_Base64} /></td>                
                 <td>{item.audit_UsuCre}</td>
                 <td>{item.audit_FecCre}</td>
                 <td>{item.audit_UsuAct}</td>
                 <td>{item.audit_FecAct}</td>
                     
               </tr>
            ))
           )
          }
        </tbody>
    </Table> 

  </div>
  )
}
export default ContentMainListado;