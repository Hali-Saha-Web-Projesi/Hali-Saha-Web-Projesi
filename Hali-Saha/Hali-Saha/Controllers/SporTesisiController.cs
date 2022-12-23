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
            ViewData["Title2"] = _localizer["Yeni Tesis Ekle"];
            ViewData["Title3"] = _localizer["Detaylar"];
            ViewData["Title4"] = _localizer["Spor Tesisleri"];
            return View(await _context.Tesisler.ToListAsync());
        }
        // GET: GuzellikMerkezi/Details/5
        public async Task<IActionResult> Details(string id)
        {
            ViewData["Title"] = _localizer["Güzellik Merkezi Detayları"];
            ViewData["Title1"] = _localizer["Spor Tesisi Adı"];
            ViewData["Title2"] = _localizer["Adres"];
            ViewData["Title4"] = _localizer["Spor Tesisi"];
            if (id == null)
            {
                return NotFound();
            }

            var sporTesisi = await _context.Tesisler
                .FirstOrDefaultAsync(m => m.TesisId.ToString() == id);
            if (sporTesisi == null)
            {
                return NotFound();
            }

            return View(sporTesisi);
        }

    }
}
