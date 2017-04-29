using System.Data.Entity.ModelConfiguration;
using TJ.Dominio.Entidades;

namespace TJ.Dados.EntiConfig
{
    public class CidadeConfig : EntityTypeConfiguration<Cidade>
    {
        public CidadeConfig()
        {
            HasKey(c => c.Id);

            Property(c => c.Id).HasColumnName("Cidade_Codigo");
            Property(c => c.Nome).HasColumnName("Cidade_Nome").IsRequired().HasMaxLength(40);
            Property(c => c.EstadoSigla).IsRequired().HasColumnName("Cidade_EstadoSigla").HasMaxLength(2);

            HasRequired(c => c.Estado);

            HasMany(c => c.Sentenciados);
            HasMany(c => c.Entidades);
        }
    }
}
