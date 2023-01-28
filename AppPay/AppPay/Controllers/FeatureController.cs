using AppPay.DAL;
using AppPay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppPay.Controllers
{
    public class FeatureController : Controller
    {
        private readonly AppDbContext _context;

        public FeatureController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<FeaturesModel> features = await _context.FeaturesModels.ToListAsync();
            return View(features);
        }
    }
}
