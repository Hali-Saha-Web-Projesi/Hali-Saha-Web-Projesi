using HaliSaha_Model.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
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
            string[] roles = new string[2];
            roles[0] = "Admin";
            roles[1] = "Musteri";

            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                        Email =p.KullaniciEmail,
                        UserName=p.KullaniciAd,
                        NameSurname=p.KullaniciSoyad

                };
                var result = await _userManager.CreateAsync(user,p.KullaniciSifre);
                //var addRole = await _userManager.AddToRoleAsync(user,role.Name);

                if (result.Succeeded)
                {
                    /* System.Collections.Generic.IEnumerable<string> roles = new string[1];
                     roles.Append("Musteri");

                      await _userManager.AddToRolesAsync(user, roles);*/
                    var claims = new List<Claim>
                    {
                        //sayfadaki cookie kısmı buradaki name ve rolu tutuyor
                        new Claim(ClaimTypes.Name,p.KullaniciAd),
                        new Claim(ClaimTypes.Role,roles[1])  //user'ın rollerini alıp roller listesini yollayabilirsin
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties();

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

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
