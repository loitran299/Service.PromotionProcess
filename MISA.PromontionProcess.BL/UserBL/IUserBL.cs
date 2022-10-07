using MISA.PromotionProcess.Common;
using MISA.PromotionProcess.Common.DTO;

namespace MISA.PromontionProcess.BL.UserBL
{
    public interface IUserBL : IBaseBL<User>
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