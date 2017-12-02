using System.Data.Entity.ModelConfiguration;
using TJ.Dominio.Entidades;

namespace TJ.Dados.EntiConfig
{
    public class SentenciadoEntidadeConfig : EntityTypeConfiguration<SentenciadoEntidade>
    {
        public SentenciadoEntidadeConfig()
        {
            ToTable("Sentenciado_Entidade");
            HasKey(mse => mse.Id);

            Property(se => se.DataFim).HasColumnName("SentenciadoEntidade_DataFim").IsOptional();
            Property(se => se.DataInicio).IsRequired().HasColumnName("SentenciadoEntidade_DataInicio");
            Property(se => se.HorarioDeCumprimento).HasColumnName("SentenciadoEntidade_HorarioDeCumprimento").HasMaxLength(200);
            Property(se => se.SituacaoCumprimento).HasColumnName("SentenciadoEntidade_SituacaoCumprimento").HasMaxLength(50);
            Property(se => se.AtividadePSC).HasColumnName("Sentenciado_AtividadePSC").IsRequired().HasMaxLength(100);
            Property(se => se.EntidadeId).IsRequired().HasColumnName("SentenciadoEntidade_EntidadeId");
            Property(se => se.SentenciadoId).IsRequired().HasColumnName("SentenciadoEntidade_SentenciadoId");

            HasRequired(se => se.Entidade).WithMany(e => e.SentenciadoEntidades).HasForeignKey(see => see.EntidadeId);
            HasRequired(se => se.Sentenciado).WithMany(s => s.SentenciadoEntidades).HasForeignKey(ses => ses.SentenciadoId);
        }
    }
}
