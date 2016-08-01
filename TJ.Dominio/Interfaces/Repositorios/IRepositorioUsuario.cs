using System.Collections.Generic;
using TJ.Dominio.Entidades;

namespace TJ.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioUsuario : IRepositorioBase<Usuario>
    {
        Usuario logaUsuario(string login, string senha);
        IEnumerable<Usuario> RetornaUsuariosAtivosAsNoTracking();
    }
}
