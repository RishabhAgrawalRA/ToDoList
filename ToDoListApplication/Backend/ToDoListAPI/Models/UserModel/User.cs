namespace ToDoListAPI.Models.UserModel
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = String.Empty;
        public string PasswordHash { get; set; } = String.Empty;
        public string Roles { get; set; } = String.Empty;
        public string RefreshToken { get; set; } = String.Empty;
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
