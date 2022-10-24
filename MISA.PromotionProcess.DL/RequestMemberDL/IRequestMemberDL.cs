using MISA.PromotionProcess.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.DL.RequestMemberDL
{
    public interface IRequestMemberDL : IBaseDL<RequestMember>
    {
        /// <summary>
        /// Ngừng hoạt động member them gia request với requestID
        /// </summary>
        /// <param name="requestID"></param>
        /// <returns></returns>
        int inActiveByRequestID(Guid requestID);
        
        /// <summary>
        /// Ngừng hoạt động member them gia
        /// </summary>
        /// <param name="requestID"></param>
        /// <returns></returns>
        int inActive(Guid requestID, Guid employeeID);
        
        /// <summary>
        /// Lấy bới mã yêu cầu và mã nhân viên
        /// </summary>
        /// <param name="requestID"></param>
        /// <returns></returns>
        RequestMember getByRequestAndEmployee(Guid requestID, Guid employeeID);
    }
}
