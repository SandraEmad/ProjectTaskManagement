using ProjectTaskManagement.Domain.Entities;

namespace ProjectTaskManagement.Application.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateToken(ApplicationUser user); 
    }
}