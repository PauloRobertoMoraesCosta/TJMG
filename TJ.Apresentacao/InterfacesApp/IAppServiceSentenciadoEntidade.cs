using System.Collections.Generic;
using TJ.Dominio.Entidades;

namespace TJ.Apresentacao.InterfacesApp
{
    public interface IAppServiceSentenciadoEntidade : IAppServiceBase<SentenciadoEntidade>
    {
        IEnumerable<SentenciadoEntidade> RetornarPorSentenciado(int sentenciadoId);
    }
}
