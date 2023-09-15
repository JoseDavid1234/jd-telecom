using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JDTelecomunicaciones.Models
{
    [Table("promocion")]
    public class Promocion
    {
        [Column("id_promocion")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id_promocion {get;set;}
        public string nombre_promocion {get;set;}
        public string efecto_promocion {get;set;}

        public Usuario usuario {get;set;}
    }
}