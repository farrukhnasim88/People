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
    public class ChildrenController : Controller
    {
        private readonly PeopleContext _context;

        public ChildrenController(PeopleContext context)
        {
            _context = context;
        }


        public IActionResult Kids(int id)
        {
            List<Children> childrens = new List<Children>();
             childrens = _context.Children.Where(k => k.PersonId  == id).ToList();

            return View(childrens);


        }


        public IActionResult Family(int id, int sid)
        {
            List<Person> persons = new List<Person>();
            persons = _context.Persons.Where(o => o.SpouseId == id).ToList();
            List<Children> childrens = new List<Children>();
            childrens= _context.Children.Where(c => c.PersonId==id ).ToList();
            return View("~/Views/Children/index.cshtml", childrens);
            
        }


        // GET: Children
        public async Task<IActionResult> Index()
        {
            return View(await _context.Children.ToListAsync());
        }

        // GET: Children/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var children = await _context.Children
                .FirstOrDefaultAsync(m => m.ChildrenId == id);
            if (children == null)
            {
                return NotFound();
            }

            return View(children);
        }

        // GET: Children/Create
        public IActionResult Create(int id)
        {
            ViewBag.id = id;
            return View();
        }

        // POST: Children/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChildrenId,FirstName,LastName,Age,Gender,IsAlive,PersonId")] Children children)
        {
            if (ModelState.IsValid)
            {
                _context.Add(children);
                await _context.SaveChangesAsync();
                // return RedirectToAction(nameof(Index));
                return RedirectToAction("MarriedCouples", "People");
            }
            return View(children);
        }

        // GET: Children/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var children = await _context.Children.FindAsync(id);
            if (children == null)
            {
                return NotFound();
            }
            return View(children);
        }

        // POST: Children/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChildrenId,FirstName,LastName,Age,Gender,IsAlive")] Children children)
        {
            if (id != children.ChildrenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(children);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChildrenExists(children.ChildrenId))
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
            return View(children);
        }

        // GET: Children/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var children = await _context.Children
                .FirstOrDefaultAsync(m => m.ChildrenId == id);
            if (children == null)
            {
                return NotFound();
            }

            return View(children);
        }

        // POST: Children/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var children = await _context.Children.FindAsync(id);
            _context.Children.Remove(children);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChildrenExists(int id)
        {
            return _context.Children.Any(e => e.ChildrenId == id);
        }
    }
}
