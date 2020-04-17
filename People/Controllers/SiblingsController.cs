using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using People.Data;
using People.Models;

namespace People.Controllers
{
    public class SiblingsController : Controller
    {
        private readonly PeopleContext _context;

        public SiblingsController(PeopleContext context)
        {
            _context = context;
        }



        public IActionResult Siblings(int id)
        {
            List<Sibling> siblings = new List<Sibling>();
            siblings = _context.Siblings.Where(k => k.PersonId == id).ToList();

            return View(siblings);


        }



        // GET: Siblings
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.id = id;
            return View(await _context.Siblings.ToListAsync());
        }

        // GET: Siblings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sibling = await _context.Siblings
                .FirstOrDefaultAsync(m => m.SiblingId == id);
            if (sibling == null)
            {
                return NotFound();
            }

            return View(sibling);
        }

        // GET: Siblings/Create
        public IActionResult Create(int id)
        {
            ViewBag.id = id;
            return View();
        }

        // POST: Siblings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SiblingId,FirstName,LastName,Age,Gender,IsAlive,PersonId")] Sibling sibling)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(sibling);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "People");
            }
            return View(sibling);

            
        }

        // GET: Siblings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sibling = await _context.Siblings.FindAsync(id);
            if (sibling == null)
            {
                return NotFound();
            }
            return View(sibling);
        }

        // POST: Siblings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SiblingId,FirstName,LastName,Age,Gender,IsAlive,PersonId")] Sibling sibling)
        {
            if (id != sibling.SiblingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sibling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiblingExists(sibling.SiblingId))
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
            return View(sibling);
        }

        // GET: Siblings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sibling = await _context.Siblings
                .FirstOrDefaultAsync(m => m.SiblingId == id);
            if (sibling == null)
            {
                return NotFound();
            }

            return View(sibling);
        }

        // POST: Siblings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sibling = await _context.Siblings.FindAsync(id);
            _context.Siblings.Remove(sibling);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiblingExists(int id)
        {
            return _context.Siblings.Any(e => e.SiblingId == id);
        }
    }
}
