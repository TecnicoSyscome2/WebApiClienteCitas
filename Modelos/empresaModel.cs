using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Citas.ClientesApp.Modelos
{
    [Table("empresas", Schema = "dbo")]
    public class empresaModel
    { 
            [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int IdEmpresa { get; set; }

            [Column("nombreempresa")]
            [MaxLength(150)]
            public string? NombreEmpresa { get; set; }

            [Column("correo")]
            [MaxLength(50)]
            [EmailAddress]
            public string? Correo { get; set; }

            [Column("telefono")]
            [MaxLength(50)]
            public string? Telefono { get; set; }

            [Column("pais")]
            public int? Pais { get; set; }

            [Column("contacto")]
            [MaxLength(50)]
            public string? Contacto { get; set; }

            [Column("claveprincipal")]
            [MaxLength(50)]
            public string? ClavePrincipal { get; set; }

            [Column("nombreasesor")]
            [MaxLength(100)]
            public string? NombreAsesor { get; set; }

            [Column("hostsmtp")]
            [MaxLength(50)]
            public string? HostSmtp { get; set; }

            [Column("portsmtp")]
            [MaxLength(10)]
            public string? PortSmtp { get; set; }

            [Column("sllsmtp")]
            [MaxLength(5)]
            public string? SllSmtp { get; set; }

            [Column("usuariosmtp")]
            [MaxLength(20)]
            public string? UsuarioSmtp { get; set; }

            [Column("contraseñasmtp")]
            [MaxLength(50)]
            public string? ContraseñaSmtp { get; set; }
    }
}
