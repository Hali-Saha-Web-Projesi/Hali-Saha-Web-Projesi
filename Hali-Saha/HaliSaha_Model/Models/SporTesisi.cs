using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaliSaha_Model.Models
{
    public class SporTesisi
    {
        [Key]
        public int TesisId { get; set; }

        [Display(Name = "Tesis Adı  :")]
        public string TesisAdi { get; set; }

        [Display(Name = "Tesis Adresi  :")]
        public string TesisAdresi { get; set; }


        [DataType(DataType.ImageUrl)]
        public string TesisResmi { get; set; }

        [Display(Name = "Değerlendirmeniz")]
        [DataType(DataType.MultilineText)]
        public string TesisDegerlendirmesi { get; set; }

        [Display(Name = "Puanınız")]
        public int TesisPuani { get; set; }

        public ICollection<Randevu> randevular { get; set; }



    }
}
