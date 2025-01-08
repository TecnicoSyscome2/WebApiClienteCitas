using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebApi.Citas.ClientesApp.Modelos;
using Microsoft.EntityFrameworkCore;
using WebApi.Citas.ClientesApp.DAL;


namespace citasApp.clientes.DAL
{
    public class ClientesDAL
    {
        private readonly BDConexion _context;

        public ClientesDAL(BDConexion context)
        {
            _context = context;
        }

        // Create
        public async Task<Clientes> CreateAsync(Clientes clientes)
        {
            _context.clientes.Add(clientes);
            await _context.SaveChangesAsync();
            return clientes;
        }

        // Read (All)
        public async Task<IEnumerable<Clientes>> GetAllAsync()
        {
            return await _context.clientes.ToListAsync();
        }

        // Read (By Id)
        public async Task<Clientes?> GetByIdAsync(long id)
        {
            return await _context.clientes.FindAsync(id);
        }

        // Update
        public async Task<Clientes?> UpdateAsync(Clientes clientes)
        {
            var existingClientes = await _context.clientes.FindAsync(clientes);
            if (existingClientes == null) return null;
            existingClientes.Id = clientes.Id;
            existingClientes.Nombre = clientes.Nombre;
            existingClientes.Email = clientes.Email;
            existingClientes.Telefono = clientes.Telefono;
            existingClientes.TipoUsuario = clientes.TipoUsuario;

            _context.clientes.Update(clientes);
            await _context.SaveChangesAsync();
            return existingClientes;
        }

        // Delete
        public async Task<bool> DeleteAsync(long id)
        {
            var usuario = await _context.clientes.FindAsync(id);
            if (usuario == null) return false;

            _context.clientes.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
