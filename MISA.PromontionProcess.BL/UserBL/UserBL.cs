using MISA.PromotionProcess.Common;
using MISA.WEB07.CNTT2.LOI.BL;
using MISA.WEB07.CNTT2.LOI.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromontionProcess.BL.UserBL
{

    public class UserBL : BaseBL<User>, IUserBL
    {
        private readonly IBaseDL<User> _baseDL;
        public UserBL(IBaseDL<User> iBaseDL) : base(iBaseDL)
        {
            _baseDL = iBaseDL;
        }

        #region overide
        protected override void BeforeSaveAndUpdate(User entity)
        {
            entity.Password = BCrypt.Net.BCrypt.HashPassword(entity.Password);
            base.BeforeSaveAndUpdate(entity);
        }

        protected override void BeforeSaveAsyn(User entity)
        {
            base.BeforeSaveAsyn(entity);
        }
        #endregion
    }
}
