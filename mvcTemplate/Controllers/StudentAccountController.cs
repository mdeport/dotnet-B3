using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Student> _signInManager;
        private readonly UserManager<Student> _userManager;

        public AccountController(SignInManager<Student> signInManager, UserManager<Student> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult LoginStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginStudent(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            var result = await _signInManager.PasswordSignInAsync(user?.UserName!, model.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }
    }
}