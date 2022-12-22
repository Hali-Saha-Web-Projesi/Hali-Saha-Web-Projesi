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
        private readonly RoleManager<AppRole> _roleManager;

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
                    AssignRole(p.KullaniciId);
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

        [HttpGet]
        public async void AssignRole(int id)
        {

            var user=_userManager.Users.FirstOrDefault(x => x.Id == id);
            var roles = _roleManager.Roles.ToList();
            TempData["Userid"] = user.Id;
            var userRoles = await _userManager.GetRolesAsync(user);
            List<RoleAssignViewModel> model=new List<RoleAssignViewModel>();
            foreach (var role in roles)
            {
                RoleAssignViewModel m = new RoleAssignViewModel();
                m.RoleId = role.Id;
                m.RoleName = role.Name; 
                model.Add(m);   
            }
 
        }
    }
}
