using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaliSaha_Model.Models
{
    public class Register
    {
        [Key]
        public int KullaniciId { get; set; }

        [Required(ErrorMessage = "Lütfen adınızı giriniz.")]
        [MaxLength(20)]
        [MinLength(3)]
        [Display(Name = "İsim   :")]
        public string KullaniciAd { get; set; }

        [Required(ErrorMessage = "Lütfen soyadınızı giriniz.")]
        [MaxLength(20)]
        [Display(Name = "Soyisim   :")]
        public string KullaniciSoyad { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre  :")]
        [MinLength(8, ErrorMessage = "Şifreniz minimum 8 karakterden oluşmalıdır.")]
        public string KullaniciSifre { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi tekrar giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Tekrar Şifre  :")]
        [Compare("KullaniciSifre",ErrorMessage ="Şifreler Uyuşmuyor.")]
        public string KullaniciSifreTekrar { get; set; }

        [Required(ErrorMessage = "Lütfen email adresinizi giriniz.")]
        [EmailAddress]
        [Display(Name = "Email  :")]
        public string KullaniciEmail { get; set; }

        public ICollection<Randevu> randevular { get; set; }

    }
}
