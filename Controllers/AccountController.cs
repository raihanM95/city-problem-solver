﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CityProblemSolver.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult User()
        {
            return View();
        }
    }
}
