#nullable disable

using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Auth;

public class LoginModel : PageModel
{
    private readonly IAuthenticationService _authService;

    public LoginModel(IAuthenticationService authService) => _authService = authService;

    [BindProperty]
    public LoginRequest Input { get; set; }

    public string ErrorMessage { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _authService.AuthenticateAsync(Input);
        if (response.IsAuthenticated)
        {
            return RedirectToPage("/Index");
        }

        ErrorMessage = response.ErrorMessage;
        return Page();
    }
}
