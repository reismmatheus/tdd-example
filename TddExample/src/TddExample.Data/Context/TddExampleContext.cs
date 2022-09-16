using Microsoft.EntityFrameworkCore;
using TddExample.Domain;

namespace TddExample.Data.Context
{
    public class TddExampleContext : DbContext
    {
        public TddExampleContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
