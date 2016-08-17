using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TJ.Dominio.Entidades;

namespace TJ.Dados.EntiConfig
{
    public class EntidadeConfig : EntityTypeConfiguration<Entidade>
    {
        public EntidadeConfig()
        {
            HasKey(e => e.Id);

            Property(e => e.Id).HasColumnName("Entidade_Id");
            Property(e => e.Nome).HasColumnName("Entidade_Nome").IsRequired().HasMaxLength(30);
        }
    }
}
