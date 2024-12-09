using Microsoft.AspNetCore.Mvc;
using mvc.Models;

namespace mvc.Controllers
{
    public class TeacherController : Controller
    {
        private static List<Teacher> teachers = new()
        {
            new() { Id = 1,Age = 20, Firstname = "John", Lastname = "Doe", Langage = "Ruby on rails", Matter = "DEV"},
            new() { Id = 2,Age = 20, Firstname = "NAME 1", Lastname = "Doe1", Langage = "C#", Matter = "DEV"},
            new() { Id = 3,Age = 20, Firstname = "NAME 2", Lastname = "Doe2", Langage = "C++", Matter = "DEV"},
            new() { Id = 4,Age = 20, Firstname = "NAME 3", Lastname = "Doe3", Langage = "PHP", Matter = "DEV"},
        };
        public ActionResult Index()
        {
            return View(teachers);
        }

        public ActionResult Delete(int id)
        {
            var teacher = teachers.FirstOrDefault(t => t.Id == id);
            teachers.Remove(teacher);
            return RedirectToAction("Index");
        }

        public ActionResult Show(int id)
        {
            var teacher = teachers.FirstOrDefault(t => t.Id == id);
            return View(teacher);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                teacher.Id = teachers.Max(e => e.Id) + 1;
                teachers.Add(teacher);
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        public ActionResult Update(int id)
        {
            var teacher = teachers.FirstOrDefault(t => t.Id == id);
            return View(teacher);
        }

        [HttpPost]
        public IActionResult Update(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                var existingTeacher = teachers.FirstOrDefault(t => t.Id == teacher.Id);
                existingTeacher.Firstname = teacher.Firstname;
                existingTeacher.Lastname = teacher.Lastname;
                existingTeacher.Age = teacher.Age;
                existingTeacher.Matter = teacher.Matter;
                existingTeacher.Langage = teacher.Langage;
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }
    }
}