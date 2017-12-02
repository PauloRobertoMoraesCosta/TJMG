using System.Collections.Generic;
using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Apresentacao.ServiceApp
{
    public class AppServiceCumprimentoMes : AppServiceBase<CumprimentoMes>, IAppServiceCumprimentoMes
    {
        protected readonly IServicoCumprimentoMes _serviceCumprimentoMes;
        public AppServiceCumprimentoMes(IServicoCumprimentoMes servicoCumprimentoMes
            )
            : base(servicoCumprimentoMes)
        {
            _serviceCumprimentoMes = servicoCumprimentoMes;
        }

        public IEnumerable<CumprimentoMes> RetornarPorSentenciadoEntidade(int sentenciadoEntidadeId)
        {
            return _serviceCumprimentoMes.RetornarPorSentenciadoEntidade(sentenciadoEntidadeId);
        }
        public IEnumerable<CumprimentoMes> RetornarPorSentenciado(int sentenciadoId)
        {
            return _serviceCumprimentoMes.RetornarPorSentenciado(sentenciadoId);
        }
    }
}
