using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models.DTO;

namespace WebApplication1.Models.ViewModels.NewsViewModels.User
{
    public class IndexNewsUserViewModels
    {
        public IEnumerable<newsDTO> NewsDTOs { get; set; } = default!;

        public SelectList AuthourSL { get; set; } = default!;

        public int AuthourId { get; set; }

        public string? Search { get; set; }
    }
}
