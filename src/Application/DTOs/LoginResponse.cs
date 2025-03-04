#nullable disable

namespace Application.DTOs;

public record LoginResponse(bool IsAuthenticated, string ErrorMessage);
