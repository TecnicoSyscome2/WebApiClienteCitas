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
    public class userModel : Logins
    {
        //Crear una nueva tabla de usuarios
        [Key]    
        [Column("id")]
        public int Id { get; set; }

        [StringLength(256)]
        [Column("username")]
        public string UserName { get; set; }

        [StringLength(256)]
        [Column("email")]
        [Required]
        public string Email { get; set; }

        [Column("password")]
        [Required]
        public string Password { get; set; }

        [Column("empresaid")]
        public int? EmpresaId { get; set; }

        [Column("idrol")]
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

    public interface Logins
    {
        string Email { get; set; }
        string Password { get; set; }
    }
}
