using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Repositorios;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Dominio.Servicos
{
    public class ServicoEstado : ServicoBase<Estado>, IServicoEstado
    {
        protected readonly IRepositorioEstado _repositorioEstado;

        public ServicoEstado(IRepositorioEstado repositorioEstado) : base(repositorioEstado)
        {
            _repositorioEstado = repositorioEstado;
        }
    }
}
