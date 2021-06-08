using Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    
    public class MainController : ControllerBase
    {
        public MainController()
        {
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult GetResponse<T>(IDataResult<T> result)
        {
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult GetResponse(IResult result)
        {
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult GetResponse<T>(IDataResult<T> result, int total)
        {
            if (result.Success)
                return Ok(new { data = new { data = result.Data, count = total }, result.Message, result.Success });
            return BadRequest(result);

        }
    }
}