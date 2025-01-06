using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Citas.ClientesApp.Modelos
{
    [Table("usuariosregistrados", Schema = "dbo")]
    public class userModel
    {
        //Crear una nueva tabla de usuarios
        [Key]
        [StringLength(450)]
        public string Id { get; set; }

        [StringLength(256)]
        public string UserName { get; set; }

        [StringLength(256)]
        public string NormalizedUserName { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(256)]
        public string NormalizedEmail { get; set; }

       // public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        //public string SecurityStamp { get; set; }

        //public string ConcurrencyStamp { get; set; }

        //public string PhoneNumber { get; set; }

        //public bool PhoneNumberConfirmed { get; set; }

        //public bool TwoFactorEnabled { get; set; }

        //public DateTimeOffset? LockoutEnd { get; set; }

        //public bool LockoutEnabled { get; set; }

        //public int AccessFailedCount { get; set; }

        public int? EmpresaId { get; set; }

        public int? idrol{ get; set; }

        public int? activo { get; set; }
        [Column("fecharegistro")]
        public DateTime? registerdate { get; set; }
    }
    public enum Estatus_Usuario
    {
        ACTIVO = 1,
        INACTIVO = 2
    }
}
