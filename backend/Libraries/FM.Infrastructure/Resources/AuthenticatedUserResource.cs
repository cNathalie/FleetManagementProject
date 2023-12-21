namespace FM.Infrastructure.Resources
{
    public class AuthenticatedUserResource
    {
        public int UserId { get; set; } 
        public string? Email { get; set; }
        public string? Role { get; set;} 
        public Tokens? Tokens { get; set;}

        public AuthenticatedUserResource(int userId, string? email, string? role, Tokens? tokens)
        {
            UserId = userId;
            Email = email;
            Role = role;
            Tokens = tokens;
        }
    }
}
