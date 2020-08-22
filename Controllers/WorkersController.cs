using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityProblemSolver.DatabaseContext;
using CityProblemSolver.Models;
using CityProblemSolver.ViewModels;
//using CityProblemSolver.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityProblemSolver.Controllers
{
    public class WorkersController : Controller
    {
        private readonly CityProblemSolverDBContext _context;

        public WorkersController(CityProblemSolverDBContext context)
        {
            _context = context;
        }

        // GET: Workers/Dashboard
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserWorker") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Worker", "Account");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserWorker");
            return RedirectToAction("Worker", "Account");
        }

        // GET: Workers/Create
        public IActionResult Create()
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

        // POST: Workers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Phone,NID,Address,UserName,Password,ConfirmPassword")] Register register)
        {
            if (ModelState.IsValid)
            {
                // Username is already Exist
                if (IsUserNameExist(register.UserName))
                {
                    ModelState.AddModelError("UserName", "User name already exist");
                    return View(register);
                }
                User _user = new User();
                _user.Name = register.Name;
                _user.Phone = register.Phone;
                _user.NID = register.NID;
                _user.Address = register.Address;
                _user.UserName = register.UserName;
                _user.Password = register.Password;
                _user.UserType = "Worker";
                _context.Add(_user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Workers));
            }
            return View(register);
        }

        // GET: Workers
        public async Task<IActionResult> Workers()
        {
            if (HttpContext.Session.GetString("UserAdmin") != null)
            {
                return View(await _context.Users.Where(m => m.UserType == "Worker").ToListAsync());
            }
            else
            {
                return RedirectToAction("Admin", "Account");
            }
        }

        // GET: Workers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            
            if (HttpContext.Session.GetString("UserAdmin") != null)
            {
                Register _register = new Register();
                _register.Name = user.Name;
                _register.Phone = user.Phone;
                _register.NID = user.NID;
                _register.Address = user.Address;
                _register.UserName = user.UserName;
                _register.Password = user.Password;
                
                return View(_register);
            }
            else
            {
                return RedirectToAction("Admin", "Account");
            }
        }

        // POST: Workers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Phone,NID,Address,UserName,Password,ConfirmPassword")] Register register)
        {
            if (id != register.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    User _user = new User();
                    _user.ID = register.ID;
                    _user.Name = register.Name;
                    _user.Phone = register.Phone;
                    _user.NID = register.NID;
                    _user.Address = register.Address;
                    _user.UserName = register.UserName;
                    _user.Password = register.Password;
                    _user.UserType = "Worker";

                    _context.Update(_user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(register.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Workers));
            }
            return View(register);
        }

        // POST: Workers/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Workers));
        }

        public bool IsUserNameExist(string userName)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
