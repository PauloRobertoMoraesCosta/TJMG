using System.Collections.Generic;
using TJ.Dominio.Entidades;

namespace TJ.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioEndereco : IRepositorioBase<Endereco>
    {
        IEnumerable<Endereco> RetornarPorEntidade(int codigoEntidade);
    }
}
