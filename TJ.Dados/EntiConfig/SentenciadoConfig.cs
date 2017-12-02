using System.Data.Entity.ModelConfiguration;
using TJ.Dominio.Entidades;

namespace TJ.Dados.EntiConfig
{
    public class SentenciadoConfig : EntityTypeConfiguration<Sentenciado>
    {
        public SentenciadoConfig()
        {
            HasKey(s => s.Id);

            Property(s => s.DataNascimento).HasColumnName("Sentenciado_DataNascimento").IsRequired();
            Property(s => s.Endereco).HasColumnName("Sentenciado_Endereco").HasMaxLength(50).IsRequired();
            Property(s => s.Escolaridade).HasColumnName("Sentenciado_Escolaridade").HasMaxLength(50).IsRequired();
            Property(s => s.EstadoCivil).HasColumnName("Sentenciado_EstadoCivil").IsRequired().HasMaxLength(25);
            Property(s => s.Filiacao).HasColumnName("Sentenciado_Filiacao").HasMaxLength(200);
            Property(s => s.Naturalidade).HasColumnName("Sentenciado_Naturalidade").HasMaxLength(50);
            Property(s => s.Nome).HasColumnName("Sentenciado_Nome").HasMaxLength(100).IsRequired();
            Property(s => s.Observacao).HasColumnName("Sentenciado_Observacao").HasMaxLength(300);
            Property(s => s.OcupacaoExperiencia).HasColumnName("Sentenciado_Ocupacao").HasMaxLength(100);
            Property(s => s.Origem).HasColumnName("Sentenciado_Origem").HasMaxLength(50);
            Property(s => s.PenaAnos).HasColumnName("Sentenciado_PenaAnos").IsOptional();
            Property(s => s.PenaMeses).HasColumnName("Sentenciado_PenaMeses").IsOptional();
            Property(s => s.PenaDias).HasColumnName("Sentenciado_PenaDias").IsRequired();
            Property(s => s.ResponsavelSetor).HasColumnName("Sentenciado_ResponsavelSetor").HasMaxLength(50).IsRequired();
            Property(s => s.PontoReferencia).HasColumnName("Sentenciado_PontoReferencia").HasMaxLength(200);
            Property(s => s.Processo).HasColumnName("Sentenciado_Processo").HasMaxLength(25).IsRequired();
            Property(s => s.StatusPena).HasColumnName("Sentenciado_StatusPena").HasMaxLength(50);
            Property(s => s.Telefone).HasColumnName("Sentenciado_Telefone").HasMaxLength(50);
            Property(s => s.SomaDePena).HasColumnName("Sentenciado_SomaDePena");
            Property(s => s.SomaDePenaObservacao).HasColumnName("Sentenciado_SomaDePenaObservacao").HasMaxLength(300);
            Property(s => s.Detracao).HasColumnName("Sentenciado_Detracao");
            Property(s => s.DetracaoObservacao).HasColumnName("Sentenciado_DetracaoObservacao").HasMaxLength(300);
            Property(s => s.Sexo).HasColumnName("Sentenciado_Sexo").HasMaxLength(9).IsRequired();
            Property(s => s.BairroId).HasColumnName("Sentenciado_Bairro").IsRequired();
            Property(s => s.CidadeId).HasColumnName("Sentenciado_Cidade").IsRequired();
            Property(s => s.UsuarioCadastroId).HasColumnName("Sentenciado_UsuarioCadastoId").IsRequired();
            Property(s => s.UsuarioAlteracaoId).HasColumnName("Sentenciado_UsuarioAlteracaoId");
            
            HasRequired(s => s.Bairro).WithMany(b => b.Sentenciados).HasForeignKey(sb => sb.BairroId);
            HasRequired(s => s.Cidade).WithMany(c => c.Sentenciados).HasForeignKey(sc => sc.CidadeId);
            HasRequired(s => s.UsuarioCadastro).WithMany(u => u.SentenciadosCadastro).HasForeignKey(su => su.UsuarioCadastroId);
            HasOptional(s => s.UsuarioAlteracao).WithMany(u => u.SentenciadosAlteracao).HasForeignKey(s => s.UsuarioAlteracaoId);
        }
    }
}
