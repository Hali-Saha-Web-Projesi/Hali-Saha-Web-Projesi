using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HaliSaha_Model.Models
{
    public class AppUser :IdentityUser <int> //<int> alıyordu onceden sıkıntı cıkacak mı bak
    {
        public string NameSurname { get; set; }
       // public ICollection<Randevu> randevular { get; set; }
        //public string Password { get; set; }
        // public string Role { get; set; }
        //[Display(Name = "Adınız")]
        //public string uyeAdi { get; set; }
        //[Display(Name = "Soyadınız")]
        //public string uyeSoyadi { get; set; }
        //[Display(Name = "Telefon Numaranız")]
        //[DataType(DataType.PhoneNumber)]
        //public string musteriTelNo { get; set; }
        //public ICollection<Randevu> randevular { get; set; }

        //public string ImageUrl { get; set; }

    }
}
