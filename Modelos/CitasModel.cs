
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Citas.ClientesApp.Modelos
{
    [Table("citas", Schema = "dbo")]
    public class CitasModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

       // [ForeignKey("clientes")]
        [Column("cliente_id")]
        public long? ClienteId { get; set; }

        [ForeignKey("Asesores")]
        [Column("asesor_id")]
        public long? AsesorId { get; set; }

        [Column("fecha")]
        public DateTime? Fecha { get; set; }

        [Column("estado")]
        [RegularExpression("^(cancelada|confirmada|pendiente)$", ErrorMessage = "El estado debe ser 'cancelada', 'confirmada' o 'pendiente'.")]
        public string Estado { get; set; }
        [Column("horas")]
        public int Horas { get; set; }
        [Column("minutos")]
        public int minutos { get; set; }

        [Column("correo")]
        public string Correo { get; set; }

        [Column("idempresa")]
        public int idempresa { get; set; }

        // Relaciones de navegación
        public virtual Clientes? Cliente { get; set; }
        public virtual AsesoresModel? Asesor { get; set; }

        [NotMapped]
        public string? NombreCliente { get; set; }
        [NotMapped]
        public string? NombreAsesor { get; set; }
    }
    public enum Estado_Citas {
        pendiente,
        confirmada,
        cancelada
       // Finalizada
    }
}
