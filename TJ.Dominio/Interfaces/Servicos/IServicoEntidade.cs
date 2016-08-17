using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Repositorios;

namespace TJ.Dominio.Interfaces.Servicos
{
    public interface IServicoEntidade : IRepositorioBase<Entidade>
    {
    }
}
