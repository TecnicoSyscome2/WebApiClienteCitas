using WebApi.Citas.ClientesApp.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Citas.ClientesApp.Modelos;

namespace WebApi.Citas.ClientesApp.Controllers.Empresas
{
    [Route("api/Syscome/Empresa")]
    [ApiController]
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

        // GET: api/Usuario/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpresasById(int id)
        {
            var empresa = await _empres.GetByIdAsync(id);
            if (empresa == null) return NotFound();
            return Ok(empresa);
        }
    }
}
