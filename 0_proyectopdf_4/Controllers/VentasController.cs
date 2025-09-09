using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proyectopdf.Data;
using proyectopdf.Models;
using proyectopdf.Models.ViewModels;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyectopdf.Controllers
{
    public class VentasController : Controller
    {
        private readonly proyectopdfContext _context;

        public VentasController(proyectopdfContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
              return _context.Venta != null ? 
                          View(await _context.Venta.ToListAsync()) :
                          Problem("Entity set 'proyectopdfContext.Venta'  is null.");
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Venta == null)
            {
                return NotFound();
            }

            var venta = await _context.Venta
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVenta,NumeroVenta,DocumentoCliente,NombreCliente,SubTotal,ImpuestoTotal,Total")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venta);
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Venta == null)
            {
                return NotFound();
            }

            var venta = await _context.Venta.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVenta,NumeroVenta,DocumentoCliente,NombreCliente,SubTotal,ImpuestoTotal,Total")] Venta venta)
        {
            if (id != venta.IdVenta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.IdVenta))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Venta == null)
            {
                return NotFound();
            }

            var venta = await _context.Venta
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Venta == null)
            {
                return Problem("Entity set 'proyectopdfContext.Venta'  is null.");
            }
            var venta = await _context.Venta.FindAsync(id);
            if (venta != null)
            {
                _context.Venta.Remove(venta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
          return (_context.Venta?.Any(e => e.IdVenta == id)).GetValueOrDefault();
        }





        //Imprimir
        public IActionResult ImprimirVenta(int id)
        {
            var venta = _context.Venta
                .Include(v => v.DetalleVenta)
                .FirstOrDefault(v => v.IdVenta == id);

            if (venta == null)
            {
                return NotFound("No se encontró la venta con el ID proporcionado.");
            }

            ViewModelVenta modelo = new ViewModelVenta
            {
                numeroventa = venta.NumeroVenta,
                documentocliente = venta.DocumentoCliente,
                nombrecliente = venta.NombreCliente,

                subtotal = venta.SubTotal?.ToString("N0"),
                impuesto = venta.ImpuestoTotal?.ToString("N0"),
                total = venta.Total?.ToString("N0"),

                detalleventa = venta.DetalleVenta.Select(dv => new ViewModelDetalleVenta
                {
                    producto = dv.NombreProducto,
                    cantidad = dv.Cantidad.ToString(),
                    precio = dv.Precio?.ToString("N0"),
                    total = dv.Total?.ToString("N0")
                }).ToList()
            };

            return new ViewAsPdf("ImprimirVenta", modelo)
            {
                //FileName = $"Venta_{modelo.numeroventa}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }

    }
}
