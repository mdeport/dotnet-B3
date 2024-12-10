using Microsoft.AspNetCore.Mvc;
using mvc.Data;
using mvc.Models;

namespace mvc.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext _context;
    
        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var teachers = _context.Teachers;
            return View(teachers);
        }

        public ActionResult Delete(int id)
        {
            var teacher = _context.Teachers.FirstOrDefault(t => t.Id == id);
            _context.Teachers.Remove(teacher);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Show(int id)
        {
            var teacher = _context.Teachers.FirstOrDefault(t => t.Id == id);
            return View(teacher);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Teachers.Add(teacher);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            var teacher = _context.Teachers.FirstOrDefault(t => t.Id == id);
            return View(teacher);
        }

        [HttpPost]
        public IActionResult Update(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Teachers.Update(teacher);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }
    }
}