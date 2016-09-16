using System.Collections.Generic;
using TJ.Dominio.Entidades;

namespace TJ.Apresentacao.InterfacesApp
{
    public interface IAppServiceEndereco : IAppServiceBase<Endereco>
    {
        IEnumerable<Endereco> RetornarPorEntidade(int codigoEntidade);
    }
}
