using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TJ.Dominio.Entidades;

namespace TJ.Dados.EntiConfig
{
    public class EnderecoConfig : EntityTypeConfiguration<Endereco>
    {
        public EnderecoConfig()
        {
            HasKey(e => e.Id);

            Property(e => e.Id).HasColumnName("Endereco_Id");
            Property(e => e.Logradouro).HasColumnName("Endereco_Logradouro").IsRequired().HasMaxLength(100);
            Property(e => e.Numero).HasColumnName("Endereco_Numero").IsRequired().HasMaxLength(5);
            Property(e => e.Bairro).HasColumnName("Endereco_Bairro").IsRequired().HasMaxLength(20);
            Property(e => e.Cidade.Id).HasColumnName("Endereco_Cidade").IsRequired();
            Property(e => e.Entidade.Id).HasColumnName("Endereco_Cidade").IsRequired();
        }
    }
}
