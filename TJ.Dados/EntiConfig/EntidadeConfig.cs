using System.Data.Entity.ModelConfiguration;
using TJ.Dominio.Entidades;

namespace TJ.Dados.EntiConfig
{
    public class EntidadeConfig : EntityTypeConfiguration<Entidade>
    {
        public EntidadeConfig()
        {
            HasKey(e => e.Id);

            Property(e => e.Id).HasColumnName("Entidade_Id");
            Property(e => e.Nome).HasColumnName("Entidade_Nome").IsRequired().HasMaxLength(50);
            Property(e => e.AtividadePrincipal).HasColumnName("Entidade_AtividadePrincipal").HasMaxLength(50).IsRequired();
            Property(e => e.Endereco).HasColumnName("Entidade_Endereco").HasMaxLength(100).IsRequired();
            Property(e => e.BairroId).HasColumnName("Entidade_Bairro").IsRequired();
            Property(e => e.CidadeId).HasColumnName("Entidade_Cidade").IsRequired();
            Property(e => e.PontoReferencia).HasColumnName("Entidade_PontoReferencia").HasMaxLength(200).IsRequired();
            Property(e => e.Responsavel).HasColumnName("Entidade_Responsavel").HasMaxLength(100).IsRequired();
            Property(e => e.Telefone).HasColumnName("Entidade_Telefone").HasMaxLength(50).IsRequired();
            Property(s => s.UsuarioCadastroLogin).HasColumnName("Entidade_UsuarioCadastro").IsRequired().HasMaxLength(20);
            Property(s => s.UsuarioAlteracaoLogin).HasColumnName("Entidade_UsuarioAlteracao").HasMaxLength(20);

            HasRequired(e => e.Bairro);
            HasRequired(e => e.Cidade);
            HasMany(e => e.MovimentacaoSentenciadoEntidades);
        }
    }
}
