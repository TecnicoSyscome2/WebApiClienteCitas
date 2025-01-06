using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Citas.ClientesApp.Modelos
{
    [Table("productos", Schema = "dbo")]
    public class ProductosModel
    {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Column("id")]
            public long Id { get; set; }

            [Column("especialidad")]
            public int? Especialidad { get; set; }

            [Column("descripcion", TypeName = "text")]
            public string? Descripcion { get; set; }

            [Column("imagen", TypeName = "text")]
            public string? Imagen { get; set; }

            [Column("tipoprecio")]
            public int? TipoPrecio { get; set; }

            [Column("precio", TypeName = "decimal(12,2)")]
            public decimal? Precio { get; set; }

            [Column("descuento")]
            public int? Descuento { get; set; }

            [Column("incluyeiva")]
            public int? IncluyeIva { get; set; }

            [Column("tiempo")]
            public int? Tiempo { get; set; }

            [ForeignKey("Empresa")]
            [Column("idempresa")]
            public int? IdEmpresa { get; set; }

            public virtual empresaModel? Empresa { get; set; }

        
    }
}
