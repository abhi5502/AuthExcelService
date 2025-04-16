using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthExcelService.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet("admin-dashboard")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetDashboard() => Ok("Admin Dashboard Data");
    }
}
