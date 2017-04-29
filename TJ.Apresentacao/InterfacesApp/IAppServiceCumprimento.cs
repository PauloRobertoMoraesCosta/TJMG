using System.Collections.Generic;
using TJ.Dominio.Entidades;

namespace TJ.Apresentacao.InterfacesApp
{
    public interface IAppServiceCumprimento : IAppServiceBase<Cumprimento>
    {
        IEnumerable<Cumprimento> RetornarPorSentenciado(int sentenciadoId);
    }
}
