using System.Collections.Generic;
using TJ.Dominio.Entidades;

namespace TJ.Dominio.Interfaces.Servicos
{
    public interface IServicoUsuario : IServicoBase<Usuario>
    {
        Usuario LogaUsuario(string login, string senha);
        IEnumerable<Usuario> RetornaUsuariosAtivosAsNoTracking();
        Usuario RetornarPorLogin(string login);
    }
}
