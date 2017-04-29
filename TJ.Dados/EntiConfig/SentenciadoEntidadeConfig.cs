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

            Property(mse => mse.DataFim).HasColumnName("SentenciadoEntidade_DataFim").IsOptional();
            Property(mse => mse.DataInicio).IsRequired().HasColumnName("SentenciadoEntidade_DataInicio");
            Property(mse => mse.HorarioDeCumprimento)
                .HasColumnName("SentenciadoEntidade_HorarioDeCumprimento")
                .HasMaxLength(200);
            Property(mse => mse.SituacaoCumprimento)
                .HasColumnName("SentenciadoEntidade_SituacaoCumprimento")
                .HasMaxLength(50);
            Property(mse => mse.EntidadeId).IsRequired().HasColumnName("SentenciadoEntidade_Instituicao");
            Property(mse => mse.SentenciadoId).IsRequired().HasColumnName("SentenciadoEntidade_Sentenciado");

            HasRequired(mse => mse.Entidade);
            HasRequired(mse => mse.Sentenciado);
        }
    }
}
