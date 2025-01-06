using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Citas.ClientesApp.Modelos
{
    [Table("rolesusuarios")]
    public class rolModel
    {
        //crear tabla de roles
        [Key]
        [StringLength(450)]
        public string Id { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(256)]
        public string NormalizedName { get; set; }

        public string ConcurrencyStamp { get; set; }


    }
}
