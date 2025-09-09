using Microsoft.AspNetCore.Mvc;

namespace proyectopdf_1.Controllers
{
    public class NegocioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
