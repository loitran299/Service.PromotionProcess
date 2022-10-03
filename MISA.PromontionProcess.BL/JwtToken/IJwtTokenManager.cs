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
        string Authenticate(UserDTO user);
    }
}
