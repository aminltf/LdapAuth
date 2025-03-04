#nullable disable

using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;

namespace Infrastructure.Ldap.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IUserRepository userRepository) => _userRepository = userRepository;

    public async Task<LoginResponse> AuthenticateAsync(LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            return new LoginResponse(false, "Username and password are required.");
        }

        var isAuthenticated = await _userRepository.ValidateCredentialsAsync(request.Username, request.Password);

        if (!isAuthenticated)
        {
            return new LoginResponse(false, "Invalid username or password.");
        }

        return new LoginResponse(true, "Authenticated successfully.");
    }
}
