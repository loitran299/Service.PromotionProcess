using MISA.PromotionProcess.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.Common.DTO
{
    public class RequestFilter
    {
        public string EmployeeID { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public RequestStatus? Status { get; set; }

        public RequestType? RequestType { get; set; }

        public Boolean? IsManager { get; set; }

        public Level? CurrentLevel { get; set; }
    }
}
