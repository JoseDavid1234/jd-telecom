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

    [Column("nombre_completo")]
    [Display(Name = "Nombre Completo")]
    public string NombreCompleto { get; set; }

    [Column("dni")]
    [Display(Name = "N° de Documento de Identidad")]
    public string DNI { get; set; }

    [Column("correo_electronico")]
    [Display(Name = "Correo Electrónico")]
    public string CorreoElectronico { get; set; }

    [Column("numero_telefono")]
    [Display(Name = "Número de teléfono")]
    public string NumeroTelefono { get; set; }

    [Column("politicas_privacidad")]
    [Display(Name = "He leído y aceptado las políticas de privacidad")]
    public bool PoliticasPrivacidad { get; set; }

    [Column("autorizo_publicidad")]
    [Display(Name = "Autorizo el envío de publicidad e información comercial")]
    public bool AutorizoPublicidad { get; set; }
  }
}
