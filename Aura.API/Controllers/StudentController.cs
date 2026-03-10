using System.Security.Claims;
using Aura.Application.DTOs;
using Aura.Domain.Entities;
using Aura.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aura.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Bloqueia acesso sem Token JWT
public class StudentController(StudentRepository repository) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateStudentDto model)
    {
        // 1. Extrai o ID do Professor do Token
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized();

        // 2. Mapeamento manual do DTO para a Entidade Student
var student = new Student
{
    Name = model.Name,
    Email = model.Email,
    Phone = model.Phone,
    City = model.City,
    State = model.State,
    Subject = model.Subject,
    Level = model.Level,
    CurrentGrade = model.CurrentGrade,
    TeacherId = Guid.Parse(userIdClaim),
    CreatedAt = DateTime.UtcNow
};

        // 3. Salva no PostgreSQL via repositório
        await repository.AddAsync(student);

        return Ok(new { message = "Aluno cadastrado com sucesso!", studentId = student.Id });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Garante que o professor veja apenas os SEUS próprios alunos
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized();

        var students = await repository.GetByTeacherIdAsync(Guid.Parse(userIdClaim));
        
        return Ok(students);
    }
}