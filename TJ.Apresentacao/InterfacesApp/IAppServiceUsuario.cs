using System.Collections.Generic;
using TJ.Dominio.Entidades;

namespace TJ.Apresentacao.InterfacesApp
{
    public interface IAppServiceUsuario : IAppServiceBase<Usuario>
    {
        Usuario logaUsuario(string login, string senha);
        IEnumerable<Usuario> RetornaUsuariosAtivosAsNoTracking();
    }
}
