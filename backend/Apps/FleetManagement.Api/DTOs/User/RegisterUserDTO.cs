using FleetManagement.Api.CustomValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Api.DTOs.User
{
    public class RegisterUserDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Password must at least be 5 characters")]
        public string? Password { get; set; }
        [Required]
        [IsRole(ErrorMessage = "Role can only be 'User' or 'Admin'")]
        public string? Role { get; set; }
    }
}
