using System.Collections.Generic;
using TJ.Dominio.Entidades;

namespace TJ.Dominio.Interfaces.Servicos
{
    public interface IServicoEntidade : IServicoBase<Entidade>
    {
        IEnumerable<Entidade> RetornaEntidadesAtivasAsNoTracking();
    }
}
