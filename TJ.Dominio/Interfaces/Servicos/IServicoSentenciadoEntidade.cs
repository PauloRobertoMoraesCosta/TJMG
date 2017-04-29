using System.Collections.Generic;
using TJ.Dominio.Entidades;

namespace TJ.Dominio.Interfaces.Servicos
{
    public interface IServicoSentenciadoEntidade : IServicoBase<SentenciadoEntidade>
    {
        IEnumerable<SentenciadoEntidade> RetornarPorSentenciado(int sentenciadoId);
    }
}
