using WebApi.Citas.ClientesApp.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Citas.ClientesApp.DAL
{
    public class AsesoresDAL
    {
        private readonly BDConexion _context;

        public AsesoresDAL(BDConexion context)
        {
            _context = context;
        }

        // Read (All)
        public async Task<IEnumerable<AsesoresModel>> GetAllAsync()
        {
            return await _context.asesores.ToListAsync();
        }

        public async Task<List<AsesoresModel>> GetByIdAsync(long id)
        {
            return await _context.asesores.Where(e => e.Especialidad == id).ToListAsync();
        }
    }
}
