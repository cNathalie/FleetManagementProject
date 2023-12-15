namespace FM.Domain.Models
{
    public class DUserRefreshToken
    {
        public int RefreshTokenId { get; set; }
        public int UserId { get; set; }
        public string? RefreshToken { get; set; }
        public bool IsActive { get; set; } = true;

        public DUserRefreshToken(int userId, string? refreshToken, bool isActive)
        {
            UserId = userId;
            RefreshToken = refreshToken;
            IsActive = isActive;
        }

        public DUserRefreshToken()
        {
        }
    }
}
