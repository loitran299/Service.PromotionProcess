using Dapper;
using MISA.PromotionProcess.Common.DTO;
using MISA.PromotionProcess.Common.Enums;
using MISA.PromotionProcess.Common.Model;
using MISA.PromotionProcess.DL.RequestMemberDL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.DL.RequestDL
{
    public class RequestDL : BaseDL<Request>, IRequestDL
    {
        #region Field

        private readonly string _conn;
        private readonly IRequestMemberDL _requesMemberDL;
        #endregion

        #region Constructer
        public RequestDL(IRequestMemberDL requestMemberDL)
        {
            _conn = DBContext.ConnectionStrings;
            _requesMemberDL = requestMemberDL;
        }
        #endregion
        #region Methods

        public List<Request> getByEmployee(string employeeId)
        {
            using (var connection = new MySqlConnection(this._conn))
            {

                var sql = $"SELECT * FROM request WHERE CreatedEmployeeID = @employeeID";
                var record = connection.Query<Request>(sql, new { employeeID = employeeId }).ToList();
                return record;
            }
        }

        public List<Request> getByManager(string employeeId)
        {
            using (var connection = new MySqlConnection(this._conn))
            {

                var sql = $"SELECT * FROM view_requestmember_request WHERE EmployeeID = @employeeID";
                var record = connection.Query<Request>(sql, new { employeeID = employeeId }).ToList();
                return record;
            }
        }

        /// API Lấy Nhân viên theo bộ lọc
        /// </summary>
        /// <param name="Offset">index bản ghi lấy đầu tiên</param>
        /// <param name="Limit">Giới hạn số bản ghi</param>
        /// <param name="Sort">Sắp xếp theo thuộc tính truyền vào</param>
        /// <param name="Where">Điều kiện lọc</param>
        /// <returns>Danh sách nhân viên, số bản ghi</returns>
        /// Created by: TVLOI (19/08/2022)
        public Tuple<IEnumerable<Common.DTO.RequestDTO>, int> Filter(int Offset, int Limit, string Sort, string Where)
        {
            using (var connection = new MySqlConnection(this._conn))
            {
                var sql = @"Proc_Request_GetPaging";
                var parameter = new { v_Offset = Offset, v_Limit = Limit, v_Sort = Sort, v_Where = Where , v_Table = "view_requestmember_request"};
                var results = connection.QueryMultiple(sql, parameter, commandType: CommandType.StoredProcedure);
                var requests = results.Read<RequestDTO>().ToList();
                var totalRecords = results.Read<int>().First();
                var tuple = new Tuple<IEnumerable<Common.DTO.RequestDTO>, int>((IEnumerable<Common.DTO.RequestDTO>)requests, totalRecords);
                return tuple;
            }
        }
        #endregion
        #region Override
        protected override void AfterSaveAsyn(Request entity)
        {
            RequestMember requestMember = new RequestMember();
            requestMember.EmployeeID = entity.CreatedEmployeeID;
            requestMember.RequestID = entity.RequestID;
            requestMember.RequestMemberID = Guid.NewGuid();
            requestMember.Role = RoleRequest.Create;
            requestMember.FinishDate = DateTime.Now;
            _requesMemberDL.Add(requestMember);
            base.AfterSaveAsyn(entity);
        }
        #endregion
    }
}
