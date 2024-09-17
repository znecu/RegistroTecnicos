using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class PrioridadesServices
{
    private readonly Contexto _contexto;

    public PrioridadesServices(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool>Guardar(Prioridades prioridad)
    {
        if(!await Existe(prioridad.PrioridadId))
            return await Insertar(prioridad);
        else
            return await Modificar(prioridad);
    }

    private async Task<bool> Modificar(Prioridades prioridad)
    {
        _contexto.Update(prioridad);
        return await _contexto
            .SaveChangesAsync() > 0;
    }

    private async Task<bool> Insertar(Prioridades prioridad)
    {
        _contexto.Prioridades.Add(prioridad);
        return await _contexto
            .SaveChangesAsync() > 0;
    }

    private async Task<bool> Existe(int prioridadId)
    {
       return await _contexto.Prioridades
            .AnyAsync(p => p.PrioridadId == prioridadId);
    }  
    public async Task<bool> ExistePrioridad(int prioridadId, string descripcion)
    {
       return await _contexto.Prioridades
            .AnyAsync(p => p.PrioridadId != prioridadId &&
            (p.Descripcion.ToLower().Equals(descripcion.ToLower())));
    }

    public async Task<bool> Eliminar(int id)
    {
        var prioridades = await _contexto.Prioridades
            .Where(p => p.PrioridadId == id)
            .ExecuteDeleteAsync();
        return prioridades > 0;
    }

    public async Task<Prioridades?> Buscar(int id)
    {
        return await _contexto.Prioridades
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PrioridadId == id);
    }
    
    public async Task<List<Prioridades>> Listar(Expression<Func<Prioridades, bool>> criterio)
    {
        return await _contexto.Prioridades
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}
