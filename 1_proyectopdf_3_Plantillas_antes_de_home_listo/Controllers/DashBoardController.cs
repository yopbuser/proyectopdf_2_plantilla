using Microsoft.AspNetCore.Mvc;

namespace proyectopdf_1.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
