using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;
using WebApplication1.Data.Entities;
using WebApplication1.Models.DTO;
using WebApplication1.Models.ViewModels.NewsViewModels.admin;
using WebApplication1.Models.ViewModels.NewsViewModels.User;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        private readonly NewsContext _context;
        private readonly IMapper mapper;
        private readonly ILogger _logger;

        public AdminController(NewsContext context, IMapper mapper, ILoggerFactory logger)
        {
            _context = context;
            this.mapper = mapper;
            _logger = logger.CreateLogger<AdminController>();
        }

        public async Task<IActionResult> Index(int AuthourId, string? search)
        {
            IQueryable<News> news = _context.News.
                Include(t => t.Authour);
            if (AuthourId > 0)
                news = news.Where(t => t.AuthourId == AuthourId);

            if (search is not null)
                news = news.Where(t => t.Title!.Contains(search));

            IQueryable<Authour> authours = _context.Authours;

            SelectList _AuthorSL = new SelectList(await authours.Distinct().ToListAsync(),
                dataValueField: nameof(Authour.Id),
                dataTextField: nameof(Authour.AuthourName),
                selectedValue: AuthourId
                );
            IEnumerable<newsDTO> tempNews = mapper
                .Map<IEnumerable<newsDTO>>(
                await news.ToListAsync());
            IndexNewsAdminViewModels vm = new IndexNewsAdminViewModels
            {
                NewsDTOs = tempNews,
                AuthourSL = _AuthorSL,
                Search = search,
                AuthourId = AuthourId
            };
            return View(vm);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var cat = await _context.News.Include(c=>c.Authour)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cat == null)
            {
                return NotFound();
            }
            DetailNewsAdminViewModel vM = new DetailNewsAdminViewModel
            {
                NewsDTO = mapper.Map<newsDTO>(cat),
            };
            return View(vM);
        }
        public IActionResult Create()
        {
            SelectList _AuthourSL = new SelectList(_context.Authours.Distinct().ToList(),
                dataValueField: nameof(Authour.Id),
                dataTextField: nameof(Authour.AuthourName),
                selectedValue: _context.Authours.Select(t => t.Id).FirstOrDefault()
                );
            CreateNewsAdminViewModels vm = new CreateNewsAdminViewModels {
                AuthourSL = new SelectList(_context.Authours, "Id", "AuthourName")
        };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateNewsAdminViewModels vM)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(t => t.Errors);
                foreach (var error in errors)
                    _logger.LogInformation(error.ErrorMessage);
                SelectList _AthourSL = new SelectList(await _context.Authours.ToListAsync(),
                    nameof(Authour.Id),
                    nameof(Authour.AuthourName),
                    selectedValue:vM.NewsDTO.AuthourId
                    );
                vM.AuthourSL = _AthourSL;
                foreach (var error in ModelState.Values.SelectMany(t => t.Errors))
                {
                    _logger.LogError(error.ErrorMessage);
                }
                return View(vM);
            }
            using (BinaryReader br = new BinaryReader(vM.Image.OpenReadStream()))
            {
                vM.NewsDTO.Image = br.ReadBytes((int)vM.Image.Length);
            }
            News createdCat = mapper.Map<News>(vM.NewsDTO);
            _context.Add(createdCat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            EditNewsAdminViewsModels vM = new EditNewsAdminViewsModels
            {
                NewsDTO = mapper.Map<newsDTO>(news),
                AuthourSL= new SelectList(await _context.Authours.ToListAsync(),
                    nameof(Authour.Id),
                    nameof(Authour.AuthourName),
                    selectedValue: id
                    )
        };
            return View(vM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditNewsAdminViewsModels vM)
        {
            if (id != vM.NewsDTO.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(t => t.Errors))
                    _logger.LogError(error.ErrorMessage);
                return View(vM);
            }
            try
            {
                if (vM.Image is not null)
                {
                    using (BinaryReader br = new BinaryReader(vM.Image.OpenReadStream()))
                    {
                        vM.NewsDTO.Image = br.ReadBytes((int)vM.Image.Length);
                    }
                }
                News editedNews = mapper.Map<News>(vM.NewsDTO);
                _context.Update(editedNews);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatExists(vM.NewsDTO.Id))
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.News == null)
            {
                return Problem("Entity set 'NewsContext.News'  is null.");
            }
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatExists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }
    }
}
