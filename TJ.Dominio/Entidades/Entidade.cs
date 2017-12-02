using System.Collections.Generic;

namespace TJ.Dominio.Entidades
{
    public class Entidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string AtividadePrincipal { get; set; }
        public string Endereco { get; set; }
        public string PontoReferencia { get; set; }
        public string Responsavel { get; set; }
        public string Telefone { get; set; }
        public string Ativo { get; set; }
        public int BairroId { get; set; }
        public int CidadeId { get; set; }
        public int UsuarioCadastroId { get; set; }
        public int? UsuarioAlteracaoId { get; set; }
        public virtual Bairro Bairro { get; set; }
        public virtual Cidade Cidade { get; set; }
        public virtual Usuario UsuarioCadastro { get; set; }
        public virtual Usuario UsuarioAlteracao { get; set; }
        public virtual ICollection<SentenciadoEntidade> SentenciadoEntidades { get; set; }

        public Entidade()
        {
            SentenciadoEntidades = new HashSet<SentenciadoEntidade>();
        }
    }
}
