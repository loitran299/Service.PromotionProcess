﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.Common.Model
{
    public class Request
    {
        /// <summary>
        /// ID yêu cầu
        /// </summary>
        public Guid? RequestID { get; set; }

        /// <summary>
        /// ID nhân viên tạo yêu cầu
        /// </summary>
        public Guid? CreatedEmployeeID { get; set; }

        /// <summary>
        /// Ngày tạo yêu cầu
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Ngày gửi yêu cầu
        /// </summary>
        public DateTime? RequestDate { get; set; }

        /// <summary>
        /// Cấp đang duyệt
        /// </summary>
        public int? CurrentLevel { get; set; }

        /// <summary>
        /// ID sản phẩm
        /// </summary>
        public Guid? ProductID { get; set; }

        /// <summary>
        /// Ngày bắt đầu có hiệu lực
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Ngày hết hiệu lực
        /// </summary>
        public DateTime? ExpiredDate { get; set; }

        /// <summary>
        /// Giảm giá cho (sản phẩm, toàn bộ đơn hàng)
        /// </summary>
        public int? DiscountFor { get; set; }

        /// <summary>
        /// Áp dụng cho(bán mới, gia hạn ....)
        /// </summary>
        public int? ApplyFor { get; set; }

        /// <summary>
        /// Trạng thái của yêu cầu
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// Loại giảm giá
        /// </summary>
        public int? DiscountType { get; set; }

        /// <summary>
        /// Giá trước khi áp mã
        /// </summary>
        public int? PriceBefore { get; set; }

        /// <summary>
        /// Lý do xin mã
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Danh mục
        /// </summary>
        public int? Category { get; set; }

        /// <summary>
        /// Cấp xin duyệt
        /// </summary>
        public int? LevelCreatedUserChoose { get; set; }

        /// <summary>
        /// Nhân viên xin duyệt
        /// </summary>
        public Guid? EmployeeIDCreatedUserChoose { get; set; }

        /// <summary>
        /// Lý do từ chối
        /// </summary>
        public string? ReasonForRefusal { get; set; }

        /// <summary>
        /// Mã cộng tác viên
        /// </summary>
        public string CollaboratorCode { get; set; }

        //---------------------------------Customer Infomation --------------------------------------//
        /// <summary>
        /// Số CMT
        /// </summary>
        public string? CustomerIdentity { get; set; }

        /// <summary>
        /// Tên khách hàng
        /// </summary>
        public string? CustomerName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Mã ngân sách
        /// </summary>
        public string? BudgetCode { get; set; }

        /// <summary>
        /// Ngày thành lập
        /// </summary>
        public DateTime? EstablishDate { get; set; }

        /// <summary>
        /// Người liên hệ
        /// </summary>
        public string? ContactBy { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }
    }
}