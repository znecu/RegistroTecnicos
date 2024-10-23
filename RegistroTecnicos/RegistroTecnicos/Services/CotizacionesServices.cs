using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class CotizacionesServices(IDbContextFactory<Contexto> DbFactory)
{
    private async Task<bool>Guardar(Cotizaciones cotizaciones)
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

    public async Task<List<Cotizaciones>> Listar(Expression<Func<Cotizaciones, bool>> criterio)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Cotizaciones
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}
