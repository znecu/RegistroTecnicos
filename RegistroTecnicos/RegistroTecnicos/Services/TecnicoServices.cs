using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TecnicoServices
{
    private readonly Contexto _contexto;

    public TecnicoServices(Contexto contexto)
    {
        _contexto = contexto;
    }

    //métodos

    private async Task <bool> Existe(int tecnicoId)
    {
        return await _contexto.Tecnicos
            .AnyAsync(t => t.TecnicoId == tecnicoId);
    }

    public async Task<bool> ExisteTecnico(int tecnicoId, string nombre)
    {
        return await _contexto.Tecnicos
            .AnyAsync(e => e.TecnicoId != tecnicoId && e.NombreTecnico.ToLower().Equals(nombre.ToLower()));

    }
    private async Task<bool> Insertar(Tecnicos tecnico)
    {
        _contexto.Tecnicos.Add(tecnico);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Tecnicos tecnico)
    {
        _contexto.Update(tecnico);
        return await _contexto
            .SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Tecnicos tecnico)
    {
        if(!await Existe(tecnico.TecnicoId))
            return await Insertar(tecnico);
        else
            return await Modificar(tecnico);
    }

    public async Task<bool> Eliminar(int id)
    {
        var tecnicos = await _contexto.Tecnicos
            .Where(T => T.TecnicoId == id).ExecuteDeleteAsync();
        return tecnicos > 0;
    }

    public async Task<Tecnicos?> Buscar(int id)
    {
        return await _contexto.Tecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(T => T.TecnicoId == id);
    }

    public List<Tecnicos> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        return _contexto.Tecnicos
            .AsNoTracking()
            .Where(criterio)
            .ToList();
    }
}
