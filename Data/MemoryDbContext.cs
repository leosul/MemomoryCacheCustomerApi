using MemoryCache.API.Model;
using Microsoft.EntityFrameworkCore;

namespace MemoryCache.API.Data;

public class MemoryDbContext : DbContext
{
    public MemoryDbContext(DbContextOptions<MemoryDbContext> options) : base(options)
    {

    }

    public DbSet<Customer> Customers { get; set; }
}
