using Microsoft.AspNetCore.Mvc;

namespace AuthExcelService.WebApp.Areas.Staff.Controllers
{
    public class StaffDashboardController : StaffBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
