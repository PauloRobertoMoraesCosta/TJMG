using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TJ.Dominio.Entidades;

namespace TJ.Dados.EntiConfig
{
    public class CidadeConfig : EntityTypeConfiguration<Cidade>
    {
        public CidadeConfig()
        {
            HasKey(c => c.Id);

            Property(c => c.Id).HasColumnName("Cidade_Id");
            Property(c => c.Nome).HasColumnName("Cidade_Nome").IsRequired().HasMaxLength(40).HasColumnAnnotation(IndexAnnotation.AnnotationName,new IndexAnnotation(new IndexAttribute("UN_Cidade") {IsUnique = true}));
            Property(c => c.Estado.Id).HasColumnName("Cidade_Estado").IsRequired();
        }
    }
}
