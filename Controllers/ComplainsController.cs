using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CityProblemSolver.DatabaseContext;
using CityProblemSolver.Models;
using Microsoft.AspNetCore.Http;
using CityProblemSolver.ViewModels;

namespace CityProblemSolver.Controllers
{
    public class ComplainsController : Controller
    {
        private readonly CityProblemSolverDBContext _context;

        public ComplainsController(CityProblemSolverDBContext context)
        {
            _context = context;
        }

        // GET: Complains/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("User") != null)
            {
                ViewData["AreaId"] = new SelectList(_context.Areas, "ID", "Name");
                return View();
            }
            else
            {
                return RedirectToAction("User", "Account");
            }
        }

        // POST: Complains/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AreaId,ComplainDate,ComplainDescription")] ComplainViewModel complain)
        {
            //ModelState["Status"].Errors.Clear();
            
            if (ModelState.IsValid)
            {
                Complain _complain = new Complain();
                _complain.AreaId = complain.AreaId;
                _complain.ComplainDate = complain.ComplainDate;
                _complain.ComplainDescription = complain.ComplainDescription;
                _complain.Status = "Pending";

                _context.Add(_complain);
                await _context.SaveChangesAsync();

                var user = HttpContext.Session.GetString("User").ToString();
                UserComplain _userComplain = new UserComplain();
                User aUser = _context.Users.Where(u => u.UserName == user).SingleOrDefault();
                _userComplain.ComplainId = _complain.ID;
                _userComplain.UserId = aUser.ID;
                _context.UserComplains.Add(_userComplain);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["AreaId"] = new SelectList(_context.Areas, "ID", "Name", complain.AreaId);
            return View(complain);
        }

        // GET: Complains
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("User") != null)
            {
                var user = HttpContext.Session.GetString("User").ToString();
                //UserComplain _userComplain = new UserComplain();
                var aUser = _context.Users.Where(u => u.UserName == user).SingleOrDefault();
                var aUserComplain = _context.UserComplains.Where(c => c.UserId == aUser.ID).ToList();

                List<Complain> complainList = new List<Complain>();

                foreach (var complain in aUserComplain)
                {
                    if (complainList.Count > 0)
                    {
                        if (complainList.Where(c => c.ID == complain.ComplainId).ToList().Count == 0)
                        {
                            complainList.AddRange(_context.Complains.Include(c => c.Area).Where(c => c.ID == complain.ComplainId));
                        }
                    }
                    else
                    {
                        complainList.AddRange(_context.Complains.Include(c => c.Area).Where(c => c.ID == complain.ComplainId));
                    }
                }

                //var cityProblemSolverDBContext = _context.Complains.Include(c => c.Area);
                //return View(await cityProblemSolverDBContext.ToListAsync());
                return View(complainList);
            }
            else
            {
                return RedirectToAction("User", "Account");
            }
        }

        // GET: Complains/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complain = await _context.Complains
                .Include(c => c.Area)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (complain == null)
            {
                return NotFound();
            }

            if (HttpContext.Session.GetString("User") != null)
            {
                return View(complain);
            }
            else
            {
                return RedirectToAction("User", "Account");
            }
        }

        // GET: Complains/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complain = await _context.Complains.FindAsync(id);
            if (complain == null)
            {
                return NotFound();
            }

            if (HttpContext.Session.GetString("User") != null)
            {
                ComplainViewModel _complain = new ComplainViewModel();
                _complain.ID = complain.ID;
                _complain.AreaId = complain.AreaId;
                _complain.ComplainDate = complain.ComplainDate;
                _complain.ComplainDescription = complain.ComplainDescription;

                ViewData["AreaId"] = new SelectList(_context.Areas, "ID", "Name", complain.AreaId);
                return View(_complain);
            }
            else
            {
                return RedirectToAction("User", "Account");
            }
        }

        // POST: Complains/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AreaId,ComplainDate,ComplainDescription")] ComplainViewModel complain)
        {
            if (id != complain.ID)
            {
                return NotFound();
            }
            //ModelState["ComplainDate"].Errors.Clear();
            
            if (ModelState.IsValid)
            {
                try
                {
                    Complain _complain = new Complain();
                    _complain.ID = complain.ID;
                    _complain.AreaId = complain.AreaId;
                    _complain.ComplainDate = complain.ComplainDate;
                    _complain.ComplainDescription = complain.ComplainDescription;
                    _complain.Status = "Pending";

                    _context.Update(_complain);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplainExists(complain.ID))
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
            ViewData["AreaId"] = new SelectList(_context.Areas, "ID", "Name", complain.AreaId);
            return View(complain);
        }

        // POST: Complains/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var complain = await _context.Complains.FindAsync(id);
            _context.Complains.Remove(complain);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // For Admin
        // GET: Complains/Complains
        public async Task<IActionResult> Complains()
        {
            if (HttpContext.Session.GetString("UserAdmin") != null)
            {
                var cityProblemSolverDBContext = _context.Complains.Include(c => c.Area);
                ComplainViewModel complainViewModel = new ComplainViewModel
                {
                    Complains = await cityProblemSolverDBContext.ToListAsync()
                };
                ViewData["WorkerId"] = new SelectList(_context.Users.Where(t => t.UserType == "Worker"), "ID", "Name");
                return View(complainViewModel);
            }
            else
            {
                return RedirectToAction("Admin", "Account");
            }
        }

        // GET: Complains/Detailss/5
        public async Task<IActionResult> Detailss(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complain = await _context.Complains.Include(c => c.Area).FirstOrDefaultAsync(m => m.ID == id);
            var userComplain = await _context.UserComplains.Include(c => c.User).FirstOrDefaultAsync(m => m.ComplainId == id);
            var workerComplain = await _context.WorkerComplains.Include(c => c.User).FirstOrDefaultAsync(m => m.ComplainId == id);

            ComplainViewModel complainViewModel = new ComplainViewModel
            {
                Complain = complain,
                UserComplain = userComplain,
                WorkerComplain = workerComplain
            };

            if (complainViewModel == null)
            {
                return NotFound();
            }

            if (HttpContext.Session.GetString("UserAdmin") != null)
            {
                return View(complainViewModel);
            }
            else
            {
                return RedirectToAction("Admin", "Account");
            }
        }

        // POST: Complains/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign([Bind("UserId,ComplainId")] WorkerComplain workerComplain)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workerComplain);
                await _context.SaveChangesAsync();

                Complain _complain = new Complain();
                _complain.Status = "Working";
                _context.Update(_complain);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Complains));
            }
            ViewData["WorkerId"] = new SelectList(_context.Users.Where(t => t.UserType == "Worker"), "ID", "Name");
            return View(workerComplain);
        }

        private bool ComplainExists(int id)
        {
            return _context.Complains.Any(e => e.ID == id);
        }
    }
}
