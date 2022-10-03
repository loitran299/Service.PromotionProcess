using MISA.PromotionProcess.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.Common
{
    public class User
    {
        #region Property

        /// <summary>
        /// Tên tài khoản
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Vai trò
        /// </summary>
        public Role Role { get; set; }
        #endregion
    }
}
