using AutoMapper;
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
        public PagingData<RequestDTO> Filter(int? pageSize, int? pageNumber, string? sortBy, RequestFilter requestFilter)
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
                where = $"EmployeeID = '{requestFilter.EmployeeID}'";
            }
            if (sortBy != null)
            {
                sort = sortBy;
            }
            (List<RequestDTO>? requests, pagingData.TotalRecords) = _requesDL.Filter(offSet, limit, sort, where);
            pagingData.CurrentPageRecords = requests.Count();

            if (pageSize != null)
            {
                pagingData.TotalPages = (int?)Math.Ceiling(((decimal)((decimal)pagingData.TotalRecords / pageSize)));
            }
            else
            {
                pagingData.TotalPages = 1;
            }   
            List<RequestDTO> result = requests;
            if(requestFilter.Status != RequestStatus.All)
            {
                result = requests.Where(request => request.Status == requestFilter.Status).ToList();
            }

            if(requestFilter.RequestType != null)
            {

                result = result.Where(request => request.Status == RequestStatus.NotApproved).ToList();
            }
            result = result.Where(request => (request.CreatedDate > requestFilter.StartDate) && (request.CreatedDate < requestFilter.EndDate)).ToList();
            pagingData.Data = result;
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

        /// <summary>
        /// Gửi yêu cầu
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int SendRequest(RequestDTO requestDTO)
        {
            // request
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<RequestDTO, Request>();
            });
            IMapper iMapper = config.CreateMapper();
            Request request = iMapper.Map<Request>(requestDTO);

            request.Status = (int?)RequestStatus.NotApproved;
            request.RequestDate = DateTime.Now;

            // request member
            var conf = new MapperConfiguration(cfg => {
                cfg.CreateMap<RequestDTO, RequestMember>();
            });
            IMapper iMap = conf.CreateMapper();
            RequestMember requestMember = iMap.Map<RequestMember>(requestDTO);
            requestMember.FinishDate = DateTime.Now;
            requestMember.Role = RoleRequest.Send;

            // new member
            RequestMember newMember = new RequestMember();
            newMember.RequestMemberID = Guid.NewGuid();
            newMember.RequestID = requestDTO.RequestID;
            newMember.EmployeeID = (Guid)requestDTO.EmployeeIDCreatedUserChoose;
            newMember.Role = RoleRequest.Browse;

            var result = _requesDL.Update(request.RequestID ,request);
            result = _requesMemberBL.Update(requestMember.RequestMemberID ,requestMember);
            result = _requesMemberBL.Add(newMember);
            return result;
        }
        #endregion

        #region Override
        protected override void BeforeSaveAsyn(Request entity)
        {
            entity.RequestID = Guid.NewGuid();
            entity.CreatedDate = DateTime.Now;
            base.BeforeSaveAsyn(entity);
        }
        #endregion
    }
}
