using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CityProblemSolver.Controllers
{
    //[Route("api/[controller]")]
    public class AccountController : Controller
    {
        [Route("c/access")]
        public IActionResult Admin()
        {
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
