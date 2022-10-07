using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.PromontionProcess.API;
using MISA.PromontionProcess.BL.ProductBL;
using MISA.PromontionProcess.BL.UserBL;
using MISA.PromotionProcess.BL.RequestBL;
using MISA.PromotionProcess.Common;
using MISA.PromotionProcess.Common.Model;

namespace MISA.PromotionProcess.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RequestController : BaseController<Request>
    {
        private readonly IRequestBL _requestBL;
        public RequestController(IRequestBL requestBL) : base(requestBL)
        {
            _requestBL = requestBL;
        }
    }
}
