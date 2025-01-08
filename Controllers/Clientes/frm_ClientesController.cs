using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Citas.ClientesApp.Modelos;
using WebApi.Citas.ClientesApp.DAL;
using citasApp.clientes.DAL;
namespace WebApi.Citas.ClientesApp.Controllers.Usuarios
{
    [Route("api/Syscome/cliente")]
    [ApiController]
    public class frm_ClientesController : ControllerBase
    {
        private readonly ClientesDAL _client;

        public frm_ClientesController(ClientesDAL clientesDAL)
        {
            _client = clientesDAL;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _client.GetAllAsync();
            return Ok(usuarios);
        }

        // GET: api/Usuario/{id}
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetUsuarioById(long id)
        {
            var usuario = await _client.GetByIdAsync(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        // POST: api/Usuario
        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] Clientes clientes)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createCliente = await _client.CreateAsync(clientes);
            return CreatedAtAction(nameof(GetUsuarioById), new { id = createCliente.Id }, createCliente);
        }

        //// PUT: api/Usuario/{id}
        //[HttpPut("{id:long}")]
        //public async Task<IActionResult> UpdateUsuario(long id, [FromBody] Clientes clientes)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);
        //    if (id != clientes.Id) return BadRequest("ID mismatch.");

        //    var updatedUsuario = await _client.UpdateAsync(clientes);
        //    if (updatedUsuario == null) return NotFound();

        //    return Ok(updatedUsuario);
        //}

        //// DELETE: api/Usuario/{id}
        //[HttpDelete("{id:long}")]
        //public async Task<IActionResult> DeleteUsuario(long id)
        //{
        //    var deleted = await _client.DeleteAsync(id);
        //    if (!deleted) return NotFound();

        //    return NoContent();
        //}
    }
}
