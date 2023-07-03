using ASPNETMVCCRUD.Data;
using ASPNETMVCCRUD.Models;
using ASPNETMVCCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVCCRUD.Controllers
{
    public class PersonsController : Controller
    {
        private readonly MVCDbContext mvcDbContext;

        public PersonsController(MVCDbContext mvcDbContext)
        {
            this.mvcDbContext = mvcDbContext;   
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await mvcDbContext.Persons.ToListAsync();
            var persons = await mvcDbContext.Persons.ToListAsync();
            return View(persons);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPersonViewModel addPersonRequest)
        {
            var person = new Person()
            {
                Id = Guid.NewGuid(),
                Name = addPersonRequest.Name,
                Email = addPersonRequest.Email,
                DateOfBirth = addPersonRequest.DateOfBirth,
            };
            await mvcDbContext.Persons.AddAsync(person);
            await mvcDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var person = await mvcDbContext.Persons.FirstOrDefaultAsync(x => x.Id == id);
            if(person != null)
            {
                var viewModel = new UpdatePersonViewModel()
                {
                    Id = person.Id,
                    Name = person.Name,
                    Email = person.Email,
                    DateOfBirth = person.DateOfBirth,
                };
                return await Task.Run(() => View("View",viewModel));
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdatePersonViewModel model) { 
            var person = await mvcDbContext.Persons.FindAsync(model.Id);
            if(person != null)
            {
                person.Name = model.Name;
                person.Email = model.Email;
                person.DateOfBirth = model.DateOfBirth;
                await mvcDbContext.SaveChangesAsync();
                
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdatePersonViewModel model)
        {
            var person = await mvcDbContext.Persons.FindAsync(model.Id);
            if (person != null)
            {
                mvcDbContext.Persons.Remove(person);
                await mvcDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
