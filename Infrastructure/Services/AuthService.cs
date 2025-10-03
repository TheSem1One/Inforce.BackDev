using Application.Common.Interfaces;
using Application.DTO.Auth;
using Application.DTO.User;
using Domain.Entity;
using Infrastructure.Helper;
using Infrastructure.Persistence;
using Mapster;
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
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email.ToLower().Equals(dto.Email.ToLower()));
            if (user.Password == _hash.HashingPassword(dto.Password))
            {
                var userDto = new UserTokenDto
                {
                    Username = user.Username,
                    Role = user.Role
                };
                return _token.CreateToken(userDto);
            }

            throw new Exception("Password is incorrect");
        }

        public async Task<string> Register(RegisterDto dto)
        {
            if (await EmailExists(dto.Email) && await UserExists(dto.Username))
            {
                dto.Password = _hash.HashingPassword(dto.Password);
                var user = dto.Adapt<User>();
                //var user = new User
                //{
                //   Username = dto.Username,
                // Email = dto.Email,
                //Password = _hash.HashingPassword(dto.Password)
                //};
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();

                var userDto = user.Adapt<UserTokenDto>();
                // var userDto = new UserTokenDto
                // {//
                //    Id = user.Id,
                //   Role = user.Role
                //   };

                return _token.CreateToken(userDto);
            }
            else throw new Exception("User already exists");
        }
        public async Task<bool> EmailExists(string email)
        {
            if (await _db.Users.AnyAsync(user => user.Email.ToLower().Equals(email.ToLower())))
            {
                throw new Exception("Email already exist");
            }
            return true;
        }
        public async Task<bool> UserExists(string username)
        {
            if (await _db.Users.AnyAsync(user => user.Username.ToLower().Equals(username.ToLower())))
            {
                throw new Exception("Username already exist");
            }
            return true;
        }
    }
}
