using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Repositorios;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Dominio.Servicos
{
    public class ServicoJesp : ServicoBase<Jesp>, IServicoJesp
    {
        protected readonly IRepositorioJesp _repositorioJesp;

        public ServicoJesp(IRepositorioJesp repositorioJesp) : base(repositorioJesp)
        {
            _repositorioJesp = repositorioJesp;
        }
    }
}
