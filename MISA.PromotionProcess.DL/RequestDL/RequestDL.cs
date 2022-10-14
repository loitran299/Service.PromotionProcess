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

        public List<Request> GetByEmployee(string employeeId)
        {
            using (var connection = new MySqlConnection(this._conn))
            {

                var sql = $"SELECT * FROM request WHERE CreatedEmployeeID = @employeeID";
                var record = connection.Query<Request>(sql, new { employeeID = employeeId }).ToList();
                return record;
            }
        }

        public List<Request> GetByManager(string employeeId)
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
        public Tuple<List<RequestDTO>, int> Filter(int Offset, int Limit, string Sort, RequestFilter requestFilter)
        {
            using (var connection = new MySqlConnection(this._conn))
            {
                var sql = @"Proc_Request_GetPaging_V2";
                var parameter = new { 
                    @Offset = Offset,
                    @Limit = Limit,
                    @Sort = Sort,
                    @EmployeeID = requestFilter.EmployeeID,
                    @FromDate = requestFilter.StartDate,
                    @ToDate = requestFilter.EndDate,
                    @RequestStatus = requestFilter.Status,
                    @IsLoadForManagement = requestFilter.IsManager,
                    @ProcessType = requestFilter.RequestType,
                    @CurrentLevel = requestFilter.CurrentLevel
                };
                var results = connection.QueryMultiple(sql, parameter, commandType: CommandType.StoredProcedure);
                var requests = results.Read<RequestDTO>().ToList();
                var totalRecords = results.Read<int>().First();
                var tuple = new Tuple<List<RequestDTO>, int>((List<RequestDTO>)requests, totalRecords);
                return tuple;
            }
        }

        public RequestDTO GetDtoByID(Guid id)
        {
            using (var connection = new MySqlConnection(this._conn))
            {
                var sql = $"SELECT * FROM view_requestmember_request WHERE RequestMemberID = @id";
                var record = connection.Query<RequestDTO>(sql, new { id = id }).FirstOrDefault();
                return record;
            }
        }


        #endregion
        #region Override
        protected override void AfterSaveAsyn(Request entity)
        {
            RequestMember requestMember = new RequestMember
            {
                EmployeeID = entity.CreatedEmployeeID,
                RequestID = entity.RequestID,
                RequestMemberID = Guid.NewGuid(),
                Role = RoleRequest.Create,
                FinishDate = DateTime.Now,
                Active = true
            };
            _requesMemberDL.Add(requestMember);
            base.AfterSaveAsyn(entity);
        }
        #endregion
    }
}
