using Microsoft.AspNetCore.Mvc;

namespace AuthExcelService.WebApp.Areas.Admin.Controllers
{
    public class AdminDashboardController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
