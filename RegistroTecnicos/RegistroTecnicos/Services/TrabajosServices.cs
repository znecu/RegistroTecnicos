using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TrabajosServices
{
    private readonly Contexto _contexto;

    public TrabajosServices(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> Guardar(Trabajos trabajo)
    {
        if (!await Existe(trabajo.TrabajoId))
        {
            foreach (var detalle in trabajo.TrabajoDetalle)
            {
                var articulo = await BuscarArticulos(detalle.ArticuloId);
                if (articulo != null)
                {
                    articulo.Existencia -= detalle.Cantidad;
                    await ActualizarArticulo(articulo);
                }
            }
            return await Insertar(trabajo);

        }
        else
            return await Modificar(trabajo);
    }

    private async Task<bool> Modificar(Trabajos trabajo)
    {
        _contexto.Update(trabajo);
        return await _contexto
            .SaveChangesAsync() > 0;
    }

    private async Task<bool> Insertar(Trabajos trabajo)
    {
        _contexto.Trabajos.Add(trabajo);
        return await _contexto
            .SaveChangesAsync() > 0;
    }

    private async Task<bool> Existe(int trabajoId)
    {
        return await _contexto.Trabajos
            .AnyAsync(t => t.TrabajoId == trabajoId);
    }

    public async Task<bool> ExisteTrabajo(int trabajoId, string descripcion)
    {
        return await _contexto.Trabajos
            .AnyAsync(t => t.TrabajoId != trabajoId &&
            (t.Descripcion.ToLower().Equals(descripcion.ToLower())));
    }

    public async Task<bool> Eliminar(int id)
    {
        var trabajos = await _contexto.Trabajos
            .Where(t => t.TrabajoId == id)
            .ExecuteDeleteAsync();
        return trabajos > 0;
    }

    public async Task<Trabajos?> Buscar(int id)
    {
        return await _contexto.Trabajos
            .Include(t => t.Tecnicos)
            .Include(c => c.Clientes)
            .Include(p => p.Prioridades)
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TrabajoId == id);
    }
    public async Task<List<TrabajoDetalle>> BuscarDetalle(int id)
    {
        return await _contexto.TrabajoDetalles
            .Include(td => td.Articulos)
            .AsNoTracking()
            .Where(td => td.TrabajoId == id)
            .ToListAsync();
    }
    public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
    {
        return await _contexto.Trabajos
            .Include(t => t.Clientes)
            .Include(t => t.Tecnicos)
            .Include(p => p.Prioridades)
            .Include(d => d.TrabajoDetalle)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<List<Articulos>> ListarArticulos()
    {
        return await _contexto.Articulos
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<TrabajoDetalle>> ListarDetalle(int trabajoId)
    {
        var detalle = await _contexto.TrabajoDetalles
            .Where(d => d.TrabajoId == trabajoId)
            .ToListAsync();

        return detalle;
    }

    public async Task<Articulos> BuscarArticulos(int id)
    {
        return await _contexto.Articulos
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.ArticuloId == id)   
    }

    public async Task<bool> ActualizarArticulo(Articulos articulo)
    {
        _contexto.Articulos.Update(articulo);
        return await _contexto
            .SaveChangesAsync() > 0;
    }
}
