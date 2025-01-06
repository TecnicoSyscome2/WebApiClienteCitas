//using WebApi.Citas.Administrador.Auth;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Text.Json;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authentication.JwtBearer;

//using Microsoft.AspNetCore.Cors;
//using WebApi.Citas.Administrador.DAL;
//using WebApi.Citas.Administrador.Modelos;

//namespace ApiRest.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    [EnableCors("MyPolicy")]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//    public class UsuarioController : ControllerBase
//    {
//        private readonly userModel usuarios = new userModel();

//        // Codigo para agregar la seguridad JWT
//        private readonly JwtAuthenticationService authService;
//        public UsuarioController(JwtAuthenticationService pAuthService)
//        {
//            authService = pAuthService;
//        }
//        //************************************************

//        [HttpPost("Buscar")]
//        public async Task<List<userModel>> search([FromBody] userModel pUser)
//        {
//            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
//            string struser = JsonSerializer.Serialize(pUser);
//            userModel users = JsonSerializer.Deserialize<userModel>(struser, option);
//            return await administradorDAL.SearchAsync(users);

//        }

//        [HttpGet]
//        public async Task<IEnumerable<registeredUsers>> Get()
//        {
//            return await UsuarioBL.ObtainAllAsync();
//        }

//        [HttpPut("{id}")]
//        public async Task<ActionResult> Put(int id, [FromBody] registeredUsers pUsuario)
//        {
//            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
//            string strUsuario = JsonSerializer.Serialize(pUsuario);
//            registeredUsers usuario = JsonSerializer.Deserialize<registeredUsers>(strUsuario, option);
//            if (usuario.id == id)
//            {

//                await UsuarioBL.ModifyAsync(usuario);
//                return Ok();
//            }
//            else
//            {
//                return BadRequest();
//            }
//        }

//        [HttpDelete("{id}")]
//        public async Task<ActionResult> Delete(int id)
//        {
//            try
//            {
//                registeredUsers usuario = new registeredUsers();
//                usuario.id = id;
//                await UsuarioBL.DeleteAsync(usuario);
//                return Ok();
//            }
//            catch (Exception)
//            {
//                return BadRequest();
//            }
//        }

//    }

//}

