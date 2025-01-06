using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Citas.ClientesApp.Modelos
{
    [Table("clientes", Schema = "dbo")]
    public class Clientes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("nombre")]
        public string? Nombre { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("telefono")]
        public string? Telefono { get; set; }

        [Column("tipo_usuario")]
        [RegularExpression("^(personal|paciente)$", ErrorMessage = "El tipo de usuario debe ser 'personal' o 'paciente'.")]
        public string? TipoUsuario { get; set; }
    }
}