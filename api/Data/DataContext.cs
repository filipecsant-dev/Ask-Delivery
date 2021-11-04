using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {  }

        
        public DbSet<Users> users { get; set; }
        public DbSet<Request> request { get; set; }
    }
}