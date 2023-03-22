using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApplication1.Models.DTO
{
    public class newsDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You have to write Title of news")]
        [Display(Name = "Title")]
        public string Title { get; set; } = default!;
        [Required]
        public string Description { get; set; } = default!;

        //public string? AuthorName { get; set; }
        [Display(Name = "Author Name")]

        public AuthourDTO? AuthourDTO { get; set; }

        public int AuthourId { get; set; }

        public byte[]? Image { get; set; }

        public DateTime Date { get; set; }
    }
}
