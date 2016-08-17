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
    public class ServicoTelefone: ServicoBase<Telefone>, IServicoTelefone
    {
        protected readonly IRepositorioTelefone _repositorioTelefone;

        public ServicoTelefone(IRepositorioTelefone repositorioTelefone) : base(repositorioTelefone)
        {
            _repositorioTelefone = repositorioTelefone;
        }
    }
}
