using System.Collections.Generic;
using System.Data.Entity;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Repositorios;

namespace TJ.Dados.Repositorios
{
    public class RepositorioEntidade : RepositorioBase<Entidade>, IRepositorioEntidade
    {
        public IEnumerable<Entidade> RetornaTodos()
        {
            return db.Entidades.Include(e => e.Bairro).Include(e => e.Cidade);
        }
    }
}
