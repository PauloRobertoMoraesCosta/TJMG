using System.Data.Entity.ModelConfiguration;
using TJ.Dominio.Entidades;

namespace TJ.Dados.EntiConfig
{
    public class EnderecoConfig : EntityTypeConfiguration<Endereco>
    {
        public EnderecoConfig()
        {
            HasKey(e => e.Id);

            Property(e => e.Id).HasColumnName("Endereco_Id");
            Property(e => e.Logradouro).HasColumnName("Endereco_Logradouro").IsRequired().HasMaxLength(50);
            Property(e => e.Numero).HasColumnName("Endereco_Numero").IsRequired().HasMaxLength(5);
            Property(e => e.PontoReferencia).HasColumnName("Endereco_PontoReferencia").HasMaxLength(200);
            Property(e => e.Responsavel).HasColumnName("Endereco_Responsavel").HasMaxLength(100);
            Property(e => e.Telefones).HasColumnName("Endereco_Telefone").HasMaxLength(50);
            Property(e => e.BairroId).IsRequired().HasColumnName("Endereco_BairroId");
            Property(e => e.CidadeId).IsRequired().HasColumnName("Endereco_CidadeId");
            Property(e => e.EntidadeId).IsRequired().HasColumnName("Endereco_EntidadeId");

            HasRequired(e => e.Bairro);
            HasRequired(e => e.Cidade);
            HasRequired(e => e.Entidade);
        }
    }
}