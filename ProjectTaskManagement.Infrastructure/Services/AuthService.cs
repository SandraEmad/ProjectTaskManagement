using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectTaskManagement.Application.DTOs.Auth;
using ProjectTaskManagement.Application.Interfaces;
using ProjectTaskManagement.Domain.Entities;
using ProjectTaskManagement.Domain.Exceptions;

namespace ProjectTaskManagement.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {

            if (await _userManager.FindByEmailAsync(dto.Email) != null)
                throw new DomainException("Email already exists");

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                throw new DomainException(
                    string.Join(", ", result.Errors.Select(e => e.Description)));

            var role = dto.Role == "Admin" ? "Admin" : "User";
            await _userManager.AddToRoleAsync(user, role);
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                throw new DomainException(
                    string.Join(", ", result.Errors.Select(e => e.Description)));

            return new AuthResponseDto
            {
                Token = _jwtService.GenerateToken(user),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {

            var user = await _userManager.FindByEmailAsync(dto.Email)
                ?? throw new UnauthorizedException();


            if (!await _userManager.CheckPasswordAsync(user, dto.Password))
                throw new UnauthorizedException();

            return new AuthResponseDto
            {
                Token = _jwtService.GenerateToken(user),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!
            };
        }
    }
}