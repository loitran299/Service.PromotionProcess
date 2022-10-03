using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.CNTT2.LOI.Core.DTO
{
    /// <summary>
    /// Đối tượng trả về của phân trang
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagingData<T>
    {
        #region Constructor
        public PagingData(int? currentPage)
        {
            if (!currentPage.HasValue)
            {
                CurrentPage = 1;
            }
            else
            {
                CurrentPage = currentPage;
            }

        }

        #endregion

        #region Properties
        /// <summary>
        /// Tổng số bản ghi thỏa mãn điều kiện sdd
        /// </summary>
        public int? TotalRecords { get; set; }

        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int? TotalPages { get; set; } = 0;

        /// <summary>
        /// Trang hiện tại
        /// </summary>
        public int? CurrentPage { get; set; } = 0;

        /// <summary>
        /// Số bản ghi của trang
        /// </summary>
        public int? CurrentPageRecords { get; set; } = 0;

        /// <summary>
        /// Mảng đối tượng thỏa mãn điều kiện lọc và phân trang
        /// </summary>
        public List<T> Data { get; set; } = new List<T>();

        #endregion
    }
}
