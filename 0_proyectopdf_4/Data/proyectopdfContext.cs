using Microsoft.EntityFrameworkCore;
using proyectopdf.Data;
using proyectopdf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Utiliza Entity Framework Core para interactuar con una base de datos, define el contexto de base de datos.
//Se utiliza en los controladores, para acceder a los datos:
//private readonly proyectopdfContext _context;
namespace proyectopdf.Data
{
    public class proyectopdfContext : DbContext
    {
        //Permite inyectar opciones de configuración (como la cadena de conexión) desde el archivo Startup.cs o Program.cs.
        public proyectopdfContext (DbContextOptions<proyectopdfContext> options)
            : base(options)
        {
        }

        // Especifica una tabla virtual llamada Venta que representa la entidad Venta en la base de datos.
        // Lo cual permite realizar algo como: var ventas = _context.Venta.ToList();
        public DbSet<proyectopdf.Models.Venta> Venta { get; set; } = default!;
    }
}

// Se configura de esta forma: En Program.cs o Startup.cs, se registra así:
//services.AddDbContext<proyectopdfContext>(options =>
//    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
