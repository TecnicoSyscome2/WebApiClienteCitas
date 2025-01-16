using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Citas.ClientesApp.Modelos
{
    [Table("citasdet", Schema = "dbo")]
    public class CitasDetModel
    {     
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Column("idcitadet")]
            public int IdCitaDet { get; set; }

            [ForeignKey("Citas")]
            [Column("idcita")]
            public int? IdCita { get; set; }

            [ForeignKey("Productos")]
            [Column("producto")]
            public long? Producto { get; set; }

            [Column("cantidad")]
            public int? Cantidad { get; set; }

            [Column("valortotal", TypeName = "decimal(12,2)")]
            public decimal? ValorTotal { get; set; }

            [Column("fecharegistro")]
            public DateTime? fecharegistro { get; set; }

            public virtual ProductosModel? Productos { get; set; }
            public virtual CitasModel? Citas { get; set; }
    }
}
