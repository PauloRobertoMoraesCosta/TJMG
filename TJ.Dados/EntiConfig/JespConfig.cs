using System.Data.Entity.ModelConfiguration;
using TJ.Dominio.Entidades;

namespace TJ.Dados.EntiConfig
{
    public class JespConfig : EntityTypeConfiguration<Jesp>
    {
        public JespConfig()
        {
            HasKey(j => j.Id);

            Property(j => j.Descricao).HasColumnName("Jesp_Descricao").HasMaxLength(150).IsRequired();
            Property(j => j.Email).HasColumnName("Jesp_Email").HasMaxLength(100);
            Property(j => j.Endereco).HasColumnName("Jesp_Endereco").HasMaxLength(150).IsRequired();
            Property(j => j.Telefone).HasColumnName("Jesp_Telefone").HasMaxLength(100);
            Property(j => j.BairroId).HasColumnName("Jesp_BairroId");
            Property(j => j.CidadeId).HasColumnName("Jesp_CidadeId");
            Property(j => j.HorarioDeFuncionamento).HasColumnName("Jesp_HorarioFuncionamento").HasMaxLength(100);

            HasRequired(j => j.Bairro);
            HasRequired(j => j.Cidade);
        }
    }
}
