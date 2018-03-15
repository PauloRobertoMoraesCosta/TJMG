using System.Collections.Generic;
using TJ.Dominio.Entidades;

namespace TJ.Apresentacao.InterfacesApp
{
    public interface IAppServiceEntidade : IAppServiceBase<Entidade>
    {
        IEnumerable<Entidade> RetornaEntidadesAtivas();
    }
}
