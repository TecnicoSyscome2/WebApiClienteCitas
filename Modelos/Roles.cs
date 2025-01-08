using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Citas.ClientesApp.Modelos
{
    [Table("roles")]
    public class rolModel
    {
        //crear tabla de roles
        [Key]
        [StringLength(450)]
        public string id { get; set; }

        [StringLength(256)]
        public string nombre { get; set; }

        public string descripcion { get; set; }

    }
}
