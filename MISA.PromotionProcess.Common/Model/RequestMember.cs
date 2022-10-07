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
    [Table("RequestMember")]
    public class RequestMember
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public Guid RequestMemberID { get; set; }

        /// <summary>
        /// ID yêu cầu
        /// </summary>
        public Guid RequestID { get; set; }

        /// <summary>
        /// ID nhân viên tham gia yêu cầu
        /// </summary>
        public Guid EmployeeID { get; set; }

        /// <summary>
        /// Vai trò người tham gia
        /// </summary>
        public RoleRequest Role { get; set; }
        
        /// <summary>
        /// Ngày kết thúc vai trò
        /// </summary>
        public DateTime FinishDate { get; set; }
    }
}
