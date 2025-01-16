using WebApi.Citas.ClientesApp.DAL;
using WebApi.Citas.ClientesApp.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace WebApi.Citas.ClientesApp.Controllers.Productos
{
    [Route("api/Syscome/producto")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class frm_ProductosController : ControllerBase
    {
        private readonly ProductosDAL _productos;

        public frm_ProductosController(ProductosDAL productos)
        {
            _productos = productos;
        }

  
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ProductosModel>>> GetByIdAsync(long id)
        {
            var producto = await _productos.GetByIdAsync(id);

            if (producto == null || producto.Count == 0)
            {
                return NotFound("No se encontraron empresas con el ID proporcionado.");
            }

            return Ok(producto);
        }

        [HttpGet("especialidad/{id}")]
        public async Task<IActionResult> GetEspecialidadById(long id)
        {
            var especialidad = await _productos.GetEspecialidadByIdAsync(id);

            if (especialidad == null)
            {
                return NotFound();
            }

            return Ok(especialidad);
        }
    }
}
