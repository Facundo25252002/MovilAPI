using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovilAPI.Models.Data;
using MovilAPI.Models.DTO;
using MovilAPI.Models.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace MovilAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuariosRepository _usuariosRepository;

        public UsuariosController(IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }


        [HttpGet("usuarios")]
        [ActionName(nameof(GetUsuariosAsync))]
        public IEnumerable<Usuarios> GetUsuariosAsync()
        {
            return _usuariosRepository.GetUsuarios();
        }


        [HttpGet("{id}")]
        [ActionName(nameof(GetUsuariosById))]
        public ActionResult<Usuarios> GetUsuariosById(int id)
        {
            var usuariosByID = _usuariosRepository.GetUsuariosById(id);
            if (usuariosByID == null)
            {
                return NotFound();
            }
            return Ok(usuariosByID);
        }



        [HttpPost("crearUsuarios")]
        [ActionName(nameof(CreateUsuariosAsync))]
        public async Task<ActionResult<Usuarios>> CreateUsuariosAsync(Usuarios usuarios)
        {
            if (usuarios == null) {


                return BadRequest();
            }

            await _usuariosRepository.CreateUsuariosAsync(usuarios);
            return CreatedAtAction(nameof(GetUsuariosById), new { id = usuarios.id }, usuarios);
        }




        [HttpPut("{id}")]
        [ActionName(nameof(UpdateUsuarios))]
        public async Task<ActionResult> UpdateUsuarios(int id, Usuarios usuarios)
        {
            if (id != usuarios.id)
            {
                return BadRequest();
            }

            var updated = await _usuariosRepository.UpdateUsuariosAsync(usuarios);
            if (!updated)
            {
                return NotFound(); // controlar error 404 Not Found
            }



            return NoContent();
        }

        [HttpDelete("{id}")]
        [ActionName(nameof(DeleteUsuarios))]
        public async Task<IActionResult> DeleteUsuarios(int id)
        {
            var usuarios = _usuariosRepository.GetUsuariosById(id);
            if (usuarios == null)
            {
                return NotFound();
            }
            var deleted = await _usuariosRepository.DeleteUsuariosAsync(usuarios);
            if (!deleted)
            {
                return StatusCode(500); // Controlar error 500 Internal Server Error
            }



            return NoContent();
        }

   

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LoginAsync(LoginRequest login)
        {

            try
            {
                // Obtener la lista de usuarios de manera asincrónica
                var usuariosList = await _usuariosRepository.GetUsuariosAsync();

                // Realizar la búsqueda
                var userEncontrado =  usuariosList
                    .FirstOrDefault(x => x.usuario == login.Usuario && x.password == login.Password);

                if (userEncontrado == null)
                {
                    return StatusCode(401); 
                }
                else
                {
                    // Generar el token
                    string token = CreateAccessToken(userEncontrado);

                    return Ok(new LoginResponse { Token = token });
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que pueda ocurrir durante la operación asincrónica
                Debug.WriteLine(ex.Message);
                return StatusCode(500); // Error interno del servidor
            }
        }



        string CreateAccessToken(Usuarios user)
        {
            // suppose a public key can read from appsettings
            string K = "12345678901234567890123456789012345678901234567890123456789012345678901234567890";
            // convert to bytes
            var key = Encoding.UTF8.GetBytes(K);
            // convert to symetric Security
            var skey = new SymmetricSecurityKey(key);
            // Sign de Key
            var SignedCredential = new SigningCredentials(skey, SecurityAlgorithms.HmacSha256Signature);
            // Add Claims
            var uClaims = new ClaimsIdentity(new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub,user.nombre),
                    new Claim(JwtRegisteredClaimNames.Email,user.email)
                });
            // define expiration
            var expires = DateTime.UtcNow.AddDays(1);
            // create de token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = uClaims,
                Expires = expires,
                Issuer = "ITES",
                SigningCredentials = SignedCredential,
            };
            //initiate the token handler
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenJwt = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(tokenJwt);

            return token;
        }

    }
}
