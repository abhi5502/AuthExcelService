using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthExcelService.API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("user-dashboard")]
        public IActionResult GetDashboard() => Ok("user Dashboard Data");
    }
}
