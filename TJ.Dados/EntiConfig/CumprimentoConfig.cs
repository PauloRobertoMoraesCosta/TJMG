using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using TJ.Dominio.Entidades;

namespace TJ.Dados.EntiConfig
{
    public class CumprimentoConfig : EntityTypeConfiguration<Cumprimento>
    {
        public CumprimentoConfig()
        {
            HasKey(c => c.Id);

            Property(c => c.Data).HasColumnName("Cumprimento_Data").IsRequired();//.HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UN_Cumprimento") { IsUnique = true }));
            Property(c => c.HorarioEntrada).HasColumnName("Cumprimento_HorarioEntrada").IsRequired();
            Property(c => c.HorarioSaida).HasColumnName("Cumprimento_HorarioSaida").IsRequired();
            Property(c => c.HorarioEntradaAlmoco).HasColumnName("Cumprimento_HorarioEntradaAlmoco").IsOptional();
            Property(c => c.HorarioSaidaAlmoco).HasColumnName("Cumprimento_HorarioSaidaAlmoco").IsOptional();
            Property(c => c.DiferencaHoras).HasColumnName("Cumprimento_DiferencaHoras");
            Property(c => c.CumprimentoMesId).HasColumnName("Cumprimento_CumprimentoMesId").IsRequired();
            
            HasRequired(c => c.cumprimentoMes).WithMany(cm => cm.Cumprimentos).HasForeignKey(ccm => ccm.CumprimentoMesId);
        }
    }
}
