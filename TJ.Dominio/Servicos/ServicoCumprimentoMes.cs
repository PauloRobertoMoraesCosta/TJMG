using System;
using System.Collections.Generic;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Repositorios;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Dominio.Servicos
{
    public class ServicoCumprimentoMes : ServicoBase<CumprimentoMes>, IServicoCumprimentoMes
    {
        protected readonly IRepositorioCumprimentoMes _repositorioCumprimentoMes;
        public ServicoCumprimentoMes(IRepositorioCumprimentoMes repositorioCumprimentoMes)
            : base(repositorioCumprimentoMes)
        {
            _repositorioCumprimentoMes = repositorioCumprimentoMes;
        }

        public IEnumerable<CumprimentoMes> RetornarPorSentenciadoEntidade(int sentenciadoEntidadeId)
        {
            return _repositorioCumprimentoMes.RetornarPorSentenciadoEntidade(sentenciadoEntidadeId);
        }
        public IEnumerable<CumprimentoMes> RetornarPorSentenciado(int sentenciadoId)
        {
            return _repositorioCumprimentoMes.RetornarPorSentenciado(sentenciadoId);
        }

    }
}
