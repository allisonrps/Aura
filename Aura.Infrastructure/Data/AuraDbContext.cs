using Aura.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aura.Infrastructure.Data;

public class AuraDbContext : DbContext
{
    public AuraDbContext(DbContextOptions<AuraDbContext> options) : base(options) { }

    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<ExtraLesson> ExtraLessons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurações de precisão para valores monetários
        modelBuilder.Entity<Payment>()
            .Property(p => p.Value)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Student>()
            .Property(s => s.CurrentGrade)
            .HasPrecision(5, 2);

        // Relacionamentos (O EF geralmente entende, mas é bom ser explícito)
        modelBuilder.Entity<Teacher>()
            .HasMany(t => t.Students)
            .WithOne(s => s.Teacher)
            .HasForeignKey(s => s.TeacherId);

        base.OnModelCreating(modelBuilder);
    }
}