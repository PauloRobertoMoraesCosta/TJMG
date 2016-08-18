using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Repositorios;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Dominio.Servicos
{
    public class ServicoEntidade : ServicoBase<Entidade>, IServicoEntidade
    {
        protected readonly IRepositorioEntidade _repositorioEntidade;

        public ServicoEntidade(IRepositorioEntidade repositorioEntidade) : base(repositorioEntidade)
        {
            _repositorioEntidade = repositorioEntidade;
        }
    }
}
