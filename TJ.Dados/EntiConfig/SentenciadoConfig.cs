using System.Data.Entity.ModelConfiguration;
using TJ.Dominio.Entidades;

namespace TJ.Dados.EntiConfig
{
    public class SentenciadoConfig : EntityTypeConfiguration<Sentenciado>
    {
        public SentenciadoConfig()
        {
            HasKey(s => s.Id);

            Property(s => s.AtividadePSC).HasColumnName("Sentenciado_AtividadePSC").HasMaxLength(100);
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
            Property(s => s.StatusPena).HasColumnName("Sentenciado_StatusPena").HasMaxLength(30);
            Property(s => s.Telefone).HasColumnName("Sentenciado_Telefone").HasMaxLength(50);
            Property(s => s.Comutacao).HasColumnName("Sentenciado_Comutacao");
            Property(s => s.ComutacaoObservacao).HasColumnName("Sentneciado_ComutacaoObservacao").HasMaxLength(200);
            Property(s => s.Detracao).HasColumnName("Sentenciado_Detracao");
            Property(s => s.DetracaoObservacao).HasColumnName("Sentneciado_DetracaoObservacao").HasMaxLength(200);
            Property(s => s.BairroId).HasColumnName("Sentenciado_Bairro").IsRequired();
            Property(s => s.CidadeId).HasColumnName("Sentenciado_Cidade").IsRequired();
            Property(s => s.UsuarioCadastroLogin).HasColumnName("Sentenciado_UsuarioCadastro").IsRequired().HasMaxLength(20);
            Property(s => s.UsuarioAlteracaoLogin).HasColumnName("Sentenciado_UsuarioAlteracao").HasMaxLength(20);
            Property(s => s.Sexo).HasColumnName("Sentenciado_Sexo").HasMaxLength(9).IsRequired();

            HasRequired(s => s.Bairro);
            HasRequired(s => s.Cidade);

            HasMany(s => s.SentenciadoEntidades);
        }
    }
}
