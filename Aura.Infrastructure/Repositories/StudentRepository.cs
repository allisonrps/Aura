using Aura.Domain.Entities;
using Aura.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Aura.Infrastructure.Repositories;

public class StudentRepository(AuraDbContext context)
{
    public async Task AddAsync(Student student)
    {
        await context.Students.AddAsync(student);
        await context.SaveChangesAsync();
    }

    public async Task<List<Student>> GetByTeacherIdAsync(Guid teacherId)
    {
        return await context.Students
            .Where(s => s.TeacherId == teacherId)
            .ToListAsync();
    }
}