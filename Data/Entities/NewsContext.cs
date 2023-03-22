using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication1.Data.Entities
{
    public class NewsContext:DbContext
    {
        public DbSet<Authour> Authours { get; set; } = default!;
        public DbSet<News> News => Set<News>();

        public NewsContext(DbContextOptions<NewsContext> options) : base(options)
        {

        }
}
}
