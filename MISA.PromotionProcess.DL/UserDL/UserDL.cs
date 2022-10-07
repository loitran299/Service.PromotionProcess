using Dapper;
using MISA.PromotionProcess.Common;
using MISA.PromotionProcess.Common.DTO;
using MySqlConnector;
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

        public UserDTO getByUsername(string username)
        {
            using (var connection = new MySqlConnection(this._conn))
            {

                var sql = $"SELECT * FROM view_user_employee WHERE Username = @username";
                var record = connection.Query<UserDTO>(sql, new { username = username }).FirstOrDefault();
                return record;
            }
        }
        #endregion

        #region overide
        #endregion
    }
}
