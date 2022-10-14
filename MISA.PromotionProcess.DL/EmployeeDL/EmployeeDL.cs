using Dapper;
using MISA.PromotionProcess.Common.Enums;
using MISA.PromotionProcess.Common.Model;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
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
        public dynamic GetBrowser(Level level)
        {
            using (var connection = new MySqlConnection(this._conn))
            {
                var sql = @"Proc_Employee_Browser";
                var parameter = new
                {
                    @Level = level
                };
                var results = connection.QueryMultiple(sql, parameter, commandType: CommandType.StoredProcedure);
                var requests = results.Read<Employee>().ToList();
                return requests;
            }
        }
    }
}
