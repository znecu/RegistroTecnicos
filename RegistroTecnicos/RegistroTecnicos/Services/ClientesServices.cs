using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class ClientesServices
{
    private readonly Contexto _contexto;

    public ClientesServices(Contexto contexto)
    {
        _contexto = contexto;
    }

    private async Task <bool> Existe(int ClientesId)
    {
        return await _contexto.Clientes
            .AnyAsync(c => c.ClientesId == ClientesId);
    }

    public async Task<bool> ExisteCliente(int ClientesId, string Nombres, string whatsapp)
    {
        return await _contexto.Clientes
            .AnyAsync(c => c.ClientesId != ClientesId &&
            (c.Whatsapp.Equals(whatsapp) || c.Nombres.ToLower().Equals(Nombres.ToLower())));
    }

    private async Task<bool> Insertar(Clientes cliente)
    {
        _contexto.Clientes.Add(cliente);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Clientes cliente)
    {
        _contexto.Update(cliente);
        return await _contexto
            .SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Clientes cliente)
    {
        if(!await Existe(cliente.ClientesId))
            return await Insertar(cliente);
        else
            return await Modificar(cliente);
    }

    public async Task<bool> Eliminar(int id)
    {
        var clientes = await _contexto.Clientes
            .Where(c => c.ClientesId == id)
            .ExecuteDeleteAsync();
        return clientes > 0;
    }

    public async Task<Clientes?> Buscar(int id)
    {
        return await _contexto.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.ClientesId == id);
    }

    public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
    {
        return await _contexto.Clientes
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}
