using AutoMapper;
using MISA.PromontionProcess.BL;
using MISA.PromontionProcess.Common;
using MISA.PromotionProcess.BL.EmployeeBL;
using MISA.PromotionProcess.BL.RequestMemberBL;
using MISA.PromotionProcess.Common.DTO;
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
        public int SendRequest(Guid[] requests)
        {
            // request
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<RequestDTO, Request>();
            });
            IMapper iMapper = config.CreateMapper();

            var conf = new MapperConfiguration(cfg => {
                cfg.CreateMap<RequestDTO, RequestMember>();
            });
            IMapper iMap = conf.CreateMapper();
            var result = 0;
            foreach (Guid requestMemberID in requests)
            {   
                RequestDTO requestDTO = _requesDL.GetDtoByID(requestMemberID);

                if (requestDTO.Status == RequestStatus.Draft)
                {
                    Request request = iMapper.Map<Request>(requestDTO);

                    request.Status = RequestStatus.NotApproved;
                    request.RequestDate = DateTime.Now;
                    Employee browser = _employeeBL.GetByID((Guid)requestDTO.EmployeeIDCreatedUserChoose);
                    request.CurrentLevel = browser.Level;

                    // request member
                    RequestMember requestMember = iMap.Map<RequestMember>(requestDTO);
                    requestMember.FinishDate = DateTime.Now;
                    requestMember.Role = RoleRequest.Send;

                    // new member
                    RequestMember newMember = new RequestMember();
                    newMember.RequestMemberID = Guid.NewGuid();
                    newMember.RequestID = requestDTO.RequestID;
                    newMember.EmployeeID = (Guid)requestDTO.EmployeeIDCreatedUserChoose;
                    newMember.Role = RoleRequest.Browse;

                    result = _requesDL.Update(request.RequestID, request);
                    result = _requesMemberBL.Update(requestMember.RequestMemberID, requestMember);
                    result = _requesMemberBL.Add(newMember);
                }
            }
            return result;
        }

        /// <summary>
        /// Xóa yêu cầu
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        public int DeleteMultiple(Guid[] requestMemberIDs)
        {
            // request
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<RequestDTO, Request>();
            });
            IMapper iMapper = config.CreateMapper();
            int result = 0;
            foreach (Guid requestMemberID in requestMemberIDs)
            {
                RequestDTO requestDTO = _requesDL.GetDtoByID(requestMemberID);
                if(requestDTO.Status == RequestStatus.Draft)
                {

                result = _requesMemberBL.InActiveByRequestID(requestDTO.RequestID);

                Request request = iMapper.Map<Request>(requestDTO);
                request.Status = RequestStatus.Deleted;

                result = _requesDL.Update(request.RequestID, request);
                }
            }
            return result;
        }

        /// <summary>
        /// Thu hồi yêu cầu
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        public int RevokeRequests(Guid[] requestMemberIDs)
        {
            // request
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<RequestDTO, Request>();
            });
            IMapper iMapper = config.CreateMapper();
            int result = 0;
            foreach (Guid requestMemberID in requestMemberIDs)
            {
                RequestDTO requestDTO = _requesDL.GetDtoByID(requestMemberID);
                if (requestDTO.CurrentLevel < Level.Manager)
                {

                    Request request = iMapper.Map<Request>(requestDTO);
                    request.Status = RequestStatus.Draft;
                    result = _requesDL.Update(request.RequestID, request);

                    result = _requesMemberBL.InActive(requestDTO.RequestID, (Guid)requestDTO.EmployeeIDCreatedUserChoose);
                }
            }
            return result;
        }

        /// <summary>
        /// Duyệt yêu cầu
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        public int ApprovalRequests(Guid[] requests)
        {
            int result = 0;
            foreach(Guid requestID in requests)
            {
                Request request = _requesDL.GetByID(requestID);
                if(request.LevelCreatedUserChoose == request.CurrentLevel)
                {
                    request.Status = RequestStatus.Approved;
                    request.VoucherCode = this.GenerateCode();

                    RequestMember requestMember = _requesMemberBL.getByRequestAndEmployee(request.RequestID, request.EmployeeIDCreatedUserChoose);
                    requestMember.FinishDate = DateTime.Now;
                    result = _requesDL.Update(request.RequestID, request);
                    result = _requesMemberBL.Update(requestMember.RequestMemberID, requestMember);

                }
            }
            return result;
        }

        private string GenerateCode()
        {
            string newGuid = Guid.NewGuid().ToString();
            newGuid = newGuid.Replace("-", "");
            return newGuid.Substring(0, 8);
        }
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
