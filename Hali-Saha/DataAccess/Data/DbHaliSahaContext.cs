
//giriş yap deyince linq ile soegu yapıcaz


using HaliSaha_Model.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    //model classlar arasındaki ilişkiyi veritabanına yansıtmak için dbContext sınıfı kullanılıyor.migration ile db tarafına anlatıyoruz.
    //bu sınıf database içerisine model classlarımı push edecek
    public class DbHaliSahaContext:IdentityDbContext<AppUser,AppRole,int>
    {

        //migrationstaki kodların dolu olması için oluşturulmalı.
        // public DbSet<Kullanici> Kullanicilar { get; set; } //veritabanındaki Kullanicilar tablosu
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<SporTesisi> Tesisler { get; set; }
        //public DbSet<Register> Registers { get; set; }

       // protected override void OnModelCreating(ModelBuilder modelBuilder)
       // {
       //     base.OnModelCreating(modelBuilder);
       //     modelBuilder.Entity<Randevu>()
       //.HasKey(r => new { r.kullaniciId });
       //     modelBuilder.Entity<Randevu>()
       //         .HasOne(r => r.Kullanici)
       //         .WithMany(u => u.randevular)
       //         .HasForeignKey(r => r.kullaniciId);




       //  }
            public DbHaliSahaContext(DbContextOptions<DbHaliSahaContext> options) : base
   (options)
        {
            //Database.EnsureCreated();
            //eklemek için Dbset tanımlanmalı.

        }



    }
}
