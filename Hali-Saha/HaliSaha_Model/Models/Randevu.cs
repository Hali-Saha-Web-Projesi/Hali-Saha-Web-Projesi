using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HaliSaha_Model.Models
{
    public class Randevu
    {
        [Key]
        public int RandevuId { get; set; }

        [ForeignKey("kullanici")]
        public int KullaniciId { get; set; }
        public Kullanici kullanici1 { get; set; } //sen bir şuraya bak

        public Register kullanici;

       

        [ForeignKey("sporTesisi")]
        public int TesisId { get; set; } 
        public SporTesisi sporTesisi { get; set; }


        [DataType(DataType.DateTime)]
        [Display(Name = "Randevu Günü ve Saati")]
        public DateTime randevuTarihi { get; set; }
    }
}
