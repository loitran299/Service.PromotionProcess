using MISA.PromotionProcess.Common;
using MISA.PromotionProcess.Common.DTO;
using MISA.PromotionProcess.DL;
using MISA.PromotionProcess.DL.UserDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromontionProcess.BL.UserBL
{

    public class UserBL : BaseBL<User>, IUserBL
    {
        #region Field
        private readonly IUserDL _userDL;

        #endregion

        #region Constructer

        public UserBL(IUserDL userDL) : base(userDL)
        {
            _userDL = userDL;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Thêm 1 user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>username</returns>
        /// <exception cref="NotImplementedException"></exception>
        ///  CreatedBy: TVLoi (09/04/2022)
        public string add(User user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lấy ra 1 user
        /// </summary>
        /// <param name="username">Tên đăng nhập</param>
        /// <returns></returns>
        ///  CreatedBy: TVLoi (09/04/2022)
        public UserDTO getByUsername(string username)
        {
            return _userDL.getByUsername(username);
        }
        #endregion

        #region overide
        /// <summary>
        /// Trước khi save hoặc update
        /// </summary>
        /// <param name="entity"></param>
        ///  CreatedBy: TVLoi (09/04/2022)
        protected override void BeforeSaveAndUpdate(User entity)
        {
            entity.Password = BCrypt.Net.BCrypt.HashPassword(entity.Password);
            base.BeforeSaveAndUpdate(entity);
        }

        /// <summary>
        /// Trước khi save
        /// </summary>
        /// <param name="entity"></param>
        ///  CreatedBy: TVLoi (09/04/2022)
        protected override void BeforeSaveAsyn(User entity)
        {
            base.BeforeSaveAsyn(entity);
        }
        #endregion
    }
}
