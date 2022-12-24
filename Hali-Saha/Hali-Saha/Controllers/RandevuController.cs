using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Hali_Saha.Controllers
{
    [Authorize]
    public class RandevuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
