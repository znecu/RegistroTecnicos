using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<Tecnicos> Tecnicos { get; set; }

    public DbSet<TiposTecnicos> TiposTecnicos { get; set; }

    public DbSet<Clientes> Clientes { get; set; }

    public DbSet<Trabajos> Trabajos { get; set; }
    public DbSet<Prioridades> Prioridades { get; set; }
    public DbSet<TrabajoDetalle> TrabajoDetalles { get; set; }

    public DbSet<Articulos> Articulos { get; set; }
    public DbSet<Cotizaciones> Cotizaciones { get; set; }
    public DbSet<CotizacionesDetalle> CotizacionesDetalle { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Articulos>().HasData(
            new List<Articulos>()
            {
                new()
                {
                    ArticuloId = 1,
                    Descripcion = "Cable RJ45",
                    Costo = 900,
                    Precio = 1500,
                    Existencia = 50,

                },

                new() {

                    ArticuloId = 2,
                    Descripcion = "Papel de impresora",
                    Costo = 3000,
                    Precio = 100,
                    Existencia = 100,

                },

                new()
                {
                    ArticuloId = 3,
                    Descripcion = "Pin de carga",
                    Costo = 3500,
                    Precio = 5000,
                    Existencia = 45,

                },

                new() {
                    ArticuloId = 4,
                    Descripcion = "Cámara de vigilancia",
                    Costo = 9000,
                    Precio = 11000,
                    Existencia = 50,
                },

                new() {
                    ArticuloId = 5,
                    Descripcion = "Router",
                    Costo = 2000,
                    Precio = 3000,
                    Existencia = 150,
                },

                new() {
                    ArticuloId = 6,
                    Descripcion = "Pantalla de celular",
                    Costo = 6500,
                    Precio = 9000,
                    Existencia = 100,
                },
                new() {
                    ArticuloId = 7,
                    Descripcion = "Tintas de impresoras",
                    Costo = 4000,
                    Precio = 6000,
                    Existencia = 200,
                }
            }

         );
        base.OnModelCreating(modelBuilder);

    }
}
