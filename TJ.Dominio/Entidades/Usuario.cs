using System;
using System.Collections.Generic;

namespace TJ.Dominio.Entidades
{
    /// <summary>
    /// Classe com as regras para entidade Usuario
    /// </summary>
    public class Usuario
    {
        public Usuario()
        {
            SentenciadosCadastro = new HashSet<Sentenciado>();
            SentenciadosAlteracao = new HashSet<Sentenciado>();
            EntidadesCadastro = new HashSet<Entidade>();
            EntidadesAlteracao = new HashSet<Entidade>();
        }

        #region "Atributos"

        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public DateTime DataCadastro{ get; set; }
        public string DadosRegistro { get; set; }
        public string Super { get; set; }
        public string Ativo { get; set; }
        public virtual ICollection<Sentenciado> SentenciadosCadastro { get; set; }
        public virtual ICollection<Sentenciado> SentenciadosAlteracao { get; set; }
        public virtual ICollection<Entidade> EntidadesCadastro { get; set; }
        public virtual ICollection<Entidade> EntidadesAlteracao { get; set; }
        public virtual ICollection<CumprimentoMes> CumprimentosMes { get; set; }

        #endregion
    }
}
