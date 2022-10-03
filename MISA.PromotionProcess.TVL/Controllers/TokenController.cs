using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.PromontionProcess.BL.JwtToken;
using MISA.PromotionProcess.Common;
using MISA.PromotionProcess.Common.DTO;

namespace MISA.PromotionProcess.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly IJwtTokenManager _tokenManager;

        public TokenController(IJwtTokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] UserDTO user)
        {
            var token = _tokenManager.Authenticate(user);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
