using LibraryManagmentAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentAPI.Infrastructure.Data
{
    public class AppDbContext:IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
    }
}
