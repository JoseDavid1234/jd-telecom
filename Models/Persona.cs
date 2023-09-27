using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JDTelecomunicaciones.Models
{
    [Table("persona")]
    public class Persona
    {

        [Column("id_persona")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id_persona {get;set;}

        public string nombrePersona {get;set;}
        public string apPatPersona {get;set;}

        public string apMatPersona {get;set;}

        public string dniPersona {get;set;}

        public char sexoPersona {get;set;}
    }
}