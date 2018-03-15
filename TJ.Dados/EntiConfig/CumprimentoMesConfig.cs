using System.Data.Entity.ModelConfiguration;
using TJ.Dominio.Entidades;

namespace TJ.Dados.EntiConfig
{
    public class CumprimentoMesConfig : EntityTypeConfiguration<CumprimentoMes>
    {
        public CumprimentoMesConfig()
        {
            HasKey(c => c.Id);

            Property(c => c.Mes).HasColumnName("CumprimentoMes_Mes").IsRequired();
            Property(c => c.Ano).HasColumnName("CumprimentoMes_Ano").IsRequired();
            Property(c => c.Observacao).HasColumnName("CumprimentoMes_Observação").HasMaxLength(200);
            Property(c => c.TempoCumprido).HasColumnName("CumprimentoMes_TempoCumprido").IsRequired().HasMaxLength(50);
            Property(c => c.SentenciadoEntidadeId).HasColumnName("CumprimentoMes_SentenciadoEntidadeId").IsRequired();
            Property(c => c.UsuarioId).HasColumnName("CumprimentoMes_UsuarioId").IsRequired();
                        
            HasRequired(c => c.usuario).WithMany(u => u.CumprimentosMes).HasForeignKey(cu => cu.UsuarioId);

            HasRequired(c => c.sentenciadoEntidade).WithMany(se => se.CumprimentosMes).HasForeignKey(cse => cse.SentenciadoEntidadeId);
        }
    }
}
