namespace ToDoListAPI.Dto
{
    public class TokenResponseDto
    {
        public string AccessToken { get; set; } = String.Empty;
        public string RefreshToken { get; set; } = String.Empty;
    }
}
