using System.Collections.Generic;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Repositorios;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Dominio.Servicos
{
    public class ServicoUsuario : ServicoBase<Usuario>, IServicoUsuario
    {
        private readonly IRepositorioUsuario _usuarioRepositorio;

        public ServicoUsuario(IRepositorioUsuario usuarioRepositorio) : base(usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }


        public Usuario LogaUsuario(string login, string senha)
        {
            return _usuarioRepositorio.logaUsuario(login, senha);
        }


        public IEnumerable<Usuario> RetornaUsuariosAtivosAsNoTracking()
        {
            return _usuarioRepositorio.RetornaUsuariosAtivosAsNoTracking();
        }
    }
}
