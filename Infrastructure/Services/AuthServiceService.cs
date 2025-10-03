using Application.Common.Interfaces;
using Application.Features.Auth.Dto;
using Domain.Entities;
using Infrastructure.Helper;
using Infrastructure.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class AuthServiceService(DatabaseContext db, Hashing hash, TokenManipulation token) : IAuthService
    {
        private readonly TokenManipulation _token = token;
        private readonly Hashing _hash = hash;
        private readonly DatabaseContext _db = db;

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email.ToLower().Equals(dto.Email.ToLower()));
            if (user.Password != _hash.HashingPassword(dto.Password))
            {
                throw new Exception("User with this email or password not found");
            }

            var userDto = user.Adapt<UserTokenDto>();

            return _token.CreateToken(userDto);
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
            {
                throw new Exception("Password and Confirm Password do not match");
            }
            dto.Password = _hash.HashingPassword(dto.Password);
            var user = dto.Adapt<User>();

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            var userDto = user.Adapt<UserTokenDto>();

            return _token.CreateToken(userDto);
        }
    }
}
