using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;

public class AccountController : Controller
{
    private readonly SignInManager<Teacher> _signInManager;

    private readonly UserManager<Teacher> _userManager;

    public AccountController(SignInManager<Teacher> signInManager, UserManager<Teacher> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(AccountViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = new Teacher
        {
            UserName = model.Lastname + model.Firstname,
            Email = model.Email,
            Firstname = model.Firstname,
            Lastname = model.Lastname,
            Langage = "",
            Matter = "",
            IsStudent = false,
            IsTeacher = true
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        ModelState.AddModelError(string.Empty, "Erreur lors de l'inscription");

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("IndexAccount");
    }

    public IActionResult IndexAccount()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
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

        ModelState.AddModelError(string.Empty, "Erreur lors de la connexion");

        return View(model);
    }
}