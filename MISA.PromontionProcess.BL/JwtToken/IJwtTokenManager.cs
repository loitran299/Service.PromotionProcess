using MISA.PromotionProcess.Common;
using MISA.PromotionProcess.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromontionProcess.BL.JwtToken
{
    public interface IJwtTokenManager
    {
        /// <summary>
        /// Tạo Jwt token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        ///  CreatedBy: TVLoi (09/04/2022)
        string Authenticate(UserDTO user);
    }
}
