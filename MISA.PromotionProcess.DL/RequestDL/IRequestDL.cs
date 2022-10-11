using MISA.PromotionProcess.Common.DTO;
using MISA.PromotionProcess.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.DL.RequestDL
{
    public interface IRequestDL : IBaseDL<Request>
    {
        /// <summary>
        /// Lấy request cho nhân viên bằng id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        List<Request> getByEmployee(string employeeId);

        /// <summary>
        /// Lấy request cho quản lý bằng id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        List<Request> getByManager(string employeeId);

        /// <summary>
        /// API Lấy Nhân viên theo bộ lọc
        /// </summary>
        /// <param name="Offset">index bản ghi lấy đầu tiên</param>
        /// <param name="Limit">Giới hạn số bản ghi</param>
        /// <param name="Sort">Sắp xếp theo thuộc tính truyền vào</param>
        /// <param name="Where">Điều kiện lọc</param>
        /// <returns>Danh sách nhân viên, số bản ghi</returns>
        /// Created by: TVLOI (19/08/2022)
        Tuple<List<RequestDTO>, int> Filter(int Offset, int Limit, string Sort, string Where);
    }
}
