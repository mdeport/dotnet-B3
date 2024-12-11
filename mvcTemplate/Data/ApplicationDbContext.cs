using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mvc.Models;

namespace mvc.Data;

public class ApplicationDbContext : IdentityDbContext<Teacher>
{
    //public DbSet<Teacher> Teachers { get; set; }
    
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}