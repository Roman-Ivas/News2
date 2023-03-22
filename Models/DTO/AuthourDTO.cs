using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApplication1.Models.DTO
{
    public class AuthourDTO
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Authour' Name")]
        public string AuthourName { get; set; } = default!;

        public ICollection<newsDTO> News { get; set; } = default!;
    }
}
