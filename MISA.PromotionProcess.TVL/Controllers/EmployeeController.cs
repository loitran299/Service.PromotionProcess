using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.PromontionProcess.API;
using MISA.PromontionProcess.BL.ProductBL;
using MISA.PromontionProcess.BL.UserBL;
using MISA.PromotionProcess.BL.EmployeeBL;
using MISA.PromotionProcess.Common;
using MISA.PromotionProcess.Common.Enums;
using MISA.PromotionProcess.Common.Model;

namespace MISA.PromotionProcess.API.Controllers
{
    //[Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController<Employee>
    {
        private readonly IEmployeeBL _employeeBL;
        public EmployeeController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            _employeeBL = employeeBL;
        }

        [HttpGet("Browser")]
        public IActionResult GetBrowser(Level level)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _employeeBL.GetBrowser(level));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, HandleError.GenerateExceptionResult(ex, HttpContext));
            }
        }
    }
}
