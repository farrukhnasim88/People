using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using People.Data;
using People.Models;
using People.Controllers;


namespace People.Controllers
{
    public class PeopleController : BaseController
    {
        private readonly PeopleContext _context;

        public PeopleController(PeopleContext context)
        {
            _context = context;
        }


        public IActionResult GetFamilyTree(int id)
        {
           
                Person person = _context.Persons.Find(id);
                string fullName = person.FirstName + " " + person.LastName;
                ViewBag.person = fullName;
               
                

                List<Children> children = _context.Children.Where(o => o.PersonId == id).ToList();
                if (children == null)
                {
                    children = new List<Children>();
                }
                List<Sibling> siblings = _context.Siblings.Where(o => o.PersonId == id).ToList();
                if (siblings == null)
                {
                    siblings = new List<Sibling>();
                }
                ViewBag.Children = children;
                ViewBag.Siblings = siblings;
                return View("~/Views/FamilyTree/FamilyTreeView.cshtml");
            
           
            
        }

        


        public IActionResult Family (string SearchString)
        {
          
            var people = from m in _context.Persons select m;

            if (!String.IsNullOrEmpty(SearchString))
            {
                people = people.Where(s => s.FirstName.Contains(SearchString));

            }
            
            
                return View(people.ToList());
            

        }

        public async Task<IActionResult> Detail(int? id, string spousename , string firstname)
        {
            ViewBag.spousename = spousename;

            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .Include(s => s.Children)
          // .ThenInclude(e => e.Siblings)
        .AsNoTracking()

                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }



        // GET: People/Edit/5
        public async Task<IActionResult> Edits(int? id, string spousename, string firstname)
        {
            ViewBag.name = spousename;
            ViewBag.firstname = firstname;
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }


        public IActionResult MarriedCouples()
        {
            List<Person> people = new List<Person>();
             people = _context.Persons.Where(o => o.SpouseId >0).ToList();
            List<PersonViewModels> personViews = new List<PersonViewModels>();
           
            //Comments
            foreach (Person item in people)
            {
                PersonViewModels pvm = new PersonViewModels();
                pvm.PresonId = item.PersonId;
                pvm.FirstName = item.FirstName;
                pvm.LastName = item.LastName;
                pvm.Age = item.Age;
                pvm.Gender = item.Gender;
                pvm.IsAlive = item.IsAlive;
                Person person = _context.Persons.FirstOrDefault(o => o.PersonId == item.SpouseId);
                pvm.SpouseName = person.FirstName + " " + person.LastName;

                personViews.Add(pvm);

            }

            return View("~/Views/People/Spouse.cshtml", personViews.ToList());
        }

        public IActionResult Confirmed(int id1, int id2)
        {
            Person firstPerson = _context.Persons.Find(id1);
            Person secondPerson = _context.Persons.Find(id2);
            firstPerson.SpouseId = secondPerson.PersonId;
            secondPerson.SpouseId = firstPerson.PersonId;
             _context.SaveChanges();

            List<Person> people =  _context.Persons.ToList();
            List<PersonViewModels> personViews = new List<PersonViewModels>();

            foreach (Person item in people)
            {
                PersonViewModels pvm = new PersonViewModels();
                pvm.PresonId = item.PersonId;
                pvm.FirstName = item.FirstName;
                pvm.LastName = item.LastName;
                pvm.Age = item.Age;
                pvm.Gender = item.Gender;
                pvm.IsAlive = item.IsAlive;
                Person person = _context.Persons.FirstOrDefault(o => o.PersonId == item.SpouseId);
                pvm.SpouseName = person.FirstName + " " + person.LastName;

                personViews.Add(pvm);

            }



            return View("~/Views/People/Spouse.cshtml", personViews.ToList());
        }



        public IActionResult Marry(string fName, int id, string gender, int spouseId)
        {
            ViewBag.id = id;
            ViewBag.FirstName = fName;

            List<Person> oppGenderPersonUnMarried = new List<Person>();
            if (gender != null && gender.ToLower() == "male")
            {
                oppGenderPersonUnMarried = _context.Persons.Where(o => o.IsAlive == true && o.SpouseId == 0 && o.Gender == "Female").ToList();

            }

            else
            {
                oppGenderPersonUnMarried = _context.Persons.Where(o => o.IsAlive == true && o.SpouseId == 0 && o.Gender == "male").ToList();
            }


            return View(oppGenderPersonUnMarried);
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            List<Person> people = new List<Person>();
             people= await _context.Persons.Where (o => o.SpouseId == 0).ToListAsync();

            return View(people);
           // return View(await _context.Persons.ToListAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FirstName,LastName,Age,Gender,IsAlive")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId,FirstName,LastName,Age,Gender,IsAlive")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
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
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ShowAllDetails(int personid)
        {
            var a = _context.Persons.Where(o => o.PersonId == personid).Include(o => o.Children).Include(o => o.Siblings).FirstOrDefault();
            ViewBag.AllDetails = a;
            return View("~/Views/FamilyTree/AllDetailsView.cshtml");

        }

        private bool PersonExists(int id)
        {
            return _context.Persons.Any(e => e.PersonId == id);
        }
    }


}