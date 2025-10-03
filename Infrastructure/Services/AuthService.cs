using Application.Common.Interfaces;
using Application.DTO.Auth;
using Application.DTO.User;
using Domain.Entity;
using Infrastructure.Helper;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class AuthService(DatabaseContext db, Hashing hash, TokenManipulation token) : IAuth
    {
        private readonly TokenManipulation _token = token;
        private readonly Hashing _hash = hash;
        private readonly DatabaseContext _db = db;
        public async Task<string> Login(LoginDto dto)
        {
            await UserExists(dto.Email);

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email.ToLower().Equals(dto.Email.ToLower()));

            if (_hash.HashingPassword(user.Password) == _hash.HashingPassword(dto.Password))
            {
                var userDto = new UserTokenDto
                {
                    Id = user.Id,
                    Role = user.Role
                };
                return _token.CreateToken(userDto);
            }

            throw new Exception("Password is incorrect");
        }

        public async Task<string> Register(RegisterDto dto)
        {
            if (await UserExists(dto.Email))
            {
                var user = new User
                {
                    Username = dto.Username,
                    Email = dto.Email,
                    Password = _hash.HashingPassword(dto.Password)
                };
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();

                var userDto = new UserTokenDto
                {
                    Id = user.Id,
                    Role = user.Role
                };

                return _token.CreateToken(userDto);
            }
            else throw new Exception("User already exists");
        }
        public async Task<bool> UserExists(string email)
        {
            if (await _db.Users.AnyAsync(user => user.Email.ToLower().Equals(email.ToLower())))
            {
                throw new Exception("User does not exist");
            }
            return true;
        }
    }
}
