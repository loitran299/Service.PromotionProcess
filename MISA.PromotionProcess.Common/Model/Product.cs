using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.Common.Model
{
    /// <summary>
    /// Sản phẩm
    /// </summary>
    /// CreatedBy: TVLOI (10/05/2022)
    public class Product
    {
        /// <summary>
        /// ID Sản phẩm
        /// </summary>
        public Guid ProductID { get; set; }

        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        public string ProductName { get; set; }
    }
}
