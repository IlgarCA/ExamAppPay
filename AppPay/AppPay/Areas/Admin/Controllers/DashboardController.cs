using AppPay.DAL;
using AppPay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppPay.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DashboardController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<FeaturesModel> features = await _context.FeaturesModels.ToListAsync();
            return View(features);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FeaturesModel featuresModel)
        {
            if (!ModelState.IsValid) { return View(); }

            bool isexistTitle = await _context.FeaturesModels.AnyAsync(c => c.Title.ToLower().Trim() == featuresModel.Title.ToLower().Trim());
            bool isexistDescription = await _context.FeaturesModels.AnyAsync(c =>c.Description.ToLower().Trim() == featuresModel.Description.ToLower().Trim());
            bool isexistIcon = await _context.FeaturesModels.AnyAsync(c =>c.Icon.ToLower().Trim() == featuresModel.Icon.ToLower().Trim());

            if (isexistTitle) { ModelState.AddModelError("Title", "already exist"); return View(); }
            if (isexistDescription) { ModelState.AddModelError("Description", "already exist"); return View(); }

 
            await _context.FeaturesModels.AddAsync(featuresModel);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            FeaturesModel? featuresModel = await _context.FeaturesModels.FindAsync(id);
            if(featuresModel == null) { return View(); }

            return View(featuresModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(FeaturesModel featuresModel)
        {
            FeaturesModel? editFeaturesModel = await _context.FeaturesModels.FindAsync(featuresModel.Id);
            if(featuresModel == null) { return View(); }
            if (!ModelState.IsValid) { return View(featuresModel); }

            editFeaturesModel.Title = featuresModel.Title;
            editFeaturesModel.Description = featuresModel.Description;
            editFeaturesModel.Icon = featuresModel.Icon;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            FeaturesModel? featuresModel = _context.FeaturesModels.Find(id);
            if (!ModelState.IsValid) { return View(); }

            _context.FeaturesModels.Remove(featuresModel);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
