using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.PromontionProcess.API;
using MISA.PromontionProcess.BL.ProductBL;
using MISA.PromontionProcess.BL.UserBL;
using MISA.PromotionProcess.Common;
using MISA.PromotionProcess.Common.Model;

namespace MISA.PromotionProcess.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : BaseController<Product>
    {
        private readonly IProductBL _productBL;
        public ProductController(IProductBL productBL) : base(productBL)
        {
            _productBL = productBL;
        }
    }
}
