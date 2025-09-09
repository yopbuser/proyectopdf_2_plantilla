using Microsoft.AspNetCore.Mvc;

namespace proyectopdf_1.Controllers
{
    public class VentaController : Controller
    {
        public IActionResult NuevaVenta()
        {
            return View();
        }

        public IActionResult HistorialVenta()
        {
            return View();
        }
    }
}
