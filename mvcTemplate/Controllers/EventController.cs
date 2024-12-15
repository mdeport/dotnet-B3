using Microsoft.AspNetCore.Mvc;
using System.Linq;
using mvc.Data;
using mvc.Models;
using mvc.Services;


namespace mvc.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserService _userService;

        public EventController(ApplicationDbContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public ActionResult Index()
        {
            var events = _context.Events;
            return View(events);
        }

        public async Task<IActionResult> Show(int id)   
        {
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem == null)
            {
                return NotFound();
            }

            var students = await _userService.GetStudentsInscribedAsync(id);
            ViewBag.Students = students;

            return View(eventItem);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Event newEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Events.Add(newEvent);
                _context.SaveChanges();
                TempData["Message"] = "Événement créé avec succès.";
                return RedirectToAction("Index");
            }
            return View(newEvent);
        }

        public ActionResult Delete(int id)
        {
            var existingEvent = _context.Events.Find(id);
            _context.Events.Remove(existingEvent);
            _context.SaveChanges();
            TempData["Message"] = "Événement supprimé avec succès.";
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            var curreneEvent = _context.Events.Find(id);
            return View(curreneEvent);
        }

        [HttpPost]
        public ActionResult Update(Event currentEvent)
        {
            Event currentEventToUpdate = _context.Events.Find(currentEvent.Id);
            currentEventToUpdate.Title = currentEvent.Title;
            currentEventToUpdate.Description = currentEvent.Description;
            currentEventToUpdate.EventDate = currentEvent.EventDate;
            currentEventToUpdate.MaxParticipants = currentEvent.MaxParticipants;
            currentEventToUpdate.Location = currentEvent.Location;
            _context.SaveChanges();
            TempData["Message"] = "Événement modifié avec succès.";
            
            return RedirectToAction("Index");
        }
    }
}