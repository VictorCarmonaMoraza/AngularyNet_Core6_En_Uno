using Microsoft.AspNetCore.Mvc;
using Net_Core_y_Angular_Un_Proyecto.Modelos;
using Net_Core_y_Angular_Un_Proyecto.Modelos.ViewModels;
using System.Text;

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
            Resultado res = new Resultado();
            try
            {
                using (CursoAngularNetCoreContext baseDatos = new CursoAngularNetCoreContext())
                {
                    //Obtenemos de base de datos el listado de clientes
                    var listaClientes = baseDatos.Clientes.ToList();
                    //Nos parsea la lista a un objeto json
                    res.ObjetoGenerico = listaClientes;
                }
            }
            catch (Exception ex)
            {
                res.Error = "Se produjo un error al obtener las clientes " + ex.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        public IActionResult AgregarCliente(ClienteViewmodel clienteViewmodel)
        {
            Resultado res = new Resultado();
            try
            {
                byte[] keyBbyte = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                Utils util= new Utils(keyBbyte);
                using (CursoAngularNetCoreContext basedatos = new CursoAngularNetCoreContext())
                {
                   //Creamos un objeto cliente
                   Cliente cliente= new Cliente();
                    cliente.Nombre = clienteViewmodel.nombre;
                    cliente.Email= clienteViewmodel.email;
                    cliente.Password = Encoding.ASCII.GetBytes(util.cifrar(clienteViewmodel.pass,"ClaveSecreta"));
                    cliente.FechaAlta = DateTime.Now;
                    //Añadimos a base de datos el cliente creado
                    basedatos.Clientes.Add(cliente);
                    //Confirmamos cambios
                    basedatos.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                res.Error = "Se produjo un error al hacer el lata del clinete " + ex.Message;
            }
            return Ok(res);

        }
    }
}
