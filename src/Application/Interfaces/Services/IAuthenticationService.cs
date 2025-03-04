#nullable disable

using Application.DTOs;

namespace Application.Interfaces.Services;

public interface IAuthenticationService
{
    Task<LoginResponse> AuthenticateAsync(LoginRequest request);
}
