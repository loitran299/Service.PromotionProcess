using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.PromontionProcess.API;
using MISA.PromontionProcess.BL.ProductBL;
using MISA.PromontionProcess.BL.UserBL;
using MISA.PromotionProcess.BL.StructureBL;
using MISA.PromotionProcess.Common;
using MISA.PromotionProcess.Common.Model;

namespace MISA.PromotionProcess.API.Controllers
{
    //[Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StructureController : BaseController<Structure>
    {
        private readonly IStructureBL _structureBL;
        public StructureController(IStructureBL structureBL) : base(structureBL)
        {
            _structureBL = structureBL;
        }
    }
}
