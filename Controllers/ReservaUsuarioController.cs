using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_APP.Models;
using System.Diagnostics.Contracts;
using System.Security.Claims;

namespace ProyectoFinal_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaUsuarioController : ControllerBase
    {
        public readonly ProjectappContext _dbcontex;

        public ReservaUsuarioController(ProjectappContext _contex)
        {
            _dbcontex = _contex;
        }

        [HttpGet]
        [Route("ListaReservas")]
        [Authorize]
        public IActionResult ListaReserva()
        {
            List<Reserva> listars = new List<Reserva>();
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var Valid = Funcion.ValidarToken(identity);
            try
            {
                listars = _dbcontex.Reservas.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = listars });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No se encontraron Datos" });
            }
            
        }

        [HttpPost]
        [Route("GuardarClase")]
        public IActionResult Guardar([FromBody] Reserva objUsuario)
        {

            try
            {
                _dbcontex.Reservas.Add(objUsuario);
                var result = _dbcontex.SaveChanges();
                if (result > 0)
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = "RESERVA REALIZADA EXITOSAMENTE" });
                else
                    return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "NO es posible reservar" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "NO es posible reservar" });

            }
        }

        [HttpPut]
        [Route("ModificarReserva")]
        public IActionResult Modificar([FromBody] Reserva objUsuario)
        {
            Reserva _Clase = _dbcontex.Reservas.Find(objUsuario.IdUser);

            if (_Clase == null)
            {
                return NotFound(new { mensaje = "Reserva NO encontrada" });
            }

            try
            {
                _Clase.Nombre = string.IsNullOrEmpty(objUsuario.Nombre) ? _Clase.Nombre : objUsuario.Nombre;
                _Clase.Identificacion = objUsuario.Identificacion is null ? _Clase.Identificacion : objUsuario.Identificacion;
                _Clase.Reserva1 = objUsuario.Reserva1 is null ? _Clase.Reserva1 : objUsuario.Reserva1;
                
                _dbcontex.Reservas.Update(_Clase);
                var result = _dbcontex.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = "RESERVA MODIFICADA EXITOSAMENTE" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "NO es posible realizar cambios" });
            }
        }

        [HttpDelete]
        [Route("EliminarReserva")]
        public IActionResult Eliminar(int id)
        {
            Reserva ObjUsuario = _dbcontex.Reservas.Find(id);

            if (ObjUsuario == null)
            {
                return NotFound(new { mensaje = "Reserva NO encontrada" });
            }

            try
            {
                _dbcontex.Reservas.Remove(ObjUsuario);
                _dbcontex.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = "RESERVA ELIMINADA CORRECTAMENTE" });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No es posible Eliminar la reserva" });

            }
        }
    }
}
