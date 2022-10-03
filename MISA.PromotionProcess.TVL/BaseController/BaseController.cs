using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB07.CNTT2.LOI.Api.Helpers;
using MISA.WEB07.CNTT2.LOI.BL;
using MySqlConnector;

namespace MISA.WEB07.CNTT2.LOI.Api
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        #region Field

        private IBaseBL<T> _baseBL;
        public string IdName
        {
            get
            {
                var fistProp = typeof(T).GetProperties().First().Name;
                if (fistProp == null)
                {
                    return fistProp;
                }
                return "Id";
            }
        }

        #endregion

        #region Constructor

        public BaseController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// API Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// Created by: TVLOI (23/08/2022)
        [HttpGet]
        public virtual IActionResult GetAllRecords()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _baseBL.GetAll());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, HandleError.GenerateExceptionResult(ex, HttpContext));
            }
        }

        /// <summary>
        /// API lấy bản ghi theo Id
        /// </summary>
        /// <param name="id">id bản ghi</param>
        /// <returns>1 bản ghi</returns>
        /// Created by: TVLOI (23/08/2022)
        [HttpGet("{id}")]
        public virtual IActionResult GetRecordByID([FromRoute] Guid id)
        {
            try
            {
                var record = _baseBL.GetByID(id);
                if (record != null)
                {
                    return StatusCode(StatusCodes.Status200OK, record);

                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, HandleError.GenerateExceptionResult(ex, HttpContext));
            }

        }

        /// <summary>
        /// API lấy bản ghi theo Id
        /// </summary>
        /// <param name="id">id bản ghi</param>
        /// <returns>1 bản ghi</returns>
        /// Created by: TVLOI (23/08/2022)
        [HttpPost]
        public virtual IActionResult AddRecord([FromBody] T record)
        {
            try
            {
                var validateResult = HandleError.ValidateEntity(ModelState, HttpContext);
                if (validateResult != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, validateResult);
                }

                var effectRow = _baseBL.Add(record);

                if (effectRow != 0)
                {
                    return StatusCode(StatusCodes.Status201Created, effectRow);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e004");
                }
            }
            //catch (MySqlException mySqlException)
            //{
            //    return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateDuplicateCodeErrorResult(mySqlException, HttpContext));
            //}
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, HandleError.GenerateExceptionResult(exception, HttpContext));
            }
        }

        /// <summary>
        /// API update 1 bản ghi
        /// </summary>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: TVLOI (23/08/2022)
        [HttpPut("{id}")]
        public virtual IActionResult UpdateRecord([FromRoute] Guid id, [FromBody] T record)
        {
            try
            {
                var validateResult = HandleError.ValidateEntity(ModelState, HttpContext);
                if (validateResult != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, validateResult);
                }

                var effectRow = _baseBL.Update(id, record);

                if (effectRow != 0)
                {
                    return StatusCode(StatusCodes.Status201Created, effectRow);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e004");
                }
            }
            catch (MySqlException mySqlException)
            {
                return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateDuplicateCodeErrorResult(mySqlException, HttpContext));
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, HandleError.GenerateExceptionResult(exception, HttpContext));
            }
        }

        /// <summary>
        /// API xóa 1 bản ghi
        /// </summary>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: TVLOI (23/08/2022)
        [HttpDelete("{id}")]
        public virtual IActionResult DeleteRecord([FromRoute] Guid id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _baseBL.Delete(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, HandleError.GenerateExceptionResult(ex, HttpContext));
            }

        }
        #endregion
    }
}
