using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TiposTecnicosServices
{
    private readonly Contexto _contexto;

    public TiposTecnicosServices(Contexto contexto)
    {
        _contexto = contexto;
    }

    private async Task<bool> Existe(int tipoTecnicoId)
    {
        return await _contexto.TiposTecnicos
            .AnyAsync(t => t.TiposTecnicosId == tipoTecnicoId);
    }

    public async Task<bool>ExisteTipoTecnico(int tiposTecnicosId, string descripcion)
    {
        return await _contexto.TiposTecnicos
            .AnyAsync(t => t.TiposTecnicosId != tiposTecnicosId &&
                t.Descripcion.ToLower().Equals(descripcion.ToLower()));
    }

    private async Task<bool> Insertar(TiposTecnicos tipoTecnicos)
    {
        _contexto.TiposTecnicos.Add(tipoTecnicos);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(TiposTecnicos tiposTecnicos)
    {
        _contexto.Update(tiposTecnicos);
        return await _contexto
            .SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(TiposTecnicos tiposTecnicos)
    {
        if(!await Existe(tiposTecnicos.TiposTecnicosId))
            return await Insertar(tiposTecnicos);
        else
            return await Modificar(tiposTecnicos);
    }

    public async Task<bool> Eliminar(int tiposTecnicosId)
    {
        var tiposTecnicos = await _contexto.TiposTecnicos
            .Where(t => t.TiposTecnicosId == tiposTecnicosId)
            .ExecuteDeleteAsync();
        return tiposTecnicos > 0;
    }

    public async Task<TiposTecnicos?> Buscar(int tiposTecnicosId)
    {
        return await _contexto.TiposTecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TiposTecnicosId == tiposTecnicosId);
    }

    public async Task<List<TiposTecnicos>> Listar(Expression<Func<TiposTecnicos, bool>> criterio)
    {
        return await _contexto.TiposTecnicos
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

}
