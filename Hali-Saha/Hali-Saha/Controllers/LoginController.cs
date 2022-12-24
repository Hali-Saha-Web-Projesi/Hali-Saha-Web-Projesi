using HaliSaha_Model.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hali_Saha.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
        
    {
        //sisteme identity üzerinden authentic olmak için kullanıldıgım komut
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
       // private readonly RoleManager<AppRole> _roleManager; 

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager=userManager;
           
        }
        [HttpGet]
        public IActionResult Index()
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Login p) //asenkron method oldugu için Task ve await kullanılır
        {
            //kullanıcı verilerini kontrol et
            if (ModelState.IsValid)
            {
                //false cookilerin hatırlamasına engel olurken true ise 5 kere yanlış girince banlansın 
                var result = await _signInManager.PasswordSignInAsync(p.KullaniciAd,p.KullaniciSifre,false,true);

                if (result.Succeeded)
                {

                    var user = _userManager.Users.FirstOrDefault(x => x.UserName == p.KullaniciAd);
                    var userRoles = await _userManager.GetRolesAsync(user);
                    
                    if (userRoles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Randevu");
                    }
                      

                }

            }
            return View(p);
        }

    }
}
