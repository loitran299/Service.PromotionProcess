using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.Common.Enums
{
    public enum RequestStatus
    {
        // Bản nháp
        Draft = 1,

        // chưa duyệt
        NotApproved = 2,

        //đã duyệt
        Approved = 3,

        //đã từ chối
        Refused = 4,

        //Đã gửi cho KH
        Sended = 5,
    }
}
