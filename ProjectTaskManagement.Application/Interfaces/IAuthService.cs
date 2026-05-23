
using ProjectTaskManagement.Application.DTOs.Auth;

namespace ProjectTaskManagement.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);

        Task<AuthResponseDto> LoginAsync(LoginDto dto);
    }
}
