using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApplication1.Data.Entities
{
    public class Authour
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "AUthour's Name")]

        public string AuthourName { get; set; } = default!;

        public ICollection<News> News { get; set; } = default!;
    }
}
