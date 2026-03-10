using Aura.Application.DTOs;
using Aura.Application.Interfaces;
using Aura.Domain.Entities;
using Aura.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Aura.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController(
    TeacherRepository repository, 
    IPasswordService passwordService,
    ITokenService tokenService) : ControllerBase 
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterTeacherDto model)
    {
        var existingTeacher = await repository.GetByEmailAsync(model.Email);
        if (existingTeacher != null)
            return BadRequest("Este e-mail já está cadastrado.");

        var teacher = new Teacher
        {
            Name = model.Name,
            Email = model.Email,
            PasswordHash = passwordService.HashPassword(model.Password)
        };

        await repository.AddAsync(teacher);

        return Ok(new { message = "Professor cadastrado com sucesso!" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto model)
    {
        // 1. Buscar o professor pelo e-mail
        var teacher = await repository.GetByEmailAsync(model.Email);
        if (teacher == null) 
            return Unauthorized("E-mail ou senha inválidos.");

        // 2. Verificar a senha
        var isValid = passwordService.VerifyPassword(model.Password, teacher.PasswordHash);
        if (!isValid) 
            return Unauthorized("E-mail ou senha inválidos.");

        // 3. Gerar o Token usando o serviço injetado
        var token = tokenService.GenerateToken(teacher);

        return Ok(new { token });
    }
}