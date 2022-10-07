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
        #region Field

        private readonly IConfiguration _configuration;
        private readonly IUserBL _userBL;
        #endregion

        #region Constructer

        public JwtTokenManager(IConfiguration configuration, IUserBL userBL)
        {
            _configuration = configuration;
            _userBL = userBL;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Tạo jwt token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// CreatedBy: TVLoi (09/04/2022)
        public string? Authenticate(UserDTO user)
        {
            var username = user.Username;
            var password = user.Password;
            var userResult = _userBL.getByUsername(username);
            // check password encode
            if (userResult == null || !BCrypt.Net.BCrypt.Verify(password, userResult.Password))
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
                    new Claim("EmployeeID", userResult.EmployeeID.ToString()),
                    new Claim("EmployeeName", userResult.EmployeeName),
                    new Claim("PositionName", userResult.PositionName),
                    new Claim(ClaimTypes.Role, userResult.Level.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}
