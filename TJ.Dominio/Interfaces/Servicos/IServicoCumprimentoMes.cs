using System.Collections.Generic;
using TJ.Dominio.Entidades;

namespace TJ.Dominio.Interfaces.Servicos
{
    public interface IServicoCumprimentoMes : IServicoBase<CumprimentoMes>
    {
        IEnumerable<CumprimentoMes> RetornarPorSentenciadoEntidade(int sentenciadoEntidadeId);
        IEnumerable<CumprimentoMes> RetornarPorSentenciado(int sentenciadoEntidadeId);
    }
}
