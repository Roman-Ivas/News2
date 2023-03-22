using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models.DTO;

namespace WebApplication1.Models.ViewModels.NewsViewModels.admin
{
    public class CreateNewsAdminViewModels
    {
        public newsDTO NewsDTO { get; set; } = default!;

        public SelectList? AuthourSL { get; set; } = default!;
        public IFormFile Image { get; set; } = default!;
    }
}
