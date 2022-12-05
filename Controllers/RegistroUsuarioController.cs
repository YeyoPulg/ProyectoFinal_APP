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
    public class RegistroUsuarioController : ControllerBase
    {
        public readonly ProjectappContext _dbcontex;

        public RegistroUsuarioController(ProjectappContext _contex)
        {
            _dbcontex = _contex;
        }

        [HttpGet]
        [Route("ListaRegistros")]
        [Authorize]
        public IActionResult ListaRegistro()
        {
            List<Registro> listarg = new List<Registro>();
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var Valid = Funcion.ValidarToken(identity);

            try
            {
                listarg = _dbcontex.Registros.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = listarg });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "NO SE ENCONTRARON DATOS" });
            }
        }

        [HttpGet]
        [Route("DetalleRegistro")]
        [Authorize]
        public IActionResult DetalleRegistro(int id)
        {
            Registro ObjUsuario = _dbcontex.Registros.Find(id);
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var Valid = Funcion.ValidarToken(identity);

            if (ObjUsuario == null)
            {
                return NotFound(new { mensaje = "NO EXISTE DATOS" });
            }

            try
            {
                ObjUsuario = _dbcontex.Registros.Where(x => x.IdUsuario == id).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = ObjUsuario });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "NO SE ENCONTRARON DATOS" });

            }
        }

        [HttpPost]
        [Route("GuardarUsuario")]

        public IActionResult Guardar([FromBody] Registro objUsuario)
        {

            try
            {
                _dbcontex.Registros.Add(objUsuario);
                var result = _dbcontex.SaveChanges();
                if (result > 0)
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = "USUARIO REGISTRADO EXITOSAMENTE" });
                else
                    return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "NO es posible registrarse" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "NO es posible registrarse" });

            }
        }
    }
}
