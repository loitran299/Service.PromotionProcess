using MISA.PromotionProcess.Common.Enums;
using MISA.PromotionProcess.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.DL.EmployeeDL
{
    public interface IEmployeeDL : IBaseDL<Employee>
    {
        /// <summary>
        /// Lấy ra nhân viên cấp bậc tiếp theo
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        dynamic GetBrowser(Level level);
    }
}
