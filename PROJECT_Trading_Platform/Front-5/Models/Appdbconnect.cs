using Front_5.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace front_5.Models
{
    public class Appdbcontext : IdentityDbContext<User>
    {


        public Appdbcontext(DbContextOptions options) : base(options)
        {
        }
            public DbSet<Slider> Sliders { get; set; }

            public DbSet<Sport_pro> Cards { get; set; }

            public DbSet<state> States { get; set; }

            public DbSet<Category> Category { get; set; }

            public DbSet<Products> Products { get; set; }

            public DbSet<Category_two> Category_two { get; set; }

            public DbSet<Images> Images { get; set; }
            public DbSet<Colors> Colors { get; set; }

            public DbSet<Orders> Orders { get; set; }

            public DbSet<Size> Size { get; set; }
        internal async Task<string?> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
