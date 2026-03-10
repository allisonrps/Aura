using Aura.Domain.Entities;
using Aura.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Aura.Infrastructure.Repositories;

public class TeacherRepository(AuraDbContext context)
{
    public async Task AddAsync(Teacher teacher)
    {
        await context.Teachers.AddAsync(teacher);
        await context.SaveChangesAsync();
    }

    public async Task<Teacher?> GetByEmailAsync(string email)
    {
        return await context.Teachers.FirstOrDefaultAsync(t => t.Email == email);
    }
}