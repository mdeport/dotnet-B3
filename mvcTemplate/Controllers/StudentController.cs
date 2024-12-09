using Microsoft.AspNetCore.Mvc;
using mvc.Models;

namespace mvc.Controllers
{
    public class StudentController : Controller
    {
        
        public IActionResult Index()
        {
            var student = new Student();
            student.Id = 1;
            student.Firstname = "John";
            student.Lastname = "Doe";
            student.Age = 20;
            return View(student);
        }
    }
}