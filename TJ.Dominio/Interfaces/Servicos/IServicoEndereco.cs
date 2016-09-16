using System.Collections.Generic;
using TJ.Dominio.Entidades;

namespace TJ.Dominio.Interfaces.Servicos
{
    public interface IServicoEndereco : IServicoBase<Endereco>
    {
        IEnumerable<Endereco> RetornarPorEntidade(int codigoEntidade);
    }
}
