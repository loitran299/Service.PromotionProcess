using AutoMapper;
using MISA.PromontionProcess.BL;
using MISA.PromontionProcess.Common;
using MISA.PromotionProcess.BL.EmployeeBL;
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
        private readonly IEmployeeBL _employeeBL;
        #endregion

        #region Constructor

        public RequestBL(IRequestDL requesDL, IRequestMemberBL requesMemberBL, IEmployeeBL employeeBL) : base(requesDL)
        {
            _requesDL = requesDL;
            _requesMemberBL = requesMemberBL;
            _employeeBL = employeeBL;
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

            if (sortBy != null)
            {
                sort = sortBy;
            }
            (List<RequestDTO>? requests, pagingData.TotalRecords) = _requesDL.Filter(offSet, limit, sort, requestFilter);
            pagingData.CurrentPageRecords = requests.Count();

            if (pageSize != null)
            {
                pagingData.TotalPages = (int?)Math.Ceiling(((decimal)((decimal)pagingData.TotalRecords / pageSize)));
            }
            else
            {
                pagingData.TotalPages = 1;
            }   
            pagingData.Data = requests;
            return pagingData;
        }

        public List<Request> GetByEmployee(string employeeId)
        {
            return _requesDL.GetByEmployee(employeeId);
        }

        public List<Request> GetByManager(string employeeId)
        {
            return _requesDL.GetByManager(employeeId);
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

            request.Status = RequestStatus.NotApproved;
            request.RequestDate = DateTime.Now;
            Employee browser = _employeeBL.GetByID((Guid)requestDTO.EmployeeIDCreatedUserChoose);
            request.CurrentLevel = browser.Level;

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

        //public int revokeRequest()
        #endregion

        #region Override
        protected override void BeforeSaveAsyn(Request entity)
        {
            entity.RequestID = Guid.NewGuid();
            entity.CreatedDate = DateTime.Now;
            entity.CurrentLevel = Level.Employee;
            entity.RequestDate = DateTime.Now;
            base.BeforeSaveAsyn(entity);
        }
        protected override int BeforeUpdate(Request entity)
        {
            if (entity.Status != RequestStatus.Draft)
            {
                return 0;
            }
            return 1;
            base.BeforeUpdate(entity);
        }
        #endregion
    }
}
