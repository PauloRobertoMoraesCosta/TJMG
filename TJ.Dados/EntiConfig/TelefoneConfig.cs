using System.Data.Entity.ModelConfiguration;
using TJ.Dominio.Entidades;

namespace TJ.Dados.EntiConfig
{
    public class TelefoneConfig : EntityTypeConfiguration<Telefone>
    {
        public TelefoneConfig()
        {
            HasKey(t => t.Id);

            Property(t => t.Id).HasColumnName("Telefone_Id");
            Property(t => t.Descricao).HasColumnName("Telefone_Descricao").HasMaxLength(150);
            Property(t => t.Numero).HasColumnName("Telefone_Numero").IsRequired().HasMaxLength(15);
            Property(t => t.Endereco.Id).HasColumnName("Telefone_Endereco").IsRequired();
        } 
    }
}
