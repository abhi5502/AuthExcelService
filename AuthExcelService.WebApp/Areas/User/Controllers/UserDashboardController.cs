using Microsoft.AspNetCore.Mvc;

namespace AuthExcelService.WebApp.Areas.User.Controllers
{
    public class UserDashboardController : UserBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
