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

        public dynamic getByNextLevel(Level level)
        {
            //if(level < Level.TeamLeader)
            //{
            //    level = Level.TeamLeader;
            //}else if(level < Level.Manager)
            //{
            //    level = Level.Manager;
            //}else
            //{
            //    level = Level.GeneralManager;
            //}
            List<Employee> employees = (List<Employee>)_employeeDL.GetAll();
            while(level < Level.GeneralManager)
            {
                if(employees.Any(x => x.Level == level))
                {
                    break;
                }
                level += 5;
            }
            return employees.FindAll(x => x.Level == level);
        }
    }
}
