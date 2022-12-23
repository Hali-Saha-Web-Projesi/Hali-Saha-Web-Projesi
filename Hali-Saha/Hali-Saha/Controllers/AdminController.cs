using DataAccess.Data;
using HaliSaha_Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hali_Saha.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] //veritabanı yetkilendirmesi için


    public class AdminController : Controller
    {
        //private readonly RoleManager<AppRole> _roleManager;
        private readonly DbHaliSahaContext _context;
        private readonly ILogger<AdminController> _logger;
        private readonly IStringLocalizer<AdminController> _localizer;

        public AdminController(RoleManager<AppRole> roleManager, DbHaliSahaContext context, ILogger<AdminController> logger, IStringLocalizer<AdminController> localizer)
        {
            //_roleManager = roleManager;
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

        //public async Task<IActionResult> Index()
        //{
        //    return View();
        //}
        public async Task<IActionResult> TesisIndex()
        {
            ViewData["Title"] = _localizer["Spor Tesisleri"];
            ViewData["Title2"] = _localizer["Spor Tesisi Adı"];
            ViewData["Title3"] = _localizer["Adresi"];
            ViewData["Title4"] = _localizer["Tesis Ekle"];
            ViewData["Title5"] = _localizer["Düzenle"];
            ViewData["Title6"] = _localizer["Sil"];
            ViewData["Title7"] = _localizer["Detaylar"];
            return View(await _context.Tesisler.ToListAsync());
        }
        public async Task<IActionResult> TesisDetails(string id)
        {
            ViewData["Title"] = _localizer["Spor Tesisinin Detaylarını Gör"];
            ViewData["Title2"] = _localizer["Spor Tesisi Adı"];
            ViewData["Title3"] = _localizer["Adresi"];
            ViewData["Title4"] = _localizer["Düzenle"];
            ViewData["Title5"] = _localizer["Spor Tesislerine Geri Dön"];
            if (id == null)
            {
                return NotFound();
            }

            var tesisler = await _context.Tesisler
                .FirstOrDefaultAsync(m => m.TesisId.ToString() == id);
            if (tesisler == null)
            {
                return NotFound();
            }

            return View(tesisler);
        }
        // GET: Tesisler/Create
        public IActionResult TesisCreate()
        {
            return View();
        }

        // POST:Tesisler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] //bilgi isteyen kişi gerçekten sen misin diye kontrol eder
        public async Task<IActionResult> TesisCreate([Bind("TesisId,TesisAdi,TesisAdresi,TesisResmi, TesisDegerlendirmesi,TesisPuani")] SporTesisi sporTesisi)
        {
            ViewData["Title"] = _localizer["Spor Tesisi Ekle"];
            ViewData["Title9"] = _localizer["Spor Tesisi Resmi"];
            ViewData["Title10"] = _localizer["Spor Tesisi ID"];
            ViewData["Title2"] = _localizer["Spor Tesisi Adı"];
            ViewData["Title3"] = _localizer["Adresi"];
            ViewData["Title6"] = _localizer["Ekle"];
            ViewData["Title3"] = _localizer["Spor Tesisi Degerlendirmesi"];
            ViewData["Title6"] = _localizer["Spor Tesisi Puanı"];
            ViewData["Title8"] = _localizer["Spor Tesislerine Geri Dön"];
            if (ModelState.IsValid)
            {
                _context.Add(sporTesisi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(TesisIndex));
            }
            return View(sporTesisi);
        }

        // GET: Tesisler/Edit/5
        public async Task<IActionResult> TesisEdit(string id)
        {
            ViewData["Title"] = _localizer["Spor Tesisini Düzenle"];
            ViewData["Title2"] = _localizer["Spor Tesisi Adı"];
            ViewData["Title3"] = _localizer["Adresi"];
            ViewData["Title5"] = _localizer["Kaydet"];
            ViewData["Title6"] = _localizer["Spor Tesislerine Geri Dön"];
            ViewData["Title7"] = _localizer["Tesis Resmi"];
            if (id == null)
            {
                return NotFound();
            }

            var tesisler = await _context.Tesisler.FindAsync(id);
            if (tesisler == null)
            {
                return NotFound();
            }
            return View(tesisler);
        }
        // POST: Tesisler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TesisEdit(string id, [Bind("TesisId,TesisAdi,TesisAdresi,TesisResmi, TesisDegerlendirmesi,TesisPuani")] SporTesisi sporTesisi)
        {
            if (id != sporTesisi.TesisId.ToString())
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
                    if (!SporTesisiExists(sporTesisi.TesisId.ToString()))
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
        public async Task<IActionResult> TesisDelete(string id)
        {
            ViewData["Title"] = _localizer["Spor Tesisini Sil"];
            ViewData["Title2"] = _localizer["Spor Tesisinin Adı"];
            ViewData["Title3"] = _localizer["Adresi"];
            ViewData["Title6"] = _localizer["Sil"];
            ViewData["Title8"] = _localizer["Spor Tesislerine Geri Dön"];
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
        // POST: Tesisler/Delete/5
        [HttpPost, ActionName("TesisDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TesisDeleteConfirmed(string id)
        {
            var sporTesisi = await _context.Tesisler.FindAsync(id);
            _context.Tesisler.Remove(sporTesisi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(TesisIndex));
        }

        private bool SporTesisiExists(string id)
        {
            return _context.Tesisler.Any(e => e.TesisId.ToString() == id);
        }

    }
}
