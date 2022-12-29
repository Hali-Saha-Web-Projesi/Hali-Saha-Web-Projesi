using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HaliSaha_Model.Models
{
    public class Login
    {
        [Key]
        public int KullaniciId { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre  :")]
        [MinLength(3, ErrorMessage = "Şifreniz minimum 3 karakterden oluşmalıdır.")]
        public string KullaniciSifre { get; set; }

        [Required(ErrorMessage = "Lütfen kullanıcı adınızı giriniz.")]
        [Display(Name = "Kullanıcı Adınız  :")]
        public string KullaniciAd { get; set; }

        //public ICollection<Randevu> randevular { get; set; }
    }
}
