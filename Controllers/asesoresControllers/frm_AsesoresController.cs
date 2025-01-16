using WebApi.Citas.ClientesApp.DAL;
using WebApi.Citas.ClientesApp.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace WebApi.Citas.ClientesApp.Controllers.asesoresControllers
{
    [Route("api/Syscome/asesor")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class frm_AsesoresController : ControllerBase
    {
        private readonly AsesoresDAL _asesor;

        public frm_AsesoresController(AsesoresDAL asesoresDAL)
        {
            _asesor = asesoresDAL;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerAsesores()
        {
            var asesores = await _asesor.GetAllAsync();
            return Ok(asesores);
        }

        [HttpGet("asesorxempre{ID}")]
        public async Task<ActionResult<List<AsesoresModel>>> GetByIdAsync(long ID)
        {
            var asesor = await _asesor.GetByIdAsync(ID);

            if (asesor == null || asesor.Count == 0)
            {
                return NotFound("No se encontraron asesores para esta empresa.");
            }

            return Ok(asesor);
        }
    }
}
