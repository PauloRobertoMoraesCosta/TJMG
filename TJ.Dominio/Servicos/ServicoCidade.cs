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
    public class ServicoCidade : ServicoBase<Cidade>, IServicoCidade
    {
        protected readonly IRepositorioCidade _repositorioCidade;

        public ServicoCidade(IRepositorioCidade repositorioCidade) : base(repositorioCidade)
        {
            _repositorioCidade = repositorioCidade;
        }
    }
}
