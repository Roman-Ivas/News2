using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data.Entities
{
    public class News
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You have to write Title of news")]
        [Display(Name = "Title")]
        public string Title { get; set; } = default!;

        public string Description { get; set; }=default!;

        //public string? AuthorName { get; set; }

        public Authour? Authour { get; set; }

        public int AuthourId { get; set; }


        public byte[]? Image { get; set; }

        public DateTime Date { get; set; }
    }
}
