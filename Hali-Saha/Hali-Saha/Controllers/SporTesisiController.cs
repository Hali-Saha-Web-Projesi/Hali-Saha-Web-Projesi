using DataAccess.Data;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;


namespace Hali_Saha.Controllers
{
    public class SporTesisiController : Controller
    {
        private readonly DbHaliSahaContext _context;
        private readonly ILogger<SporTesisiController> _logger;
        private readonly IStringLocalizer<SporTesisiController> _localizer;

        public SporTesisiController(DbHaliSahaContext context, ILogger<SporTesisiController> logger, IStringLocalizer<SporTesisiController> localizer)
        {
            _context = context;
            _logger = logger;
            _localizer = localizer;
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) }
            );

            return LocalRedirect(returnUrl);
        }
        // GET: GuzellikMerkezi
        public async Task<IActionResult> Index()
        {
            ViewData["Title2"] = _localizer["Yeni Merkez Ekle"];
            ViewData["Title3"] = _localizer["Detaylar"];
            ViewData["Title4"] = _localizer["Güzellik Merkezleri"];
            return View(await _context.Tesisler.ToListAsync());
        }

        // GET: GuzellikMerkezi/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Title"] = _localizer["Güzellik Merkezi Detayları"];
            ViewData["Title1"] = _localizer["Güzellik Merkezi Adı"];
            ViewData["Title2"] = _localizer["Adres"];
            ViewData["Title3"] = _localizer["Email adresi"];
            ViewData["Title4"] = _localizer["Güzellik Merkezi"];
            if (id.ToString() == null)
            {
                return NotFound();
            }

            var guzellikMerkezi = await _context.Tesisler
                .FirstOrDefaultAsync(m => m.TesisId == id);
            if (guzellikMerkezi == null)
            {
                return NotFound();
            }

            return View(guzellikMerkezi);
        }
        

    }
}
