using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Pustok.Models
{
    public class PustokContext:IdentityDbContext
    {
        public PustokContext(DbContextOptions<PustokContext> options): base(options) { }

        public DbSet<Slider> sliders { get; set; }
        public DbSet<Book> books { get; set; }  
        public DbSet<Genre> genres { get; set; }
        public DbSet<Author> authors { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
        public DbSet<AppUser> Users { get; set; }
    }
}
