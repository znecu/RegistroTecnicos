using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TecnicoServices(IDbContextFactory<Contexto> DbFactory)
{

    private async Task<bool> Existe(int tecnicoId)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Tecnicos
            .AnyAsync(t => t.TecnicoId == tecnicoId);
    }

    public async Task<bool> ExisteTecnico(int tecnicoId, string nombre)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Tecnicos
            .AnyAsync(t => t.TecnicoId != tecnicoId &&
                t.Nombres.ToLower().Equals(nombre.ToLower()));

    }
    private async Task<bool> Insertar(Tecnicos tecnico)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        _contexto.Tecnicos.Add(tecnico);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Tecnicos tecnico)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        _contexto.Update(tecnico);
        return await _contexto
            .SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Tecnicos tecnico)
    {
        if (!await Existe(tecnico.TecnicoId))
            return await Insertar(tecnico);
        else
            return await Modificar(tecnico);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        var tecnicos = await _contexto.Tecnicos
            .Where(t => t.TecnicoId == id).ExecuteDeleteAsync();
        return tecnicos > 0;
    }

    public async Task<Tecnicos?> Buscar(int id)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Tecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(T => T.TecnicoId == id);
    }
    public async Task<Tecnicos?> BuscarTipoTecnico(int id)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Tecnicos
            .Include(t => t.TiposTecnicos)
            .AsNoTracking()
            .FirstOrDefaultAsync(T => T.TecnicoId == id);
    }
    public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Tecnicos
            .Include(t => t.TiposTecnicos)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}
