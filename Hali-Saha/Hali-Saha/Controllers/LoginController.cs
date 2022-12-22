using HaliSaha_Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hali_Saha.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
        
    {
        //sisteme identity üzerinden authentic olmak için kullanıldıgım komut
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Index()
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Login p)
        {
            if (ModelState.IsValid)
            {
                //false cookilerin hatırlamasına engel olurken true ise 5 kere yanlış girince banlansın 
                var result = await _signInManager.PasswordSignInAsync(p.KullaniciAd,p.KullaniciSifre,false,true);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");

                }

            }
            return View(p);
        }

    }
}
