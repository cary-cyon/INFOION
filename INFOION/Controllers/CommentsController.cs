using INFOION.Models;
using Microsoft.AspNetCore.Mvc;
using INFOION.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace INFOION.Controllers
{
    public class CommentsController : Controller
    {
        private readonly InfoionDbContext _con;
        public CommentsController(InfoionDbContext con)
        {
            _con = con;
        }
        public IActionResult Index(int? Id)
        {
            var userId = HttpContext.Request.Cookies["id"];
            ViewData["UserId"] = new SelectList(_con.Users, "Id", "Id").ToList().Where(id => id.Value == userId);
            ViewData["ArticleId"] = new SelectList(_con.Articles, "Id", "Id").ToList().Where(id => id.Value == Id.ToString());
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Comment comment)
        {
            await _con.AddAsync(comment);
            await _con.SaveChangesAsync();
            return RedirectToAction("Index", "Articles");
        }
    }
}
