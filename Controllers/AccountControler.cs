using Microsoft.AspNetCore.Mvc;
using Library.Data;
using Library.Models;
using Library.Utility;

namespace Library.Controllers;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(User user, string plainPassword)
    {
        if (_context.Users.Any(u => u.Login == user.Login))
        {
            ModelState.AddModelError("Login", "Ten login jest już zajęty.");
        }

        if (ModelState.IsValid)
        {
            user.PasswordHash = PasswordHasher.HashPassword(plainPassword);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login");
        }
        return View(user);
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public IActionResult Login(string login, string password)
    {
        var hash = PasswordHasher.HashPassword(password);
        var user = _context.Users.FirstOrDefault(u => u.Login == login && u.PasswordHash == hash);

        if (user != null)
        {
            CookieOptions options = new CookieOptions {
                Expires = DateTime.Now.AddDays(1),
                HttpOnly = true
            };
            Response.Cookies.Append("UserLogin", user.Login, options);
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Nieprawidłowy login lub hasło";
        return View();
    }

    public IActionResult Logout()
    {
        Response.Cookies.Delete("UserLogin");
        return RedirectToAction("Index", "Home");
    }
}