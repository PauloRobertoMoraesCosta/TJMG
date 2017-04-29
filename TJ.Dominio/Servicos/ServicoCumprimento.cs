using System.Collections.Generic;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Repositorios;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Dominio.Servicos
{
    public class ServicoCumprimento : ServicoBase<Cumprimento>, IServicoCumprimento
    {
        protected readonly IRepositorioCumprimento _reposiotorioCumprimento;
        public ServicoCumprimento(IRepositorioCumprimento repositorioCumprimento)
            : base(repositorioCumprimento)
        {
            _reposiotorioCumprimento = repositorioCumprimento;
        }

        public IEnumerable<Cumprimento> RetornarPorSentenciado(int sentenciadoId)
        {
            return _reposiotorioCumprimento.RetornarPorSentenciado(sentenciadoId);
        }
    }
}
