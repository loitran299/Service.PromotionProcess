using MISA.PromontionProcess.BL;
using MISA.PromotionProcess.Common.Enums;
using MISA.PromotionProcess.Common.Model;
using MISA.PromotionProcess.DL;
using MISA.PromotionProcess.DL.EmployeeDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.BL.EmployeeBL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        private readonly IEmployeeDL _employeeDL;
        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            _employeeDL = employeeDL;
        }

        /// <summary>
        /// Lấy ra danh sách nhân viên có thể duyệt yêu cầu
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public dynamic GetBrowser(Level level)
        {
            List<Employee> employees = (List<Employee>)_employeeDL.GetBrowser(level);
            return employees;
        }
    }
}
