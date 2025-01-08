using WebApi.Citas.ClientesApp.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Citas.ClientesApp.DAL
{
    public class CitasDAL
    {
        private readonly BDConexion _context;

        public CitasDAL(BDConexion context)
        {
            _context = context;
        }

        public async Task<CitasModel> CrearCita(CitasModel nuevaCita)
        {

            if (nuevaCita.AsesorId.HasValue)
            {
                var asesor = await _context.asesores.FindAsync(nuevaCita.AsesorId);
                if (asesor == null)
                {
                    throw new ArgumentException("El asesor especificado no existe.");
                }
            }

            _context.citas.Add(nuevaCita);
            await _context.SaveChangesAsync();
            return nuevaCita;
        }

        public async Task<List<CitasModel>> ObtenerCitas()
        {
            return await _context.citas
                .Include(c => c.Cliente)
                .Include(c => c.Asesor)
                .ToListAsync();
        }

        public async Task<CitasModel?> ObtenerCitaPorId(long id)
        {
            var cita = await _context.citas
                .Include(c => c.Cliente)
                .Include(c => c.Asesor)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cita == null)
            {
                throw new KeyNotFoundException("La cita especificada no existe.");
            }

            return cita;
        }

        public async Task<CitasModel?> ActualizarCita(long id, CitasModel citaActualizada)
        {
            var citaExistente = await _context.citas
                .Include(c => c.Cliente)
                .Include(c => c.Asesor)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (citaExistente == null)
            {
                throw new KeyNotFoundException("La cita especificada no existe.");
            }

            // Validar ClienteId
            if (citaActualizada.ClienteId.HasValue)
            {
                var cliente = await _context.clientes.FindAsync(citaActualizada.ClienteId);
                if (cliente == null)
                {
                    throw new ArgumentException("El cliente especificado no existe.");
                }
            }

            if (citaActualizada.AsesorId.HasValue)
            {
                var asesor = await _context.asesores.FindAsync(citaActualizada.AsesorId);
                if (asesor == null)
                {
                    throw new ArgumentException("El asesor especificado no existe.");
                }
            }

            citaExistente.ClienteId = citaActualizada.ClienteId;
            citaExistente.AsesorId = citaActualizada.AsesorId;
            citaExistente.Fecha = citaActualizada.Fecha;
            citaExistente.Estado = citaActualizada.Estado;

            await _context.SaveChangesAsync();
            return citaExistente;
        }

        public async Task<bool> EliminarCita(int id)
        {
            var cita = await _context.citas.FindAsync(id);
            if (cita == null)
            {
                return false;
            }

            _context.citas.Remove(cita);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
