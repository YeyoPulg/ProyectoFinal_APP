using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProyectoFinal_APP.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProyectoFinal_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public readonly ProjectappContext _dbcontex;
        public IConfiguration _Configuration;

        public UsuarioController(ProjectappContext _contex, IConfiguration _config)
        {
            _dbcontex = _contex;
            _Configuration = _config;
        }

        [HttpPost]
        [Route("Session")]
        public IActionResult Session([FromBody] Usuario objeto)
        {
            try
            {
                Usuario objUsr = _dbcontex.Usuarios.Where(u => u.Usuario1 == objeto.Usuario1 && u.Pass == objeto.Pass).FirstOrDefault();
                if (objUsr == null)
                {
                    return NotFound("Credenciales Invalidas");
                }

                var jwt = _Configuration.GetSection("Jwt").Get<Jwt>();
                var Claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub , jwt.Subject),
                    new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat , DateTime.UtcNow.ToString()),
                    new Claim("Usuario1", objeto.Usuario1),
                    new Claim("Id" , objUsr.Id.ToString())
                };

                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                SigningCredentials singCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                JwtSecurityToken jwtoken = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    Claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: singCred
                    );

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", token = new JwtSecurityTokenHandler().WriteToken(jwtoken) });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, new { mensaje = ex.Message });
            }
        }
    }
}
