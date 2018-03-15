using System.Collections.Generic;
using TJ.Dominio.Entidades;
using System.Data.Entity;
using TJ.Dominio.Interfaces.Repositorios;
using TJ.Dados.Contexto;
using System.Linq;

namespace TJ.Dados.Repositorios
{
    public class RepositorioSentenciado : RepositorioBase<Sentenciado>, IRepositorioSentenciado
    {
        public IEnumerable<Sentenciado> RetornaTodos()
        {
            return db.Sentenciados.Include(e => e.SentenciadoEntidades).ToList();
        }

        public Sentenciado RetornaPorId(int Id)
        {
            return db.Sentenciados.Include(s => s.SentenciadoEntidades).Include(s => s.Bairro).Include(s => s.Cidade).Include(s => s.UsuarioCadastro).Include(s => s.UsuarioAlteracao).FirstOrDefault(e => e.Id == Id);
        }

        
    }
}
