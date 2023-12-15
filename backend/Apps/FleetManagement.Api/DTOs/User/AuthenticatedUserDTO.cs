namespace FleetManagement.Api.DTOs.User
{
    public class AuthenticatedUserDTO
    {
        public int UserId { get; set;}
        public string? Email { get; set; } 
        public string? Role { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
