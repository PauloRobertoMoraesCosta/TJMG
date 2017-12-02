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
            Property(e => e.Nome).HasColumnName("Entidade_Nome").IsRequired().HasMaxLength(100);
            Property(e => e.AtividadePrincipal).HasColumnName("Entidade_AtividadePrincipal").HasMaxLength(100).IsRequired();
            Property(e => e.Endereco).HasColumnName("Entidade_Endereco").HasMaxLength(100).IsRequired();
            Property(e => e.BairroId).HasColumnName("Entidade_Bairro").IsRequired();
            Property(e => e.CidadeId).HasColumnName("Entidade_Cidade").IsRequired();
            Property(e => e.PontoReferencia).HasColumnName("Entidade_PontoReferencia").HasMaxLength(200);
            Property(u => u.Ativo).HasColumnName("Entidade_Ativo").IsRequired().HasMaxLength(5);
            Property(e => e.Responsavel).HasColumnName("Entidade_Responsavel").HasMaxLength(100);
            Property(e => e.Telefone).HasColumnName("Entidade_Telefone").HasMaxLength(50).IsRequired();
            Property(s => s.UsuarioCadastroId).HasColumnName("Entidade_UsuarioCadastroId").IsRequired();
            Property(s => s.UsuarioAlteracaoId).HasColumnName("Entidade_UsuarioAlteracaoId").IsOptional();

            HasRequired(e => e.Bairro).WithMany(b => b.Entidades).HasForeignKey(eb => eb.BairroId);
            HasRequired(e => e.Cidade).WithMany(c => c.Entidades).HasForeignKey(ec => ec.CidadeId); ;
            HasRequired(e => e.UsuarioCadastro).WithMany(u => u.EntidadesCadastro).HasForeignKey(eu => eu.UsuarioCadastroId);
            HasOptional(e => e.UsuarioAlteracao).WithMany(u => u.EntidadesAlteracao).HasForeignKey(eu => eu.UsuarioAlteracaoId);
        }
    }
}
