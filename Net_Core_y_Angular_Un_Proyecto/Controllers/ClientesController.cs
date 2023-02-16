using Microsoft.AspNetCore.Mvc;
using Net_Core_y_Angular_Un_Proyecto.Modelos;

namespace Net_Core_y_Angular_Un_Proyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        /// <summary>
        /// Metodo para obtener de BBDD los clientes
        /// </summary>
        /// <returns>listado de clientes</returns>
        [HttpGet]
        public IActionResult DameClientes()
        {
            using ( CursoAngularNetCoreContext baseDatos = new CursoAngularNetCoreContext())
            {
                var listaClientes = baseDatos.Clientes.ToList();
                //Nos parse la lista a un objeto json
                return Ok(listaClientes);
            }
        }
    }
}
