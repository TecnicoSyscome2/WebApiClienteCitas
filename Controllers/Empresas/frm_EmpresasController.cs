using WebApi.Citas.ClientesApp.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Citas.ClientesApp.Modelos;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Citas.ClientesApp.Controllers.Empresas
{
    [Route("api/Syscome/Empresa")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class frm_EmpresasController : ControllerBase
    {
        private readonly EmpresaDAL _empres;

        public frm_EmpresasController(EmpresaDAL empresaDAL)
        {
            _empres = empresaDAL;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<IActionResult> GetEmpresas()
        {
            var empresa = await _empres.GetAllAsync();
            return Ok(empresa);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpresasById(int id)
        {
            var empresa = await _empres.GetByIdAsync(id);
            if (empresa == null) return NotFound();
            return Ok(empresa);
        }

        [HttpGet("nombreasesor{id}")]
        public async Task<IActionResult> GetNombreAsesorById(int id)
        {
            try
            {
                var empresa = await _empres.GetNombreAsesorEmpresaByIdAsync(id);

                if (empresa == null)
                {
                    return NotFound(new { Message = "No se encontró una empresa con el ID proporcionado." });
                }

                return Ok(empresa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al procesar la solicitud.", Error = ex.Message });
            }
        }
    }
}
