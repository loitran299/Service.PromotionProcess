using MISA.PromotionProcess.Common;
using MISA.PromotionProcess.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.DL.UserDL
{
    public interface IUserDL : IBaseDL<User>
    {
        /// <summary>
        /// lấy user từ username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>user</returns>
        UserDTO getByUsername(string username);

        /// <summary>
        /// Thêm 1 user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>tên user</returns>
        string add(User user);
    }
}
