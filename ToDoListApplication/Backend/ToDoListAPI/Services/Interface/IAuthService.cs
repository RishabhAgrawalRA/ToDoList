using ToDoListAPI.Dto;
using ToDoListAPI.Models.UserModel;

namespace ToDoListAPI.Services.Interface
{
    public interface IAuthService
    {
        Task<TokenResponseDto?> LoginAsync(UserDto request);
        Task<User?> RegisterAsync(UserDto request);
        Task<TokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
    }
}