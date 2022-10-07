using MISA.PromontionProcess.BL;
using MISA.PromotionProcess.Common.Enums;
using MISA.PromotionProcess.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.BL.EmployeeBL
{
    public interface IEmployeeBL : IBaseBL<Employee>
    {
        /// <summary>
        /// Lấy ra nhân viên cấp bậc tiếp theo
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        dynamic getByNextLevel(Level level);
    }
}
