using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityProblemSolver.DatabaseContext;
using CityProblemSolver.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityProblemSolver.Controllers
{
    //[Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly CityProblemSolverDBContext _context;

        public AccountController(CityProblemSolverDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("c/access")]
        public IActionResult Admin()
        {
            if(HttpContext.Session.GetString("UserAdmin") != null)
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [Route("c/access")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Admin([Bind("UserName,Password")] Login login)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                .SingleOrDefaultAsync(m => m.UserName == login.UserName && m.Password == login.Password && m.UserType == "Admin");
                if (user == null)
                {
                    ModelState.AddModelError("Password", "Invalid login attempt!");
                    return View();
                }
                else
                {
                    HttpContext.Session.SetString("UserAdmin", user.UserName);
                    return RedirectToAction("Dashboard", "Admin");
                }
            }
            return View();
        }

        public IActionResult User()
        {
            return View();
        }

        public IActionResult Worker()
        {
            return View();
        }
    }
}
