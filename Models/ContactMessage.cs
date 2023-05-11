using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Models/ContactMessage.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JDTelecomunicaciones.Models
{
  [Table("mensaje_contacto")]
    public class MensajeContacto
    {

      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      [Column("id")]
      public int Id { get; set; }


      [Column("dni")]
      [Display(Name = "DNI")]
      public string DNI { get; set; }

      [Column("numero_telefono")]
      [Display(Name = "Número de teléfono")]
      public string NumeroTelefono { get; set; }

      [Column("mensaje")]
      [Display(Name = "Mensaje")]
      public string Mensaje { get; set; }
    }
}