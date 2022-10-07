using MISA.PromotionProcess.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.Common.DTO
{
    public class UserDTO
    {
        public Guid EmployeeID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string? EmployeeName { get; set; }

        public string? PositionName { get; set; }

        public Level Level { get; set; }
    }
}
