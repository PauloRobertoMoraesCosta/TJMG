using System.Data.Entity.ModelConfiguration;
using TJ.Dominio.Entidades;

namespace TJ.Dados.EntiConfig
{
    public class EstadoConfig : EntityTypeConfiguration<Estado>
    {
        public EstadoConfig()
        {
            HasKey(e => e.Sigla);

            Property(e => e.Sigla).HasColumnName("Estado_Sigla").HasMaxLength(2); 
            Property(e => e.Nome).HasColumnName("Estado_Nome").IsRequired().HasMaxLength(20);

            HasMany(e => e.Cidades);
        }
    }
}
