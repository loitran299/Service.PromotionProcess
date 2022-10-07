using Dapper;
using MISA.PromotionProcess.Common.Enums;
using MISA.PromotionProcess.Common.Model;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.DL.EmployeeDL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
        #region Field

        private readonly string _conn;
        #endregion

        #region Constructer
        public EmployeeDL()
        {
            _conn = DBContext.ConnectionStrings;
        }
        #endregion
        public dynamic getByLevel(Level level)
        {
            using (var connection = new MySqlConnection(this._conn))
            {
                var sql = $"SELECT * FROM employee WHERE Level = @level";
                var record = connection.Query<Employee>(sql, new { level = level }).FirstOrDefault();
                return record;
            }
        }
    }
}
