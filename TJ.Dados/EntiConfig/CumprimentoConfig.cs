using System.Data.Entity.ModelConfiguration;
using TJ.Dominio.Entidades;

namespace TJ.Dados.EntiConfig
{
    public class CumprimentoConfig : EntityTypeConfiguration<Cumprimento>
    {
        public CumprimentoConfig()
        {
            HasKey(c => c.Id);

            Property(c => c.Data).HasColumnName("Cumprimento_Data").IsRequired();
            Property(c => c.InicioHH).HasColumnName("Cumprimento_InicioHH").IsRequired().HasMaxLength(2);
            Property(c => c.InicioMM).HasColumnName("Cumprimento_InicioMM").IsRequired().HasMaxLength(2);
            Property(c => c.FimHH).HasColumnName("Cumprimento_FimHH").IsOptional().HasMaxLength(2);
            Property(c => c.FimMM).HasColumnName("Cumprimento_FimMM").IsOptional().HasMaxLength(2);
            Property(c => c.DiferencaHoras).HasColumnName("Cumprimento_DiferencaHoras").HasMaxLength(5);
            Property(c => c.Usuario).HasColumnName("Cumprimento_Usuario").HasMaxLength(20).IsRequired();
            Property(c => c.EntidadeId).IsRequired().HasColumnName("Cumprimento_Instituicao");
            Property(c => c.SentenciadoId).IsRequired().HasColumnName("Cumprimento_Sentenciado");

            HasRequired(c => c.Entidade);
            HasRequired(c => c.Sentenciado);
        }
    }
}
