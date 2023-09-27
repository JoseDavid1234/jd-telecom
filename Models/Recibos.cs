using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JDTelecomunicaciones.Models
{
    [Table("recibo")]
     public class Recibos
    {
        [Column("id_recibo")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idRecibo {get;set;}
        public string plan_recibo {get;set;}
        public string mes_recibo {get;set;}
        public string fecha_vencimiento {get;set;}
        public decimal monto_recibo {get;set;}
        public string estado_recibo {get;set;}
        public Usuario usuario {get;set;}

    }
}