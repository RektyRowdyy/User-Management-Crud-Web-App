using CRUDApp.Areas.Identity.Data;
using CRUDApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SendGrid.Helpers.Mail;

namespace CRUDApp.Controllers

{
    [Authorize]
    public class UserGrid : Controller
    {
        private readonly CRUDAppDbContext user;

        public UserGrid(CRUDAppDbContext user)
        {
            this.user = user;
        }
        public async Task<IActionResult> ListUsers() 
        {
               
            return View(await user.Users.ToListAsync());
        }

        public async Task<IActionResult> DetailsAsync(string? Id) {
            if (Id == null || user.Users == null)
            {
                return NotFound();
            }

            var homePage = await user.Users              
                .FirstOrDefaultAsync(m => m.Id == Id);
            if (homePage == null)
            {
                return NotFound();
            }

            return View(homePage);
        }

        // GET: Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string? Id)
        {
            var users = await user.Users.Where(m => m.Id == Id).FirstOrDefaultAsync();
            return View(users); 
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CRUDAppUser app)
        {
            var person = await user.Users.Where(m => m.Id == app.Id).FirstOrDefaultAsync();
            if(person == null)
            {
                return NotFound();
            }

            person.FirstName = app.FirstName;
            person.LastName = app.LastName;
            person.Email = app.Email;
            person.DOB = app.DOB;
            person.Gender = app.Gender;

            var result = await user.SaveChangesAsync();

            TempData["Save"] = "User has been updated successfully";
            return RedirectToAction("ListUsers");
            
        }

        [HttpPost]
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
           
                return View("ListUsers", await user.Users.Where(
                j => j.FirstName.Contains(SearchPhrase) ||
                j.LastName.Contains(SearchPhrase) ||
                j.DOB.ToString().Contains(SearchPhrase) ||
                j.Gender.Equals(SearchPhrase) ||
                j.Email.Contains(SearchPhrase)).ToListAsync());
             
        }

    }
}
