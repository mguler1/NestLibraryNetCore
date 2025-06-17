using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NestLibraryNetCore.Api.DTOs;
using System.Net;

namespace NestLibraryNetCore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(ResponseDto<T> response)
        {
            if (response.HttpStatusCode == HttpStatusCode.NoContent)

                return new ObjectResult(null) { StatusCode = response.HttpStatusCode.GetHashCode() };

            return new ObjectResult(response) { StatusCode = response.HttpStatusCode.GetHashCode() };

        }
    }
}
