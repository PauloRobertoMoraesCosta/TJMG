using System.Data.Entity.ModelConfiguration;
using TJ.Dominio.Entidades;

namespace TJ.Dados.EntiConfig
{
    public class EntidadeConfig : EntityTypeConfiguration<Entidade>
    {
        public EntidadeConfig()
        {
            HasKey(e => e.Id);

            Property(e => e.Id).HasColumnName("Entidade_Id");
            Property(e => e.Nome).HasColumnName("Entidade_Nome").IsRequired().HasMaxLength(50);

            HasMany(e => e.Enderecos);
        }
    }
}
