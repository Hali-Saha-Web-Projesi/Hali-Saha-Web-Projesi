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
        public async Task<IActionResult> Index(Login p) //asenkron method oldugu için Task ve await kullanılır
        {
            string[] roles = new string[2];
            roles[0] = "Admin";
            roles[1] = "Musteri";

            //kullanıcı verilerini kontrol et
            if (ModelState.IsValid)
            {
                //false cookilerin hatırlamasına engel olurken true ise 5 kere yanlış girince banlansın 
                var result = await _signInManager.PasswordSignInAsync(p.KullaniciAd,p.KullaniciSifre,false,true);

                if (result.Succeeded)
                {
                    var claims = new List<Claim>
                    {
                        //sayfadaki cookie kısmı buradaki name ve rolu tutuyor
                        new Claim(ClaimTypes.Name,p.KullaniciAd),
                        new Claim(ClaimTypes.Role,roles[0])  //user'ın rollerini alıp roller listesini yollayabilirsin
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties();

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                    return RedirectToAction("TesisIndex", "Admin");

                }

            }
            return View(p);
        }

    }
}
