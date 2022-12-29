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
        //[Key]
        public int randevuId { get; set; } //primary key olmasa hiç olur mu hocam?
        //[ForeignKey("kullanici")]
        public int kullanici_Id { get; set; } //foreign key eklemeli miyiz
        //public AppUser Kullanici { get; set; }  
        
        public string TesisAdi { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Randevu Günü ve Saati")]
        public DateTime randevuSaati { get; set; }

    }
}
