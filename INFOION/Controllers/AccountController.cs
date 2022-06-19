using INFOION.Data;
using INFOION.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace INFOION.Controllers
{
    public class AccountController : Controller
    {
        private readonly InfoionDbContext _con;
        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public AccountController(InfoionDbContext con)
        {
            _con = con;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User model)
        {
            User user = await _con.Users.FirstOrDefaultAsync(u => u.Name == model.Name && u.LastName == model.LastName);
            if (user != null)
            {
                await Authenticate(model.Name); // аутентификация

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Regist()
        {
            ViewData["CountryId"] = new SelectList(_con.Countries, "Id", "Id");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Regist(User user)
        {
            await _con.Users.AddAsync(user);
            await _con.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
