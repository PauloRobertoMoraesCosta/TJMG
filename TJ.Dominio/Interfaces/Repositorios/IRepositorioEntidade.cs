using System.Collections.Generic;
using TJ.Dominio.Entidades;

namespace TJ.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioEntidade : IRepositorioBase<Entidade>
    {
        IEnumerable<Entidade> RetornaEntidadesAtivas();
    }
}
