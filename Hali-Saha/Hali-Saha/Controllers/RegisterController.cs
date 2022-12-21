using HaliSaha_Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hali_Saha.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller 
    {
        //sisteme identity üzerinden kayıt olmak için kullanıldıgım komut 
        private readonly UserManager<AppUser> _userManager;

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
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                        Email =p.KullaniciEmail,
                        UserName=p.KullaniciAd,
                        NameSurname=p.KullaniciSoyad

                };
                var result = await _userManager.CreateAsync(user,p.KullaniciSifre);
                if (result.Succeeded)
                {
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
