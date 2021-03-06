using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {   }

        
        public DbSet<Users> users { get; set; }
        public DbSet<Request> request { get; set; }
        public DbSet<RequestList> requestlist { get; set; }
        public DbSet<Menu> menu { get; set; }
        public DbSet<Neighbohoods> neighbohoods { get; set; }
        public DbSet<Coupon> coupon { get; set; }

    }
}