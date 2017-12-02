using System.Collections.Generic;
using TJ.Dominio.Entidades;

namespace TJ.Apresentacao.InterfacesApp
{
    public interface IAppServiceCumprimentoMes : IAppServiceBase<CumprimentoMes>
    {
        IEnumerable<CumprimentoMes> RetornarPorSentenciadoEntidade(int sentenciadoEntidadeId);
        IEnumerable<CumprimentoMes> RetornarPorSentenciado(int sentenciadoEntidadeId);
    }
}
