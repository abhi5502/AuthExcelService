using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthExcelService.API.Controllers.Staff
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        [HttpGet("staff-dashboard")]
        public IActionResult GetDashboard() => Ok("Staff Dashboard Data");
    }
}
