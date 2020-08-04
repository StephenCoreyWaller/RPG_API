using System;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI_RPG.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace WebAPI_RPG.Data
{
    public class AuthRepository : IAuthRepository
    {

        private readonly DataContext _context;

        private readonly IConfiguration _configuration;
        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }
        public async Task<ServiceWrapper<string>> Login(string username, string password)
        {

            ServiceWrapper<string> response = new ServiceWrapper<string>();
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Name.ToLower().Equals(username.ToLower()));

            if (user == null)
            {

                response.DidSend = false;
                response.Message = "User Not Found.";
            }
            else if (!VerifyPassword(password, user.PasswordHash, user.PsswordSalt))
            {

                response.DidSend = false;
                response.Message = "Password incorrect.";
            }
            else
            {

                response.Data = CreateToken(user);
            }
            return response;
        }
        public async Task<ServiceWrapper<int>> Register(User user, string password)
        {
            ServiceWrapper<int> wrapper = new ServiceWrapper<int>();

            if (await UserExist(user.Name))
            {

                wrapper.Message = "User already exist.";
                wrapper.DidSend = false;
                return wrapper;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PsswordSalt = passwordSalt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            wrapper.Data = user.id;

            return wrapper;
        }
        public async Task<bool> UserExist(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Name.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {

            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != passwordHash[i])
                    {

                        return false;
                    }
                }
                return true;
            }
        }
        private string CreateToken(User user)
        {
            List<Claim> claim = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
            );
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature); 

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor{

                Subject = new ClaimsIdentity(claim), 
                Expires = DateTime.Now.AddDays(1), 
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}