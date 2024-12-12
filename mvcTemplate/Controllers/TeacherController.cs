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

        public ActionResult Delete(string id)
        {
            var teacher = _context.Teachers.Find(id);
            _context.Teachers.Remove(teacher);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Show(string id)
        {
            var teacher = _context.Teachers.Find(id);
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

        public ActionResult Update(string id)
        {
            var teacher = _context.Teachers.Find(id);
            return View(teacher);
        }

        [HttpPost]
        public ActionResult Update(Teacher teacher)
        {
            Teacher teacherToUpdate = _context.Teachers.Find(teacher.Id);
            teacherToUpdate.Firstname = teacher.Firstname;
            teacherToUpdate.Lastname = teacher.Lastname;
            teacherToUpdate.Age = teacher.Age;
            teacherToUpdate.Matter = teacher.Matter;
            teacherToUpdate.Langage = teacher.Langage;
            _context.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}