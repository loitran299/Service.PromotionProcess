using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.Common
{
    [Table("user")]
    public class User
    {
        #region Properties

        [Key]
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
        public Guid EmployeeID { get; set; }
        #endregion
    }
}
