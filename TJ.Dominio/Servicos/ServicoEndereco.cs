using System.Collections.Generic;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Repositorios;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Dominio.Servicos
{
    public class ServicoEndereco : ServicoBase<Endereco>, IServicoEndereco
    {
        protected readonly IRepositorioEndereco _repositorioEndereco;

        public ServicoEndereco(IRepositorioEndereco repositorioEndereco) : base(repositorioEndereco)
        {
            _repositorioEndereco = repositorioEndereco;
        }

        public IEnumerable<Endereco> RetornarPorEntidade(int codigoEntidade)
        {
            return _repositorioEndereco.RetornarPorEntidade(codigoEntidade);
        }
    }
}
