using Aura.Domain.Entities;

namespace Aura.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(Teacher teacher);
}