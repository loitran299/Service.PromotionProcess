using MISA.PromontionProcess.BL;
using MISA.PromontionProcess.Common;
using MISA.PromotionProcess.BL.RequestMemberBL;
using MISA.PromotionProcess.Common.DTO;
using MISA.PromotionProcess.Common.Enum;
using MISA.PromotionProcess.Common.Enums;
using MISA.PromotionProcess.Common.Model;
using MISA.PromotionProcess.DL;
using MISA.PromotionProcess.DL.RequestDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.BL.RequestBL
{
    public class RequestBL : BaseBL<Request>, IRequestBL
    {
        #region Field

        private readonly IRequestDL _requesDL;
        private readonly IRequestMemberBL _requesMemberBL;
        #endregion

        #region Constructor

        public RequestBL(IRequestDL requesDL, IRequestMemberBL requesMemberBL) : base(requesDL)
        {
            _requesDL = requesDL;
            _requesMemberBL = requesMemberBL;
        }
        #endregion

        #region Methods
        /// <summary>
        /// API Lấy Nhân viên theo bộ lọc
        /// </summary>
        /// <param name="pageSize">Số bản ghi 1 trang</param>
        /// <param name="pageNumber">Trang đang chọn</param>
        /// <param name="SortBy">Sắp xếp theo thuộc tính truyền vào</param>
        /// <param name="requestFilter">Điều kiện lọc</param>
        /// <returns>Danh sách nhân viên</returns>
        /// Created by: TVLOI (19/08/2022)
        public PagingData<RequestDTO> Filter(int? pageSize, int? pageNumber, string? requestFilter, string? sortBy)
        {
            int offSet = 0;
            int limit = -1;
            string sort = null;
            string where = null;

            PagingData<RequestDTO> pagingData = new PagingData<RequestDTO>(pageNumber);
            if (pageNumber != null && pageSize != null)
            {
                offSet = (int)((pageNumber - 1) * pageSize);
                limit = (int)pageSize;
            }

            if (requestFilter != null)
            {
                where = $"EmployeeID = '{requestFilter}'";
            }
            if (sortBy != null)
            {
                sort = sortBy;
            }
            (IEnumerable<RequestDTO>? employees, pagingData.TotalRecords) = _requesDL.Filter(offSet, limit, sort, where);
            pagingData.CurrentPageRecords = employees.Count();

            if (pageSize != null)
            {
                pagingData.TotalPages = (int?)Math.Ceiling(((decimal)((decimal)pagingData.TotalRecords / pageSize)));
            }
            else
            {
                pagingData.TotalPages = 1;
            }

            pagingData.Data = (List<RequestDTO>)employees;
            return pagingData;
        }

        public List<Request> getByEmployee(string employeeId)
        {
            return _requesDL.getByEmployee(employeeId);
        }

        public List<Request> getByManager(string employeeId)
        {
            return _requesDL.getByManager(employeeId);
        }
        #endregion

        #region Override
        protected override void BeforeSaveAsyn(Request entity)
        {
            entity.RequestID = Guid.NewGuid();
            base.BeforeSaveAsyn(entity);
        }
        #endregion
    }
}
