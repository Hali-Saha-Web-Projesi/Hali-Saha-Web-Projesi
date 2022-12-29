using DataAccess.Data;
using HaliSaha_Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Data;
using System.Security.Claims;

namespace Hali_Saha.Controllers
{
    [Authorize]
    public class RandevuController : Controller
    {
        private readonly DbHaliSahaContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RandevuController> _logger;
        private readonly IStringLocalizer<RandevuController> _localizer;


        public RandevuController(DbHaliSahaContext context, ILogger<RandevuController> logger, IStringLocalizer<RandevuController> localizer, UserManager<AppUser> userManager)
        {
            _context = context;
            _logger = logger;
            _localizer = localizer;
            _userManager = userManager;
            
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
       
        public async Task<IActionResult> Index() //tesis İndex aslında
        {
            ViewData["Title"] = _localizer["Randevu Al"];
            ViewData["Title2"] = _localizer["Randevularım"];

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var liste = _context.Randevular.Include(r => r.Kullanici).Where(r => r.kullaniciId.ToString() == userId);
            var liste = _context.Randevular.Where(r => r.kullanici_Id.ToString() == userId);
            if (liste == null)
            {
                return NotFound();
            }
            return View(await liste.ToListAsync());
        }

        // GET: GuzellikMerkezi/Create
        [HttpGet]
        public IActionResult Create()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.userId = userId;

            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("kullanici_Id,TesisAdi,randevuSaati")] Randevu randevu)
        //{

        //    ViewData["Title"] = _localizer["Randevu Al"];
        //    ViewData["Title9"] = _localizer["Kullanıcı Adı"];
        //    ViewData["Title10"] = _localizer["Tesis Adı"];
        //    ViewData["Title2"] = _localizer["Randevu Tarihi"];
        //    ViewData["Title3"] = _localizer["Kaydet"];
        //    ViewData["Title8"] = _localizer["Randevularıma Geri Dön"];

        //    //if (ModelState.IsValid)
        //    //{
        //    _context.Add(randevu);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //    //}
        //    //return View(randevu);
        //}

        [HttpPost]
        public IActionResult Create(Randevu model)
        {
            if (ModelState.IsValid)
            {
                Randevu Randevu = new()
                {
                    randevuId = model.randevuId,
                    kullanici_Id = model.kullanici_Id,
                    TesisAdi = model.TesisAdi,
                    randevuSaati = model.randevuSaati
                };
                _context.Randevular.Add(Randevu);
                int affectedRowCount = _context.SaveChanges();


                if (affectedRowCount == 0)
                {
                    ModelState.AddModelError("", "randevu eklenemedi");
                }
                else
                {
                    return RedirectToAction(nameof(Index));

                }
            }
            return View(model);

        }









        //public async Task<IActionResult> Details(int id)
        //{
        //    ViewData["Title"] = _localizer["Randevularının Detaylarını Gör"];
        //    ViewData["Title2"] = _localizer["Spor Tesisi Adı"];
        //    ViewData["Title3"] = _localizer["Randevu Tarihi"];
        //    ViewData["Title6"] = _localizer["Düzenle"];
        //    ViewData["Title8"] = _localizer["Randevularıma Geri Dön"];

        //    if (id.ToString() == null)
        //    {
        //        return NotFound();
        //    }

        //    var randevu = await _context.Randevular
        //        .FirstOrDefaultAsync(m => m.randevuId == id);

        //    if (randevu == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(randevu);
        //}

        //public async Task<IActionResult> Edit(int id)
        //{
        //    ViewData["Title"] = _localizer["Randevumu Düzenle"];
        //    ViewData["Title2"] = _localizer["Spor Tesisi Adı"];
        //    ViewData["Title3"] = _localizer["Randevu Tarihi"];
        //    ViewData["Title5"] = _localizer["Kaydet"];
        //    ViewData["Title6"] = _localizer["Randevularıma Geri Dön"];

        //    if (id.ToString() == null)
        //    {
        //        return NotFound();
        //    }

        //    var randevu = await _context.Randevular.FindAsync(id);

        //    if (randevu == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(randevu);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind(nameof(Randevu.randevuId), nameof(Randevu.KullaniciId), nameof(Randevu.TesisAdi), nameof(Randevu.randevuSaati))] Randevu randevu)
        //{
        //    if (id != randevu.randevuId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(randevu
        //                );
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!SporTesisiExists(randevu.randevuId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(randevu);
        //}

        //// GET: GuzellikMerkezi/Delete/5
        //public async Task<IActionResult> Delete(int id)
        //{
        //    ViewData["Title"] = _localizer["Randevuyu Sil"];
        //    ViewData["Title2"] = _localizer["Spor Tesisi Adı"];
        //    ViewData["Title3"] = _localizer["Randevu Tarihi"];
        //    ViewData["Title6"] = _localizer["Sil"];
        //    ViewData["Title8"] = _localizer["Randevularıma Geri Dön"];

        //    if (id.ToString() == null)
        //    {
        //        return NotFound();
        //    }

        //    var randevu = await _context.Randevular
        //        .FirstOrDefaultAsync(m => m.randevuId == id);
        //    if (randevu == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(randevu);
        //}

        //// POST: GuzellikMerkezi/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var randevu = await _context.Randevular.FindAsync(id);
        //    _context.Randevular.Remove(randevu);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool SporTesisiExists(int id)
        //{
        //    return _context.Randevular.Any(e => e.randevuId == id);
        //}
    }
}
