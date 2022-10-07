using MISA.PromotionProcess.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.Common.Model
{
    [Table("employee")]
    public class Employee
    {
        /// <summary>
        /// ID Nhân viên
        /// </summary>
        /// CreatedBy: TVLOI (10/05/2022)
        [Key]
        public Guid EmployeeID { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        /// CreatedBy: TVLOI (10/05/2022)
        public string EmployeeName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        /// CreatedBy: TVLOI (10/05/2022)
        public string? Email { get; set; }

        /// <summary>
        /// Cấp bậc nhân viên
        /// </summary>
        /// CreatedBy: TVLOI (10/05/2022)
        public Level Level { get; set; }

        /// <summary>
        /// ID cơ cấu
        /// </summary>
        /// CreatedBy: TVLOI (10/05/2022)
        public Guid StructureID { get; set; }

        /// <summary>
        /// Tên vị trí công việc
        /// </summary>
        /// CreatedBy: TVLOI (10/05/2022)
        public string? PositionName { get; set; }
    }
}
