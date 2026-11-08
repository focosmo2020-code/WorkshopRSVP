using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopRSVP.Data;
using WorkshopRSVP.Models;

namespace WorkshopRSVP.Controllers
{
    public class WorkshopController : Controller
    {
        private readonly ApplicationDbContext _context;

        private static readonly List<string> _workshopNames = new()
        {
            "Routing Basics",
            "EF Core Intro",
            "Razor Essentials"
        };

        public WorkshopController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Workshop
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Workshop/RSVPForm
        [HttpGet]
        public IActionResult RSVPForm()
        {
            ViewData["Workshop"] = _workshopNames;
            return View(new RSVP());
        }

        // POST: /Workshop/Confirm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Confirm(RSVP model)
        {
            if (ModelState.IsValid)
            {
                // Lab 4: saving is not required (the in-memory list is gone).
                ViewData["Message"] = $"Thanks for registering, {model.FullName}!";
                return View(model);
            }

            ViewData["Workshop"] = _workshopNames;
            return View("RSVPForm", model);
        }

        // GET: /Workshop/Registrations - read from the database
        public async Task<IActionResult> Registrations()
        {
            var registrations = await _context.Rsvps.ToListAsync();

            var registrationsByWorkshop = registrations
                .GroupBy(r => r.WorkshopName)
                .ToDictionary(g => g.Key, g => g.ToList());

            ViewData["RegistrationsByWorkshop"] = registrationsByWorkshop;

            return View(registrations);
        }
    }
}