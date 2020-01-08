using Microsoft.EntityFrameworkCore;
using ReadersApi.Providers;

namespace ReadersApi
{
    public class MyContext : DbContext
    {
        public DbSet<Reader> Readers { get; set; }
        public DbSet<User> Users { get; set; }
    }
}