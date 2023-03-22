using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using WebApplication1.Data.Entities;
using WebApplication1.Models.DTO;
using WebApplication1.Models.ViewModels.NewsViewModels.User;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private readonly NewsContext _context;
        private readonly IMapper mapper;
        private readonly ILogger _logger;

        public UserController(NewsContext context, IMapper mapper, ILoggerFactory logger)
        {
            _context = context;
            this.mapper = mapper;
            _logger = logger.CreateLogger<UserController>();
        }

        public async Task<IActionResult> Index(int AuthourId, string? search)
        {
           
            IQueryable<News> news=_context.News.
                Include(t=>t.Authour);
            if (AuthourId>0)
                news=news.Where(t=>t.AuthourId== AuthourId);

            if (search is not null)
                news = news.Where(t => t.Title!.Contains(search));

            IQueryable<Authour> authours = _context.Authours;

            SelectList _AuthorSL = new SelectList(await authours.ToListAsync(),
                dataValueField: nameof(Authour.Id),
                dataTextField: nameof(Authour.AuthourName),
                selectedValue: AuthourId
                );
            IEnumerable<newsDTO> tempNews = mapper
                .Map<IEnumerable<newsDTO>>(
                await news.ToListAsync());
            IndexNewsUserViewModels vm=new IndexNewsUserViewModels { 
            NewsDTOs= tempNews,
            AuthourSL= _AuthorSL,
            Search= search,
            AuthourId=AuthourId
            };
            return View(vm);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var cat = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cat == null)
            {
                return NotFound();
            }
            DetailNewsViewModels vM = new DetailNewsViewModels
            {
                NewsDTO = mapper.Map<newsDTO>(cat)
            };
            return View(vM);
        }
    }
}
