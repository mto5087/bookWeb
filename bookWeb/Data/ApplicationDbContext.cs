using Microsoft.EntityFrameworkCore;
using bookWeb.Models;

namespace bookWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // creates Category table with name Categories.  It uses the category class that we wrote for its model.
        // This is the code first model, because database is not already created.

        public DbSet<Category> Categories { get; set; }
    }
}
