using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Citas.ClientesApp.Modelos;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Citas.ClientesApp.DAL
{
    public class BDConexion : DbContext
    {
        // Define las entidades del modelo
        public DbSet<Clientes> clientes { get; set; }
        public DbSet<CitasModel> citas { get; set; }
        public DbSet<AsesoresModel> asesores { get; set; }
        public DbSet<empresaModel> empresas { get; set; }
        public DbSet<ProductosModel> productos { get; set; }
        public DbSet<CitasDetModel> citasdet { get; set; }
        public DbSet<userModel> registeredusers { get; set; }

        // Constructor que utiliza las opciones configuradas externamente
        public BDConexion(DbContextOptions<BDConexion> options) : base(options) { }

        // Configuración del modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de las tablas y relaciones
            modelBuilder.Entity<Clientes>().ToTable("clientes", schema: "dbo");
            modelBuilder.Entity<empresaModel>().ToTable("empresas", schema: "dbo");

            modelBuilder.Entity<ProductosModel>().ToTable("productos", schema: "dbo")
                .HasOne(c => c.Empresa)
                .WithMany()
                .HasForeignKey(c => c.IdEmpresa)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AsesoresModel>().ToTable("Asesores", schema: "dbo");

            modelBuilder.Entity<CitasModel>()
                .HasOne(c => c.Asesor)
                .WithMany()
                .HasForeignKey(c => c.AsesorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CitasDetModel>()
                .HasOne(c => c.Citas)
                .WithMany()
                .HasForeignKey(c => c.IdCitaDet)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CitasDetModel>()
                .HasOne(c => c.Productos)
                .WithMany()
                .HasForeignKey(c => c.Producto)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
