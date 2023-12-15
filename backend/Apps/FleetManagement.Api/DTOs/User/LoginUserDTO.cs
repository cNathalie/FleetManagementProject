using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Api.DTOs.User
{
    public class LoginUserDTO 
    {
        [Required]
        [EmailAddress(ErrorMessage = "Not a valid email address")]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
