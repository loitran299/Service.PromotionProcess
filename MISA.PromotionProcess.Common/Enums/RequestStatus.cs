using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.Common.Enums
{
    public enum RequestStatus
    {
        All = 1,

        // Bản nháp
        Draft = 2,

        // chưa duyệt
        NotApproved = 3,

        //đã duyệt
        Approved = 4,

        //đã từ chối
        Refused = 5,

        //Đã gửi cho KH
        Sended = 6,
    }
}
