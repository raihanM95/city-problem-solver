using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityProblemSolver.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserAdmin") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Admin", "Account");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserAdmin");
            return RedirectToAction("Admin", "Account");
        }
    }
}
