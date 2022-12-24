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

        //public async Task<IActionResult> Index()
        //{
            
        //    return View();
        //}

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
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = _localizer["Güzellik Merkezleri"];
            ViewData["Title2"] = _localizer["Güzellik Merkezi Adı"];
            ViewData["Title3"] = _localizer["Adresi"];
            ViewData["Title4"] = _localizer["Email Adresi"];
            ViewData["Title5"] = _localizer["Merkez Ekle"];
            ViewData["Title3"] = _localizer["Düzenle"];
            ViewData["Title4"] = _localizer["Sil"];
            ViewData["Title5"] = _localizer["Detaylar"];
            return View(await _context.Tesisler.ToListAsync());
        }
        //public async Task<IActionResult> MerkezDetails(int id)
        //{
        //    ViewData["Title"] = _localizer["Güzellik Merkezlerinin Detaylarını Gör"];
        //    ViewData["Title2"] = _localizer["Güzellik Merkezi Adı"];
        //    ViewData["Title3"] = _localizer["Adresi"];
        //    ViewData["Title4"] = _localizer["Email Adresi"];
        //    ViewData["Title6"] = _localizer["Düzenle"];
        //    ViewData["Title7"] = _localizer["Randevu Al"];
        //    ViewData["Title8"] = _localizer["Güzellik Merkezlerine Geri Dön"];
        //    if (id.ToString() == null)
        //    {
        //        return NotFound();
        //    }

        //    var guzellikMerkezi = await _context.Tesisler
        //        .FirstOrDefaultAsync(m => m.TesisId == id);
        //    if (guzellikMerkezi == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(guzellikMerkezi);
        //}
        //// GET: GuzellikMerkezi/Create
        //public IActionResult MerkezCreate()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> MerkezCreate([Bind("TesisId,TesisAdi,TesisAdresi,TesisResmi,TesisDegerlendirmesi,TesisPuani")] SporTesisi guzellikMerkezi)
        //{
        //    ViewData["Title"] = _localizer["Güzellik Merkezi Ekle"];
        //    ViewData["Title9"] = _localizer["Güzellik Merkezi Resmi"];
        //    ViewData["Title10"] = _localizer["Güzellik Merkezi ID"];
        //    ViewData["Title2"] = _localizer["Güzellik Merkezi Adı"];
        //    ViewData["Title3"] = _localizer["Adresi"];
        //    ViewData["Title4"] = _localizer["Email Adresi"];
        //    ViewData["Title6"] = _localizer["Ekle"];
        //    ViewData["Title8"] = _localizer["Güzellik Merkezlerine Geri Dön"];
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(guzellikMerkezi);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(MerkezIndex));
        //    }
        //    return View(guzellikMerkezi);
        //}
        //// GET: GuzellikMerkezi/Edit/5
        //public async Task<IActionResult> MerkezEdit(int id)
        //{
        //    ViewData["Title"] = _localizer["Güzellik Merkezini Düzenle"];
        //    ViewData["Title2"] = _localizer["Güzellik Merkezi Adı"];
        //    ViewData["Title3"] = _localizer["Adresi"];
        //    ViewData["Title4"] = _localizer["Email Adresi"];
        //    ViewData["Title5"] = _localizer["Kaydet"];
        //    ViewData["Title6"] = _localizer["Güzellik Merkezlerine Geri Dön"];
        //    ViewData["Title7"] = _localizer["Merkez Resmi"];
        //    if (id.ToString() == null)
        //    {
        //        return NotFound();
        //    }

        //    var guzellikMerkezi = await _context.Tesisler.FindAsync(id);
        //    if (guzellikMerkezi == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(guzellikMerkezi);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> MerkezEdit(int id, [Bind("TesisId,TesisAdi,TesisAdresi,TesisResmi,TesisDegerlendirmesi,TesisPuani")] SporTesisi guzellikMerkezi)
        //{
        //    if (id != guzellikMerkezi.TesisId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(guzellikMerkezi);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!GuzellikMerkeziExists(guzellikMerkezi.TesisId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(MerkezIndex));
        //    }
        //    return View(guzellikMerkezi);
        //}
        //// GET: GuzellikMerkezi/Delete/5
        //public async Task<IActionResult> MerkezDelete(int id)
        //{
        //    ViewData["Title"] = _localizer["Güzellik Merkezini Sil"];
        //    ViewData["Title2"] = _localizer["Güzellik Merkezi Adı"];
        //    ViewData["Title3"] = _localizer["Adresi"];
        //    ViewData["Title4"] = _localizer["Email Adresi"];
        //    ViewData["Title6"] = _localizer["Sil"];
        //    ViewData["Title8"] = _localizer["Güzellik Merkezlerine Geri Dön"];
        //    if (id.ToString() == null)
        //    {
        //        return NotFound();
        //    }

        //    var guzellikMerkezi = await _context.Tesisler
        //        .FirstOrDefaultAsync(m => m.TesisId == id);
        //    if (guzellikMerkezi == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(guzellikMerkezi);
        //}
        //// POST: GuzellikMerkezi/Delete/5
        //[HttpPost, ActionName("MerkezDelete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> MerkezDeleteConfirmed(int id)
        //{
        //    var guzellikMerkezi = await _context.Tesisler.FindAsync(id);
        //    _context.Tesisler.Remove(guzellikMerkezi);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(MerkezIndex));
        //}

        //private bool GuzellikMerkeziExists(int id)
        //{
        //    return _context.Tesisler.Any(e => e.TesisId == id);
        //}








    }
}
