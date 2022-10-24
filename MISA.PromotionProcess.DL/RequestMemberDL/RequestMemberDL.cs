using Dapper;
using MISA.PromotionProcess.Common.Model;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.DL.RequestMemberDL
{
    public class RequestMemberDL : BaseDL<RequestMember>, IRequestMemberDL
    {
        #region Field

        private readonly string _conn;
        #endregion

        #region Constructer
        public RequestMemberDL()
        {
            _conn = DBContext.ConnectionStrings;
        }
        #endregion

        public RequestMember getByRequestAndEmployee(Guid requestID, Guid employeeID)
        {
            using (var connection = new MySqlConnection(this._conn))
            {
                var sql = $"SELECT * FROM requestmember r WHERE r.RequestID = @Request AND r.EmployeeID = @Employee";
                var parameter = new
                {
                    Request = requestID,
                    Employee = employeeID,
                };
                var requestMember = connection.Query<RequestMember>(sql, parameter).FirstOrDefault();
                return (RequestMember)requestMember;
            }
        }
    

        public int inActive(Guid requestID, Guid employeeID)
        {
            using var connection = new MySqlConnection(this._conn);
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                var sql = $"Proc_RequestMember_InActive_Revoke";
                var parameter = new
                {
                    @RequestID = requestID,
                    @EmployeeID = employeeID,
                };
                var numberOfAffectedRows = connection.Execute(sql, parameter, commandType: CommandType.StoredProcedure, transaction: transaction);
                transaction.Commit();
                return numberOfAffectedRows;
            }
        }
        public int inActiveByRequestID(Guid requestID)
        {
            using var connection = new MySqlConnection(this._conn);
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                var sql = $"Proc_RequestMember_InActive";
                var parameter = new
                {
                    @RequestID = requestID,
                };
                var numberOfAffectedRows = connection.Execute(sql, parameter, commandType: CommandType.StoredProcedure, transaction: transaction);
                transaction.Commit();
                return numberOfAffectedRows;
            }
        }
    }
}
