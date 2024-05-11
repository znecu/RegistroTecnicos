using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TecnicosServices
{
    private readonly Contexto _contexto;

    public TecnicosServices(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> Insertar(Tecnicos tecnicos)
    {
        _contexto.Tecnicos.Add(tecnicos);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Tecnicos tecnicos)
    {
        var s = await _contexto.Tecnicos.FindAsync(tecnicos.TecnicoId);
        _contexto.Entry(s!).State = EntityState.Detached;
        _contexto.Entry(tecnicos).State = EntityState.Modified;
        return _contexto.SaveChanges() > 0;
    }

    public async Task<bool> Existe(int TecnicosId)
    {
        return await _contexto.Tecnicos
        .AnyAsync(c => c.TecnicoId == TecnicosId);
    }

    public async Task <bool> Guardar(Tecnicos tecnicos)
    {
        if(!await Existe(tecnicos.TecnicoId))
            return await Insertar(tecnicos);
        else
            return await Insertar(tecnicos);
    }

    public async Task <bool> Eliminar(Tecnicos tecnicos)
    {
        var cantidad = await _contexto.Tecnicos
            .Where(p => p.TecnicoId == tecnicos.TecnicoId)
            .ExecuteDeleteAsync();

        return cantidad > 0;
    }

    public async Task <Tecnicos>? Buscar(int TecnicoId)
    {
        return await _contexto.Tecnicos
            .Where(s => s.TecnicoId == TecnicoId)
            .AsNoTracking()
            .SingleOrDefaultAsync();
    }
    public List<Tecnicos> Listar(Expression<Func<Tecnicos, bool >> Criterio)
    {
        return _contexto.Tecnicos
            .AsNoTracking()
            .ToList();
    }
}
