using MISA.PromotionProcess.Common;
using MISA.WEB07.CNTT2.LOI.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.DL.UserDL
{
    public class UserDL : BaseDL<User>, IUserDL
    {
        #region Field

        private readonly string _conn;
        #endregion

        #region Constructer
        public UserDL()
        {
            _conn = DBContext.ConnectionStrings;
        }
        #endregion

        #region Method

        public string add(User user)
        {
            throw new NotImplementedException();
        }

        public User getByUsername(string username)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region overide
        protected override void BeforeSaveAsyn(User entity)
        {

            entity.Password = BCrypt.Net.BCrypt.HashPassword(entity.Password);

            base.BeforeSaveAsyn(entity);
        }
        #endregion
    }
}
