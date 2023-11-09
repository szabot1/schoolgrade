using Microsoft.EntityFrameworkCore;
using schoolgrade.Models;

namespace schoolgrade.Data;

public class SchoolContext : DbContext
{
    public DbSet<Grade> Grades { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;

    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
    {
    }

}