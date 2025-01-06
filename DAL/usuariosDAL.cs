using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApi.Citas.ClientesApp.Modelos;

namespace WebApi.Citas.ClientesApp.DAL
{

    
    public class administradorDAL
    {
        private readonly BDConexion _context;

        // Inyección de dependencias a través del constructor
        public administradorDAL(BDConexion context)
        {
            _context = context;
        }


        private async Task<bool> ExisteLogin(userModel pUsuario)
        {
            var loginUsuarioExiste = await _context.registeredusers
                .FirstOrDefaultAsync(s => s.UserName == pUsuario.UserName && s.Id != pUsuario.Id);

            return loginUsuarioExiste != null;
        }

        public async Task<int> CreateAsync(userModel pUsuario)
        {
            if (await ExisteLogin(pUsuario))
                throw new Exception("El nombre de usuario ya existe.");

            pUsuario.registerdate = DateTime.Now;
            EncriptarMD5(pUsuario);

            _context.Add(pUsuario);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> ModifyAsync(userModel pUsuario)
        {
            var usuario = await _context.registeredusers.FirstOrDefaultAsync(s => s.Id == pUsuario.Id);
            if (usuario == null)
                throw new Exception("Usuario no encontrado.");

            usuario.idrol = pUsuario.idrol;
            usuario.UserName = pUsuario.UserName;
            usuario.NormalizedUserName = pUsuario.NormalizedUserName;
            usuario.Email = pUsuario.Email;
            usuario.activo = pUsuario.activo;

            _context.Update(usuario);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(string idUsuario)
        {
            var usuario = await _context.registeredusers.FirstOrDefaultAsync(s => s.Id == idUsuario);
            if (usuario == null)
                throw new Exception("Usuario no encontrado.");

            _context.registeredusers.Remove(usuario);
            return await _context.SaveChangesAsync();
        }

        public async Task<userModel> ObtainByIdAsync(string idUsuario)
        {
            var usuario = await _context.registeredusers.FirstOrDefaultAsync(s => s.Id == idUsuario);
            if (usuario == null)
                throw new Exception("Usuario no encontrado.");

            return usuario;
        }

        public async Task<List<userModel>> ObtainAllAsync()
        {
            return await _context.registeredusers.ToListAsync();
        }


        private static IQueryable<userModel> QuerySelect(IQueryable<userModel> query, userModel filter)
        {
            if (!filter.Id.IsNullOrEmpty())
                query = query.Where(s => s.Id == filter.Id);
            if (filter.idrol > 0)
                query = query.Where(s => s.idrol == filter.idrol);
            if (!string.IsNullOrWhiteSpace(filter.UserName))
                query = query.Where(s => s.UserName.Contains(filter.UserName));
            if (!string.IsNullOrWhiteSpace(filter.NormalizedUserName))
                query = query.Where(s => s.NormalizedUserName.Contains(filter.NormalizedUserName));
            if (filter.activo > 0)
                query = query.Where(s => s.activo == filter.activo);
            if (filter.registerdate.HasValue && filter.registerdate.Value.Year > 1000)
            {
                DateTime start = new DateTime(
                    filter.registerdate.Value.Year,
                    filter.registerdate.Value.Month,
                    filter.registerdate.Value.Day,
                    0, 0, 0);

                DateTime end = start.AddDays(1).AddMilliseconds(-1);

                query = query.Where(s => s.registerdate.HasValue &&
                                         s.registerdate.Value >= start &&
                                         s.registerdate.Value <= end);
            }
           

            return query.OrderByDescending(s => s.Id).AsQueryable();
        }

        public async Task<List<userModel>> SearchAsync(userModel filter)
        {
            var query = _context.registeredusers.AsQueryable();
            query = QuerySelect(query, filter);
            return await query.ToListAsync();
        }

        //public async Task<List<userModel>> SearchIncludeRolesAsync(userModel filter)
        //{
        //    var query = _context.registeredusers.AsQueryable();
        //    query = QuerySelect(query, filter).Include(s => s.rol);
        //    return await query.ToListAsync();
        //}

        public async Task<userModel> LoginAsync(userModel user)
        {
            EncriptarMD5(user);
            return await _context.registeredusers.FirstOrDefaultAsync(s =>
                s.UserName== user.UserName &&
                s.PasswordHash == user.PasswordHash &&
                s.activo == (int)Estatus_Usuario.ACTIVO);
        }

        public async Task<int> ChangePasswordAsync(userModel user, string oldPassword)
        {
            var oldPassHash = new userModel { PasswordHash = oldPassword };
            EncriptarMD5(oldPassHash);

            var existingUser = await _context.registeredusers.FirstOrDefaultAsync(s => s.Id == user.Id);
            if (existingUser == null)
                throw new Exception("Usuario no encontrado.");

            if (oldPassHash.PasswordHash != existingUser.PasswordHash)
                throw new Exception("El password actual es incorrecto.");

            EncriptarMD5(user);
            existingUser.PasswordHash = user.PasswordHash;
            _context.Update(existingUser);

            return await _context.SaveChangesAsync();
        }

        // Método para encriptar contraseñas usando MD5
        private static void EncriptarMD5(userModel pUsuario)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(pUsuario.PasswordHash));
                var strEncriptar = string.Concat(result.Select(b => b.ToString("x2").ToLower()));
                pUsuario.PasswordHash = strEncriptar;
            }
        }
    }
}
