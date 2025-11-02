using Microsoft.EntityFrameworkCore;
using CrudOperations_EF.Models;

namespace CrudOperations_EF.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options) { }
        public DbSet<SahilTable> SahilTable { get; set; }
    }
}
