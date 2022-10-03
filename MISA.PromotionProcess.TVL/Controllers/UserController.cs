using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.PromontionProcess.BL.UserBL;
using MISA.PromotionProcess.Common;
using MISA.WEB07.CNTT2.LOI.Api;
using MISA.WEB07.CNTT2.LOI.BL;

namespace MISA.PromotionProcess.API.Controllers
{
    //[Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : BaseController<User>
    {
        private readonly IUserBL _baseBL;
        public UserController(IUserBL baseBL) : base(baseBL)
        {
            _baseBL = baseBL;
        }
    }
}
