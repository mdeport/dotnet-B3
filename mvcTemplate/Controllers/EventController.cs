using Microsoft.AspNetCore.Mvc;
using System.Linq;
using mvc.Data;
using mvc.Models;

namespace mvc.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var events = _context.Events;
            return View(events);
        }

        public ActionResult Show(int id)
        {
            var eventToShow = _context.Events.Find(id);
            return View(eventToShow);
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
                return RedirectToAction("Index");
            }
            return View(newEvent);
        }

        public ActionResult Delete(int id)
        {
            var existingEvent = _context.Events.Find(id);
            _context.Events.Remove(existingEvent);
            _context.SaveChanges();
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
            
            return RedirectToAction("Index");
        }
    }
}