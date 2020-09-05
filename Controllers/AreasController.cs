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
    public class AreasController : Controller
    {
        private readonly CityProblemSolverDBContext _context;

        public AreasController(CityProblemSolverDBContext context)
        {
            _context = context;
        }

        // GET: Areas/Create
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

        // POST: Areas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,RoadNo")] AreaViewModel area)
        {
            if (ModelState.IsValid)
            {
                Area _area = new Area();
                _area.Name = area.Name;
                _area.RoadNo = area.RoadNo;

                _context.Add(_area);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(area);
        }

        // GET: Areas
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserAdmin") != null)
            {
                return View(await _context.Areas.ToListAsync());
            }
            else
            {
                return RedirectToAction("Admin", "Account");
            }
        }

        // GET: Areas/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var area = await _context.Areas
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (area == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(area);
        //}

        // GET: Areas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _context.Areas.FindAsync(id);
            if (area == null)
            {
                return NotFound();
            }

            if (HttpContext.Session.GetString("UserAdmin") != null)
            {
                AreaViewModel _area = new AreaViewModel();
                _area.ID = area.ID;
                _area.Name = area.Name;
                _area.RoadNo = area.RoadNo;

                return View(_area);
            }
            else
            {
                return RedirectToAction("Admin", "Account");
            }
        }

        // POST: Areas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,RoadNo")] AreaViewModel area)
        {
            if (id != area.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Area _area = new Area();
                    _area.ID = area.ID;
                    _area.Name = area.Name;
                    _area.RoadNo = area.RoadNo;

                    _context.Update(_area);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AreaExists(area.ID))
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
            return View(area);
        }

        // POST: Areas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var area = await _context.Areas.FindAsync(id);
            _context.Areas.Remove(area);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AreaExists(int id)
        {
            return _context.Areas.Any(e => e.ID == id);
        }
    }
}
