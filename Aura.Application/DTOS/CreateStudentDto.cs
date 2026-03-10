namespace Aura.Application.DTOs;

public record CreateStudentDto(
    string Name,
    string Email,
    string Phone,
    string City,
    string State,
    string Subject,
    string Level,
    decimal CurrentGrade
);