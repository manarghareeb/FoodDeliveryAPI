using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.IdentityModule
{
    public class RegisterDto
    {
        public string UserName { get; init; } = string.Empty;
        public string DisplayName { get; init; } = string.Empty;
        [EmailAddress]
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        [Phone]
        public string? PhoneNumber { get; init; }
    }
}
