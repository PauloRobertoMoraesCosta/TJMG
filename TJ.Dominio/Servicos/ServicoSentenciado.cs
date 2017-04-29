using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Repositorios;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Dominio.Servicos
{
    public class ServicoSentenciado : ServicoBase<Sentenciado>, IServicoSentenciado
    {
        protected readonly IRepositorioSentenciado _repositorioSentenciado;

        public ServicoSentenciado(IRepositorioSentenciado repositorioSentenciado) 
            : base(repositorioSentenciado)
        {
            _repositorioSentenciado = repositorioSentenciado;
        }
    }
}
