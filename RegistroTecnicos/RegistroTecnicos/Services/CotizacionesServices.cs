using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class CotizacionesServices(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool>Guardar(Cotizaciones cotizaciones)
    {
        if(!await Existe(cotizaciones.CotizacionId))
            return await Insertar(cotizaciones);
        else
            return await Modificar(cotizaciones);
    }

    private async Task<bool> Modificar(Cotizaciones cotizaciones)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        _contexto.Update(cotizaciones);
        return await _contexto
            .SaveChangesAsync() > 0;
    }

    private async Task<bool> Insertar(Cotizaciones cotizaciones)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        _contexto.Add(cotizaciones);
        return await _contexto
            .SaveChangesAsync() > 0;
    }

    private async Task<bool> Existe(int id)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Cotizaciones
            .AnyAsync(c => c.CotizacionId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        var cotizaciones = await _contexto.Cotizaciones
            .Where(c => c.CotizacionId == id)
            .ExecuteDeleteAsync();
        return cotizaciones > 0;
    }

    public async Task<Cotizaciones?> Buscar(int id)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Cotizaciones
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CotizacionId == id);
    }
    public async Task<List<CotizacionesDetalle>> BuscarDetalle(int id)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.CotizacionesDetalle
            .Include(td => td.Articulos)
            .AsNoTracking()
            .Where(cd => cd.CotizacionId == id)
            .ToListAsync();
    }
    public async Task<List<Cotizaciones>> Listar(Expression<Func<Cotizaciones, bool>> criterio)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Cotizaciones
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<List<Articulos>> ListarArticulos()
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Articulos
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<CotizacionesDetalle>> ListarDetalle(int id)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        var detalle = await _contexto.CotizacionesDetalle
            .Where(d => d.CotizacionId == id)
            .ToListAsync();

        return detalle;
    }

    public async Task<Articulos> BuscarArticulos(int id)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Articulos
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.ArticuloId == id);
    }

    public async Task<bool> ActualizarArticulo(Articulos articulo)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        _contexto.Articulos.Update(articulo);
        return await _contexto
            .SaveChangesAsync() > 0;
    }
}
