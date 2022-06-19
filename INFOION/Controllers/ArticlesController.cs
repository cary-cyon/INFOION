using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INFOION.Data;
using INFOION.Models;
using Microsoft.AspNetCore.Authorization;

namespace INFOION.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        private readonly InfoionDbContext _context;

        public ArticlesController(InfoionDbContext context)
        {
            _context = context;
        }

        // GET: Articles
        
        public async Task<IActionResult> Index()
        {
            var infoionDbContext = _context.Articles.Include(a => a.Category).Include(a => a.Country).Include(a => a.Publisher).Include(a => a.Source);
            return View(await infoionDbContext.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Category)
                .Include(a => a.Country)
                .Include(a => a.Publisher)
                .Include(a => a.Source)
                .Include(a => a.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
             
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id");
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Id");
            ViewData["SourceId"] = new SelectList(_context.Sources, "Id", "Id");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Content,CategoryId,CountryId,PublisherId,SourceId")] Article article)
        {
            _context.Add(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", article.CategoryId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", article.CountryId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Id", article.PublisherId);
            ViewData["SourceId"] = new SelectList(_context.Sources, "Id", "Id", article.SourceId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Content,CategoryId,CountryId,PublisherId,SourceId")] Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }
            try
            {
                _context.Update(article);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(article.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Category)
                .Include(a => a.Country)
                .Include(a => a.Publisher)
                .Include(a => a.Source)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Articles == null)
            {
                return Problem("Entity set 'InfoionDbContext.Articles'  is null.");
            }
            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
          return (_context.Articles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
