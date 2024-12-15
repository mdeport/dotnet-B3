using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc.Data;
using mvc.Models;

namespace mvc.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
        private readonly UserManager<Student> _userManager;

    public HomeController(ApplicationDbContext context, UserManager<Student> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index(string sortOrder, string searchString)
    {
        ViewData["CurrentSort"] = sortOrder;
        ViewData["CurrentFilter"] = searchString;
            var events = from e in _context.Events
                     select e;
        if (!string.IsNullOrEmpty(searchString))
        {
            events = events.Where(e => e.Title.Contains(searchString));
        }
        switch (sortOrder)
        {
            case "title":
                events = events.OrderBy(e => e.Title);
                break;
            case "date":
                events = events.OrderBy(e => e.EventDate);
                break;
            default:
                events = events.OrderBy(e => e.Title);
                break;
        }
        return View(await events.ToListAsync());
    }   

    [HttpGet]
     public async Task<IActionResult> Inscription(int eventId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }
        var inscription = new Inscription
            {
            EventId = eventId,
            StudentId = user.Id
        };
        _context.Inscriptions.Add(inscription);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> Desinscription(int eventId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }
        var inscription = await _context.Inscriptions.FirstOrDefaultAsync(i => i.EventId == eventId && i.StudentId == user.Id);
        _context.Inscriptions.Remove(inscription);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
