using MISA.PromotionProcess.Common.Model;
using MISA.PromotionProcess.DL;
using MISA.PromotionProcess.DL.ProductDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromontionProcess.BL.ProductBL
{
    public class ProductBL : BaseBL<Product>, IProductBL
    {
        private readonly IProductDL _productDL;
        public ProductBL(IProductDL productDL) : base(productDL)
        {
            _productDL = productDL;
        }
    }
}
