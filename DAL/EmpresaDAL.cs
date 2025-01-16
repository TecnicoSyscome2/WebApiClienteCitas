using WebApi.Citas.ClientesApp.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Citas.ClientesApp.DAL
{
    public class EmpresaDAL
    {
        private readonly BDConexion _context;

        public EmpresaDAL(BDConexion context)
        {
            _context = context;
        }

        // Read (All)
        public async Task<IEnumerable<empresaModel>> GetAllAsync()
        {
            return await _context.empresas.ToListAsync();
        }

        // Read (By Id)
        public async Task<empresaModel?> GetByIdAsync(int id)
        {
            return await _context.empresas.FindAsync(id);
        }

        public async Task<string?> GetNombreAsesorEmpresaByIdAsync(int id)
        {
            // Busca la empresa por su ID
            var empresa = await _context.empresas.FirstOrDefaultAsync(e => e.IdEmpresa == id);

            // Retorna el campo NombreAsesor si se encuentra, o null si no existe
            return empresa?.NombreAsesor;
        }

    }
}
