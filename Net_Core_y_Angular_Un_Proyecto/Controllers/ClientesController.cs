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
        private readonly IConfiguration _configuration;
        public ClientesController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

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

        /// <summary>
        /// Crea un cliente 
        /// </summary>
        /// <param name="clienteViewmodel">modelo cliente</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AgregarCliente(ClienteViewmodel clienteViewmodel)
        {
            Resultado res = new Resultado();
            try
            {
                byte[] keyBbyte = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                Utils util = new Utils(keyBbyte);
                using (CursoAngularNetCoreContext basedatos = new CursoAngularNetCoreContext())
                {
                    //Creamos un objeto cliente
                    Cliente cliente = new Cliente();
                    cliente.Nombre = clienteViewmodel.nombre;
                    cliente.Email = clienteViewmodel.email;
                    cliente.Password = Encoding.ASCII.GetBytes(util.cifrar(clienteViewmodel.pass, _configuration["ClaveCifrado"]));
                    cliente.FechaAlta = DateTime.Now;
                    //Añadimos a base de datos el cliente creado
                    basedatos.Clientes.Add(cliente);
                    //Confirmamos cambios
                    basedatos.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                res.Error = "Se produjo un error al hacer el alta del cliente " + ex.Message;
            }
            return Ok(res);
        }

        /// <summary>
        /// Edita un cliente
        /// </summary>
        /// <param name="clienteViewmodel">modelo cliente</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult EditarCliente(ClienteViewmodel clienteViewmodel)
        {
            Resultado res = new Resultado();
            try
            {
                byte[] keyBbyte = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                Utils util = new Utils(keyBbyte);
                using (CursoAngularNetCoreContext basedatos = new CursoAngularNetCoreContext())
                {
                    //Obtenemos el cliente por su email
                    Cliente cliente = basedatos.Clientes.Single(x => x.Email == clienteViewmodel.email);
                    cliente.Nombre = clienteViewmodel.nombre;
                    cliente.Password = Encoding.ASCII.GetBytes(util.cifrar(clienteViewmodel.pass, _configuration["ClaveCifrado"]));
                    basedatos.Entry(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    //Confirmamos cambios
                    basedatos.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                res.Error = "Se produjo un error al modificar un cliente" + ex.Message;
            }
            return Ok(res);
        }
    }


}
