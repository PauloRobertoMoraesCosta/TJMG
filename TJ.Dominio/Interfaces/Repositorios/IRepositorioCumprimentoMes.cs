using System.Collections.Generic;
using TJ.Dominio.Entidades;

namespace TJ.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioCumprimentoMes : IRepositorioBase<CumprimentoMes>
    {
        IEnumerable<CumprimentoMes> RetornarPorSentenciadoEntidade(int sentenciadoEntidadeId);
        IEnumerable<CumprimentoMes> RetornarPorSentenciado(int sentenciadoEntidadeId);
    }
}
