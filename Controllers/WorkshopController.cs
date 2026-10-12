using Microsoft.AspNetCore.Mvc;
using WorkshopRSVP.Models;

namespace WorkshopRSVP.Controllers
{
    public class WorkshopController : Controller
    {
        private static readonly List<RSVP> _registrations = new();

        private static readonly List<string> _workshopNames = new()
        { 
            "Routing Basics",
            "EF Core Intro",
            "Razor Essentials"
        };

        // GET: /Workshop Landing page for the workshop registration
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Workshop/RSVP empty form
        [HttpGet]
        public IActionResult RSVPForm()
        {
            ViewData["Workshop"] = _workshopNames;
            return View(new RSVP());
        }

        // POST: /Workshop/Confirm RSVP submission - receive submitted form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Confirm(RSVP model)
        {
            if (ModelState.IsValid)
            {
                // Add the registration to the list
                _registrations.Add(model);
                // Set confirmation message
                ViewData["Message"] = $"Thanks for registering, {model.FullName}!";
                // Return to strongly typed view
                return View(model);
            }
            //show form with the user input and errors if any
            ViewData["Workshop"] = _workshopNames;
            return View("RSVPForm", model);
        }

        // GET: /Workshop/Registrations - show all registrations
        public IActionResult Registrations()
        {
            var registrationsByWorkshop = _registrations
                .GroupBy(r => r.WorkshopName)
                .ToDictionary(g => g.Key, g => g.ToList());

            ViewData["RegistrationsByWorkshop"] = registrationsByWorkshop;

            return View(_registrations);
        }
    }

}