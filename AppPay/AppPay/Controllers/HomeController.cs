using AppPay.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppPay.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }

    }
}