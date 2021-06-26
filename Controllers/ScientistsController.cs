using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using scientist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace scientist.Controllers
{
    public class ScientistsController : Controller
    {
        private readonly ApplicationContext _context;

        public ScientistsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Scientists
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page??1;
            var pageSize = 20;
            var onePageOfScients = _context.Scientist.ToList().OrderByDescending(i=>i.Id).ToPagedList(pageNumber, pageSize);
            return View(onePageOfScients);
        }

        // GET: Scientists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scientist = await _context.Scientist
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scientist == null)
            {
                return NotFound();
            }

            return View(scientist);
        }

        // GET: Scientists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Scientists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Scientist scientist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scientist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(scientist);
        }

        // GET: Scientists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scientist = await _context.Scientist.FindAsync(id);
            if (scientist == null)
            {
                return NotFound();
            }
            return View(scientist);
        }

        // POST: Scientists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Scientist scientist)
        {
            if (id != scientist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scientist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScientistExists(scientist.Id))
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
            return View(scientist);
        }

        // GET: Scientists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scientist = await _context.Scientist
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scientist == null)
            {
                return NotFound();
            }

            return View(scientist);
        }

        // POST: Scientists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scientist = await _context.Scientist.FindAsync(id);
            _context.Scientist.Remove(scientist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScientistExists(int id)
        {
            return _context.Scientist.Any(e => e.Id == id);
        }


        public async Task<IActionResult> Search(string FullName,string Citizenship,string Profession,DateTime? StartDate,DateTime? EndDate,string Workplace,string Livingplace,string Degree)
        {
            var scientists = new List<Scientist>();
            var predicate = PredicateBuilder.True<Scientist>();
            if (StartDate != null && EndDate == null)
            {
                EndDate = DateTime.Now;
            }
            if (!string.IsNullOrWhiteSpace(FullName))
            {
                FullName = FullName.ToLower();
            }
            if (!string.IsNullOrWhiteSpace(Citizenship))
            {
                Citizenship = Citizenship.ToLower();
            }
            if (!string.IsNullOrWhiteSpace(Profession))
            {
                Profession = Profession.ToLower();
            }
            if (!string.IsNullOrWhiteSpace(Workplace))
            {
                Workplace = Workplace.ToLower();
            }
            if (!string.IsNullOrWhiteSpace(Livingplace))
            {
                Livingplace = Livingplace.ToLower();
            }
            if (!string.IsNullOrWhiteSpace(Degree))
            {
                Degree = Degree.ToLower();
            }
            

            if (!string.IsNullOrEmpty(FullName))
                predicate = predicate.And(i => i.FullName.Contains(FullName));
            if (!string.IsNullOrEmpty(Citizenship))
                predicate = predicate.And(i => i.Cityzenship.Contains(Citizenship));
            if (!string.IsNullOrEmpty(Profession))
                predicate = predicate.And(i => i.Profession.Contains(Profession));
            if (!string.IsNullOrEmpty(Workplace))
                predicate = predicate.And(i => i.Workplace.Contains(Workplace));
            if (!string.IsNullOrEmpty(Livingplace))
                predicate = predicate.And(i => i.Livingplace.Contains(Livingplace));
            if (!string.IsNullOrEmpty(Degree))
                predicate = predicate.And(i => i.Degree.Contains(Degree));
            if (StartDate != null && EndDate != null)
            {
                predicate = predicate.And(i => i.Birthday >= StartDate && i.Birthday <= EndDate);
            }

            scientists = _context.Scientist.Where(predicate).ToList();

            return  View(scientists);


        }
    }
}
