using Heladus.Models;
using Microsoft.EntityFrameworkCore;


namespace Heladus.Context
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Helado> Helado { get; set; }
    }
}