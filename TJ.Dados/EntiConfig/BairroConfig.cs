using System.Data.Entity.ModelConfiguration;
using TJ.Dominio.Entidades;

namespace TJ.Dados.EntiConfig
{
    public class BairroConfig : EntityTypeConfiguration<Bairro>
    {
        public BairroConfig()
        {
            HasKey(b => b.Id);

            Property(b => b.Id).HasColumnName("Bairro_Codigo");
            Property(b => b.Nome).HasColumnName("Bairro_Nome").IsRequired().HasMaxLength(40);

            HasMany(b => b.Sentenciados);
            HasMany(b => b.Entidades);
        }
    }
}
