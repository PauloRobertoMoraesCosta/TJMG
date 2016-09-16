using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TJ.Dados.Verifications;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Repositorios;

namespace TJ.Dados.Repositorios
{
    public class RepositorioEndereco : RepositorioBase<Endereco>, IRepositorioEndereco
    {
        public IEnumerable<Endereco> RetornarPorEntidade(int codigoEntidade)
        {
            try
            {
                return db.Enderecos.Where(endereco => endereco.EntidadeId.Equals(codigoEntidade)).Include(endereco => endereco.Cidade).Include(endereco => endereco.Bairro).Include(endereco => endereco.Entidade);
            }
            catch (Exception exception)
            {
                throw new DadosException("Erro ao carregar os endereços: " + exception.Message);
            }
        } 
    }
}
