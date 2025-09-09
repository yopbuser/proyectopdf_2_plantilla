using Microsoft.AspNetCore.Mvc;

namespace proyectopdf_1.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
