using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mvc.Data;
using mvc.Models;

namespace mvc.Controllers
{
    public class StudentController : Controller
    {

        private readonly UserManager<Student> _userManager;
        private readonly ApplicationDbContext _context;
        
        public StudentController(ApplicationDbContext context, UserManager<Student> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            var students = _context.Students;
            return View(students);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(StudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new Student
            {
                UserName = model.Lastname + model.Firstname,
                Email = model.Email,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Age = model.Age,
                FirstConnection = true,
                IsStudent = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        public IActionResult Show(string id)
        {
            var student = _context.Students.Find(id);
            return View(student);
        }

        public  IActionResult Delete(string id)
        {
            var student = _context.Students.Find(id);
            _context.Students.Remove(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}