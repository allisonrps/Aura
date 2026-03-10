namespace Aura.Application.DTOs;

public record RegisterTeacherDto(
    string Name,
    string Email,
    string Password
);