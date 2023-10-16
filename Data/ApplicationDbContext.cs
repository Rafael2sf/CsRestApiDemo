using Microsoft.EntityFrameworkCore;
using BuberBreakfast.Models;

namespace BuberBreakfast.Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }
    public DbSet<Breakfast> Breakfast { get; set; }
}
