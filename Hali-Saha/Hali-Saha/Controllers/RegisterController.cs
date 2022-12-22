using HaliSaha_Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Runtime.CompilerServices;

namespace Hali_Saha.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        //sisteme identity üzerinden kayıt olmak için kullanıldıgım komut 
        private readonly UserManager<AppUser> _userManager;
        //private readonly RoleManager<AppRole> _roleManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Register p)
        {
            //returnUrl = returnUrl ?? Url.Content("~/");
            //ExternalLogins 

            //p.Role = "Musteri";
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                        Email =p.KullaniciEmail,
                        UserName=p.KullaniciAd,
                        NameSurname=p.KullaniciSoyad
                        //Role = "Musteri"

                };

                //AppRole role = new AppRole()
                //{
                   
                //    Name = "Musteri",
                //    Id=2

                //};

                var result = await _userManager.CreateAsync(user,p.KullaniciSifre);
                //var addRole = await _userManager.AddToRoleAsync(user,role.Name);

                if (result.Succeeded)
                {
                   /* System.Collections.Generic.IEnumerable<string> roles = new string[1];
                    roles.Append("Musteri");
                    
                     await _userManager.AddToRolesAsync(user, roles);*/


                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);

                    }
                }

            }
            return View(p);
        }

    }
}
