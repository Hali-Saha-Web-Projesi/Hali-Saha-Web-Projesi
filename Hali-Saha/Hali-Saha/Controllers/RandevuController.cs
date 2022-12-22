using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using HaliSaha_Model.Models;

namespace Hali_Saha.Controllers
{
    public class RandevuController : Controller
    {
        private readonly DbHaliSahaContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RandevuController> _logger;
        private readonly IStringLocalizer<RandevuController> _localizer;

        public RandevuController(DbHaliSahaContext context, UserManager<AppUser> userManager, ILogger<RandevuController> logger, IStringLocalizer<RandevuController> localizer)
        {
            _localizer = localizer;
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
