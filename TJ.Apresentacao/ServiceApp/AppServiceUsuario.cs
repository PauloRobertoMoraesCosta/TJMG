using System.Collections.Generic;
using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Apresentacao.ServiceApp
{
    public class AppServiceUsuario : AppServiceBase<Usuario>, IAppServiceUsuario
    {
        private readonly IServicoUsuario _serviceUsuario;

        public AppServiceUsuario(IServicoUsuario serviceUsuario) : base(serviceUsuario)
        {
            _serviceUsuario = serviceUsuario;
        }


        public Usuario logaUsuario(string login, string senha)
        {
            return _serviceUsuario.LogaUsuario(login, senha);
        }


        public IEnumerable<Usuario> RetornaUsuariosAtivosAsNoTracking()
        {
            return _serviceUsuario.RetornaUsuariosAtivosAsNoTracking();
        }

        public Usuario RetornarPorLogin(string login)
        {
            return _serviceUsuario.RetornarPorLogin(login);
        }
    }
}
