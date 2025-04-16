using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthExcelService.WebApp.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class UserBaseController : Controller
    {
        
    }
}
