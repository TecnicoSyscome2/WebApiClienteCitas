using WebApi.Citas.ClientesApp.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.Citas.ClientesApp.Modelos;
using Microsoft.AspNetCore.Cors;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Principal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApi.Citas.ClientesApp.DAL;
using Microsoft.IdentityModel.Tokens;


namespace ApiRest.Controllers
{

    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly administradorDAL _userRepository;
        private readonly IMemoryCache _cache;
        // Codigo para agregar la seguridad JWT
        private readonly JwtAuthenticationService authService;
        public LoginController(JwtAuthenticationService pAuthService, IMemoryCache cache, administradorDAL user)
        {
            authService = pAuthService;
            _cache = cache;
            _userRepository = user;
        }
        //************************************************
     
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] userModel pUsuario)
        {

            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strUsuario = JsonSerializer.Serialize(pUsuario);
            userModel usuario = JsonSerializer.Deserialize<userModel>(strUsuario, option);
            // codigo para autorizar el usuario por JWT
            userModel usuario_auth = await _userRepository.LoginAsync(usuario);
            if (usuario_auth != null && usuario_auth.Id.IsNullOrEmpty() && usuario.UserName == usuario_auth.UserName)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromHours(8));
                var token = authService.Authenticate(usuario_auth);
                _cache.Set("Nombre", usuario.UserName, cacheEntryOptions);
                _cache.Set("Usuario", usuario.NormalizedUserName, cacheEntryOptions);
                return Ok(token.ToString());
            }
            else
            {
                return Unauthorized();
            }
            // *********************************************
        }

        [HttpPost("CrearUsuario")]
        public async Task<IActionResult> CreateUser([FromBody] userModel newUser)
        {
  
                await _userRepository.CreateAsync(newUser);
                return Ok("Nuevo usuario creado exitosamente");

        }
    }
}
