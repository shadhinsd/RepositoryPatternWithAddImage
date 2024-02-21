using Microsoft.EntityFrameworkCore;
using RepositoryWithAddImage.ViewModel;

namespace RepositoryWithAddImage.DatabaseContext;

public class StudentDbContext(DbContextOptions<StudentDbContext> dbContext) : DbContext(dbContext)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentDbContext).Assembly);
    }

public DbSet<RepositoryWithAddImage.ViewModel.StudentVm> StudentVm { get; set; } = default!;
}
