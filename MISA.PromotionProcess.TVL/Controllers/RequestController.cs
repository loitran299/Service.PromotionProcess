using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.PromontionProcess.API;
using MISA.PromontionProcess.BL.ProductBL;
using MISA.PromontionProcess.BL.UserBL;
using MISA.PromotionProcess.BL.RequestBL;
using MISA.PromotionProcess.Common;
using MISA.PromotionProcess.Common.DTO;
using MISA.PromotionProcess.Common.Enums;
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

        /// <summary>
        /// Lấy danh sách nhân viên theo bộ lọc
        /// </summary>
        /// <param name="pageSize">Số bản ghi 1 trang</param>
        /// <param name="pageNumber">Trang số?</param>
        /// <param name="employeeFilter">Lọc theo mã và tên nhân viên</param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <returns>Danh sách nhân viên</returns>
        /// CreatedBy: TVLOI 23/08/2001
        [HttpPost("Fillter")]
        public IActionResult Fillter([FromQuery] int? pageSize, [FromQuery] int? pageNumber, [FromQuery] string? sortBy, [FromBody]RequestFilter requestFilter)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _requestBL.Filter(pageSize, pageNumber, sortBy, requestFilter));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, HandleError.GenerateExceptionResult(ex, HttpContext));
            }
        }

        [HttpPut("SendRequest")]
        public IActionResult SendRequest([FromBody]Guid[] requests)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _requestBL.SendRequest(requests));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, HandleError.GenerateExceptionResult(ex, HttpContext));
            }
        }

        [HttpPut("Multiple")]
        public IActionResult DeleteMultiple([FromBody]Guid[] requests)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _requestBL.DeleteMultiple(requests));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, HandleError.GenerateExceptionResult(ex, HttpContext));
            }
        }

        [HttpPut("Revoke")]
        public IActionResult Revoke([FromBody] Guid[] requests)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _requestBL.RevokeRequests(requests));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, HandleError.GenerateExceptionResult(ex, HttpContext));
            }
        }


        [HttpPut("Approval")]
        public IActionResult Approval([FromBody] Guid[] requests)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _requestBL.ApprovalRequests(requests));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, HandleError.GenerateExceptionResult(ex, HttpContext));
            }
        }

        [HttpPut("Transfer")]
        public IActionResult TransferRequest([FromBody] Guid[] requests, [FromQuery] Guid employeeIdChooser, [FromQuery] Level level)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _requestBL.TransferRequests(requests,employeeIdChooser,level));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, HandleError.GenerateExceptionResult(ex, HttpContext));
            }
        }

        [HttpPut("Refuse")]
        public IActionResult RefuseRequest([FromBody] Guid[] requests, [FromQuery] string reasonForRefusal)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _requestBL.RefuseRequests(requests, reasonForRefusal));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, HandleError.GenerateExceptionResult(ex, HttpContext));
            }
        }
    }
}
