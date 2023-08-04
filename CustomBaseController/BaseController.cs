
using Microsoft.AspNetCore.Mvc;
using Results;
using System.Net;
namespace CustomBaseController
{
    public abstract class MsmControllerBase : ControllerBase
    {
        protected IActionResult ReturnResult(IResult result)
        {
            if (result == null)
            {
                NoContent();
            }

            if (result.StatusCode.HasValue)
            {
                return StatusCode((int)result.StatusCode.Value, result);
            }

            if (result.Success)
            {
                return Ok(result);
            }

            return Problem(result.Message, HttpContext.Request.Host.Value, (int)(result.StatusCode ?? HttpStatusCode.BadRequest));
        }

        protected string GetLanguage()
        {
            string text = "";
            return (string?)base.Request.Headers["lng"] == null ? (string?)base.Request.Headers["Accept-Language"] : string.Empty;
        }

        protected int GetUserId()
        {
            return int.Parse(base.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")!.Value);
        }
    }
}