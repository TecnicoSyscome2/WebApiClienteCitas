using WebApi.Citas.ClientesApp.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Citas.ClientesApp.DAL
{
    public class ProductosDAL
    {
        private readonly BDConexion _context;

        public ProductosDAL(BDConexion context)
        {
            _context = context;
        }
        public async Task<List<ProductosModel>> GetByIdAsync(long id)
        {
            return await _context.productos.Where(e => e.IdEmpresa == id).ToListAsync();
        }
        public async Task<ProductosModel> GetEspecialidadByIdAsync(long id)
        {
            // Obtiene solo la especialidad del producto por ID
            var especialidad = await _context.productos
                                             .Where(p => p.Id == id)
                                             .Select(p => new ProductosModel
                                             {
                                                 Especialidad = p.Especialidad
                                             })
                                             .FirstOrDefaultAsync();

            return especialidad;
        }

    }
}
