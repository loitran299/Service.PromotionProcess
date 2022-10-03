using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MISA.PromontionProcess.BL.UserBL;
using MISA.PromotionProcess.Common;
using MISA.PromotionProcess.Common.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace MISA.PromontionProcess.BL.JwtToken
{
    public class JwtTokenManager : IJwtTokenManager
    {
        private readonly IConfiguration _configuration;
        private readonly IUserBL _userBL;
        public JwtTokenManager(IConfiguration configuration, IUserBL userBL)
        {
            _configuration = configuration;
            _userBL = userBL;
        }
        public string? Authenticate(UserDTO user)
        {
            var username = user.Username;
            var password = user.Password;
            var users = _userBL.GetAll();
            if (!users.Any(x => x.Username.Equals(username) && BCrypt.Net.BCrypt.Verify(user.Password, x.Password)))
            {
                return null;
            }
            var key = _configuration.GetValue<string>("JwtConfig:Key");

            var keyBytes = Encoding.ASCII.GetBytes(key);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim("Role","admin")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
