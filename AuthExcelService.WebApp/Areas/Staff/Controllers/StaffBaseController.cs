using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthExcelService.WebApp.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Authorize(Roles = "Staff")]
    public class StaffBaseController : Controller
    {
        
    }
}
