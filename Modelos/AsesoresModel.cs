using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Citas.ClientesApp.Modelos
{
    [Table("Asesores", Schema = "dbo")]
    public class AsesoresModel
    {

            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public long Id { get; set; }

            [Column("nombre")]
            [StringLength(30)]
            public string? Nombre { get; set; }

            [Column("especialidad")]
            public int? Especialidad { get; set; }

            [Column("establecimientos_id")]
            public int? EstablecimientosId { get; set; }

            [Column("empresa_id")]
            public int? EmpresaId { get; set; }

    }
}
