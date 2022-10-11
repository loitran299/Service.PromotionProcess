﻿using MISA.PromontionProcess.BL;
using MISA.PromontionProcess.Common;
using MISA.PromotionProcess.Common.DTO;
using MISA.PromotionProcess.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.BL.RequestBL
{
    public interface IRequestBL : IBaseBL<Request>
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
        /// <param name="pageSize">Số bản ghi 1 trang</param>
        /// <param name="pageNumber">Trang đang chọn</param>
        /// <param name="SortBy">Sắp xếp theo thuộc tính truyền vào</param>
        /// <param name="requestFilter">Điều kiện lọc</param>
        /// <returns>Danh sách nhân viên</returns>
        /// Created by: TVLOI (19/08/2022)
        PagingData<RequestDTO> Filter(int? pageSize, int? pageNumber, string? requestFilter, string? SortBy);

        /// <summary>
        /// Gửi yêu cầu
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        int SendRequest(RequestDTO requestDTO);
    }
}