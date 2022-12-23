using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using HaliSaha_Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Hali_Saha.Controllers
{
    public class RandevuController : Controller
    {
        private readonly DbHaliSahaContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RandevuController> _logger;
        // private readonly IStringLocalizer<RandevuController> _localizer;

        public RandevuController(DbHaliSahaContext context, UserManager<AppUser> userManager, ILogger<RandevuController> logger, IStringLocalizer<RandevuController> localizer)
        {
            //_localizer = localizer;
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        /*[HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) }
            );

            return LocalRedirect(returnUrl);
        }*/
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var liste = _context.Randevular.Include(r => r.sporTesisi).Include(r => r.kullanici).Where(r => r.kullanici.KullaniciId.ToString() == userId);
            if (liste == null)
            {
                return NotFound();
            }
            return View(await liste.ToListAsync());
        }
        [AllowAnonymous]
        // GET: Randevu/Details/5
        public async Task<IActionResult> Details(string merkezId, string uyeId)
        {
            if (merkezId == null && uyeId == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular
                .Include(r => r.sporTesisi)
                .Include(r => r.kullanici)
                .FirstOrDefaultAsync(m => m.TesisId.ToString() == merkezId && m.KullaniciId.ToString() == uyeId);
            if (randevu == null)
            {
                return NotFound();
            }
            ViewBag.gunu = randevu.randevuTarihi;
            return View(randevu);
        }
        [Authorize]
        // GET: Randevu/Create
        public IActionResult Create()
        {
            ViewData["merkezId"] = new SelectList(_context.Tesisler, "merkezId", "merkezId");
            ViewData["uyeId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["Email"] = new SelectList(_context.Users, "Email", "Email");
            ViewData["merkezAdi"] = new SelectList(_context.Tesisler, "merkezAdi", "merkezAdi");
            ViewData["uyeAdi"] = new SelectList(_context.Users, "uyeAdi", "uyeAdi");
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.userId = userId;
            return View();
        }

        // POST: Randevu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("uyeId,merkezId,randevuDegerlendirmesi,randevuPuani,randevuSaati,randevuTuru")] Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["merkezId"] = new SelectList(_context.Tesisler, "merkezId", "merkezId", randevu.TesisId);
            ViewData["uyeId"] = new SelectList(_context.Users, "Id", "Id", randevu.KullaniciId);
            return View(randevu);
        }
        [Authorize]
        // GET: Randevu/Edit/5
        public async Task<IActionResult> Edit(string merkezId, string uyeId)
        {
            if (merkezId == null && uyeId == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular.FindAsync(merkezId, uyeId);
            if (randevu == null)
            {
                return NotFound();
            }
            ViewBag.gun = randevu.randevuTarihi;
            ViewData["merkezId"] = new SelectList(_context.Tesisler, "merkezId", "merkezId", randevu.TesisId);
            ViewData["uyeId"] = new SelectList(_context.Users, "Id", "Id", randevu.KullaniciId);
            return View(randevu);
        }

        // POST: Randevu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string merkezId, string uyeId, [Bind("uyeId,merkezId,randevuDegerlendirmesi,randevuPuani,randevuSaati,randevuTuru")] Randevu randevu)
        {
            if (merkezId != randevu.TesisId.ToString() && uyeId != randevu.KullaniciId.ToString())
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(randevu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RandevuExists(randevu.KullaniciId.ToString(), randevu.KullaniciId.ToString()))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["merkezId"] = new SelectList(_context.Tesisler, "merkezId", "merkezId", randevu.TesisId);
            ViewData["uyeId"] = new SelectList(_context.Users, "Id", "Id", randevu.KullaniciId);
            return View(randevu);
        }
        [Authorize]
        // GET: Randevu/Delete/5
        public async Task<IActionResult> Delete(string merkezId, string uyeId)
        {
            if (merkezId == null && uyeId == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular
                .Include(r => r.sporTesisi)
                .Include(r => r.kullanici)
                .FirstOrDefaultAsync(m => m.KullaniciId.ToString() == merkezId && m.KullaniciId.ToString() == uyeId);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // POST: Randevu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string merkezId, string uyeId)
        {
            var randevu = await _context.Randevular.FindAsync(merkezId, uyeId);
            _context.Randevular.Remove(randevu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RandevuExists(string merkezId, string uyeId)
        {
            return _context.Randevular.Any(e => e.TesisId.ToString() == merkezId && e.KullaniciId.ToString() == uyeId);
        }
    }
}
