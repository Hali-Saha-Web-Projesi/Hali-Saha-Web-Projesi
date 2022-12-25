using DataAccess.Data;
using HaliSaha_Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Hali_Saha.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private readonly DbHaliSahaContext _context;
        private readonly ILogger<AdminController> _logger;
        private readonly IStringLocalizer<AdminController> _localizer;

        public AdminController(DbHaliSahaContext context, ILogger<AdminController> logger, IStringLocalizer<AdminController> localizer)
        {
            _context = context;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index()
        {

            return View();
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
        public async Task<IActionResult> TesisIndex()
        {
            ViewData["Title"] = _localizer["Spor Tesisleri"];
            ViewData["Title2"] = _localizer["Spor Tesisi Adı"];
            ViewData["Title3"] = _localizer["Adresi"];
            ViewData["Title5"] = _localizer["Merkez Ekle"];
            ViewData["Title3"] = _localizer["Düzenle"];
            ViewData["Title4"] = _localizer["Sil"];
            ViewData["Title5"] = _localizer["Detaylar"];
            return View(await _context.Tesisler.ToListAsync());
        }
        public async Task<IActionResult> MerkezDetails(int id)
        {
            ViewData["Title"] = _localizer["Spor Tesislerinin Detaylarını Gör"];
            ViewData["Title2"] = _localizer["Spor Tesisi Adı"];
            ViewData["Title3"] = _localizer["Adresi"];
            ViewData["Title6"] = _localizer["Düzenle"];
            ViewData["Title8"] = _localizer["Spor Tesislerine Geri Dön"];
            if (id.ToString() == null)
            {
                return NotFound();
            }

            var sporTesisi = await _context.Tesisler
                .FirstOrDefaultAsync(m => m.TesisId == id);
            if (sporTesisi == null)
            {
                return NotFound();
            }

            return View(sporTesisi);
        }
        // GET: GuzellikMerkezi/Create
        public IActionResult MerkezCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MerkezCreate([Bind(nameof(SporTesisi.TesisId), nameof(SporTesisi.TesisAdi), nameof(SporTesisi.TesisAdresi), nameof(SporTesisi.TesisResmi))] SporTesisi sporTesisi)
        {
            ViewData["Title"] = _localizer["Spor Tesisi Ekle"];
            ViewData["Title9"] = _localizer["Spor Tesisi Resmi"];
            ViewData["Title10"] = _localizer["Spor Tesisi ID"];
            ViewData["Title2"] = _localizer["Spor Tesisi Adı"];
            ViewData["Title3"] = _localizer["Adresi"];
            ViewData["Title6"] = _localizer["Ekle"];
            ViewData["Title8"] = _localizer["Spor Tesislerine Geri Dön"];
            if (ModelState.IsValid)
            {
                _context.Add(sporTesisi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(TesisIndex));
            }
            return View(sporTesisi);
        }
        // GET: GuzellikMerkezi/Edit/5
        public async Task<IActionResult> MerkezEdit(int id)
        {
            ViewData["Title"] = _localizer["Spor Tesisini Düzenle"];
            ViewData["Title2"] = _localizer["Spor Tesisi Adı"];
            ViewData["Title3"] = _localizer["Adresi"];
            ViewData["Title5"] = _localizer["Kaydet"];
            ViewData["Title6"] = _localizer["Spor Tesislerine Geri Dön"];
            ViewData["Title7"] = _localizer["Spor Tesisi Resmi"];
            if (id.ToString() == null)
            {
                return NotFound();
            }

            var sporTesisi = await _context.Tesisler.FindAsync(id);
            if (sporTesisi == null)
            {
                return NotFound();
            }
            return View(sporTesisi);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MerkezEdit(int id, [Bind(nameof(SporTesisi.TesisId), nameof(SporTesisi.TesisAdi), nameof(SporTesisi.TesisAdresi), nameof(SporTesisi.TesisResmi))] SporTesisi sporTesisi)
        {
            if (id != sporTesisi.TesisId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sporTesisi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SporTesisiExists(sporTesisi.TesisId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw; 
                    }
                }
                return RedirectToAction(nameof(TesisIndex));
            }
            return View(sporTesisi);
        }
        // GET: GuzellikMerkezi/Delete/5
        public async Task<IActionResult> MerkezDelete(int id)
        {
            ViewData["Title"] = _localizer["Spor Tesisini Sil"];
            ViewData["Title2"] = _localizer["Spor Tesisi Adı"];
            ViewData["Title3"] = _localizer["Adresi"];
            ViewData["Title6"] = _localizer["Sil"];
            ViewData["Title8"] = _localizer["Spor Tesislerine Geri Dön"];
            if (id.ToString() == null)
            {
                return NotFound();
            }

            var sporTesisi = await _context.Tesisler
                .FirstOrDefaultAsync(m => m.TesisId == id);
            if (sporTesisi == null)
            {
                return NotFound();
            }

            return View(sporTesisi);
        }
        // POST: GuzellikMerkezi/Delete/5
        [HttpPost, ActionName("MerkezDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MerkezDeleteConfirmed(int id)
        {
            var sporTesisi = await _context.Tesisler.FindAsync(id);
            _context.Tesisler.Remove(sporTesisi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(TesisIndex));
        }

        private bool SporTesisiExists(int id)
        {
            return _context.Tesisler.Any(e => e.TesisId == id);
        }

    }
}
