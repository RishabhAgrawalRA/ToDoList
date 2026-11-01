using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ToDoListAPI.Data;
using ToDoListAPI.Dto;
using ToDoListAPI.Models.UserModel;
using ToDoListAPI.Services.Interface;

namespace ToDoListAPI.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserDbContext _userDbContext;

        public AuthService(IConfiguration _configuration, UserDbContext _userDbContext)
        {
            this._configuration = _configuration;
            this._userDbContext = _userDbContext;
        }

        public async Task<User?> RegisterAsync(UserDto request)
        {
            var existingUser = await _userDbContext.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (existingUser != null)
            {
                return null; // User already exists
            }

            var user = new User();
            user.Username = request.Username;
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, request.Password);

            await _userDbContext.Users.AddAsync(user);
            await _userDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<string?> LoginAsync(UserDto request)
        {
            User? user = await _userDbContext.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null)
            {
                return null; // User not found
            }
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                return null; // Incorrect password
            }
            string token = CreateToken(user);
            return token;
        }


        private string CreateToken(User user)
        {
            //claims are pieces of information about the user that are encoded in the token.
            //In this case, we are adding the username as a claim.
            //Later will add roles and permissions like Admin, User, etc.
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Roles)
            };

            //The key is used to sign the token, ensuring that it cannot be tampered with.
            //The signing credentials specify the algorithm used for signing the token.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Token")!));

            //Creds are the signing credentials, which include the key and the algorithm used for signing the token.
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            //The token descriptor contains information about the token, including the issuer, audience, claims, expiration time, and signing credentials.
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("Jwt:Issuer"),
                audience: _configuration.GetValue<string>("Jwt:Audience"),
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
