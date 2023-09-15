using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JDTelecomunicaciones.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Column("id_usuario")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id_usuario {get;set;}
        public string correo_usuario {get;set;}
        public string nombre_usuario {get;set;}
        public string contraseña_usuario {get;set;}

        public char rol_usuario {get;set;}

        public Persona persona {get;set;}

    }
}