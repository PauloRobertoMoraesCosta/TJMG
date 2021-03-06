﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using TJ.Dominio.Entidades;

namespace TJ.Dados.EntiConfig
{
    /// <summary>
    /// Classe com as configurações da classe/tabela usuario
    /// </summary>
    public class UsuarioConfig : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfig()
        {
            HasKey(u => u.Id);

            Property(u => u.Login).HasColumnName("Usuario_Login").IsRequired().HasMaxLength(20).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UN_Usuario") { IsUnique = true }));
            Property(u => u.Senha).HasColumnName("Usuario_Senha").IsRequired().HasMaxLength(15);
            Property(u => u.Nome).HasColumnName("Usuario_Nome").IsRequired().HasMaxLength(50);
            Property(u => u.DataCadastro).HasColumnName("Usuario_DataCadastro").IsRequired();
            Property(u => u.DadosRegistro).HasColumnName("Usuario_DadosRegistro").HasMaxLength(100);
            Property(u => u.Ativo).HasColumnName("Usuario_Ativo").IsRequired().HasMaxLength(5);
            Property(u => u.Super).HasColumnName("Usuario_Super").IsRequired().HasMaxLength(5);


            //HasMany(u => u.SentenciadosCadastro);
            // HasMany(u => u.SentenciadosAlteracao).WithOptional(u => u.UsuarioAlteracao);
            //HasMany(u => u.EntidadesCadastro).WithRequired(u => u.UsuarioCadastro);
            //HasMany(u => u.EntidadesCadastro).WithOptional(u => u.UsuarioAlteracao);
        }
    }
}
