using System.Collections.Generic;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Repositorios;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Dominio.Servicos
{
    public class ServicoUsuario : ServicoBase<Usuario>, IServicoUsuario
    {
        private readonly IRepositorioUsuario _repositorioUsuario;

        public ServicoUsuario(IRepositorioUsuario repositorioUsuario)
            : base(repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario;
        }


        public Usuario LogaUsuario(string login, string senha)
        {
            return _repositorioUsuario.logaUsuario(login, senha);
        }


        public IEnumerable<Usuario> RetornaUsuariosAtivosAsNoTracking()
        {
            return _repositorioUsuario.RetornaUsuariosAtivosAsNoTracking();
        }

        public Usuario RetornarPorLogin(string login)
        {
            return _repositorioUsuario.RetornarPorLogin(login);
        }
    }
}
