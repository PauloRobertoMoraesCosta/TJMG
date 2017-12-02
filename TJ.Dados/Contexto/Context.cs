using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using TJ.Dados.EntiConfig;
using TJ.Dominio.Entidades;

namespace TJ.Dados.Contexto
{
    /// <summary>
    /// Classe usada na conexão com o banco de dados usando Entity Framework
    /// </summary>
    public class Context : DbContext
    {
        public Context()
            : base("TJ.View.Properties.Settings.StringConection")
        {
            
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Entidade> Entidades { get; set; }
        public DbSet<Bairro> Bairros { get; set; }
        public DbSet<Sentenciado> Sentenciados { get; set; }
        public DbSet<SentenciadoEntidade> SentenciadoEntidades { get; set; }
        public DbSet<Cumprimento> Cumprimentos { get; set; }
        public DbSet<CumprimentoMes> CumprimentoMes { get; set; }
        public DbSet<Jesp> Jesps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            
            modelBuilder.Properties().Where(p => p.Name == p.ReflectedType.Name + "ID").Configure(p => p.IsKey());
            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));
            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(100));

            modelBuilder.Configurations.Add(new UsuarioConfig());
            modelBuilder.Configurations.Add(new EntidadeConfig());
            modelBuilder.Configurations.Add(new EstadoConfig());
            modelBuilder.Configurations.Add(new CidadeConfig());
            modelBuilder.Configurations.Add(new BairroConfig());
            modelBuilder.Configurations.Add(new SentenciadoConfig());
            modelBuilder.Configurations.Add(new SentenciadoEntidadeConfig());
            modelBuilder.Configurations.Add(new CumprimentoConfig());
            modelBuilder.Configurations.Add(new CumprimentoMesConfig());
            modelBuilder.Configurations.Add(new JespConfig());
        }

        // Override do SaveChanges para que a DataCadastro seja sempre hoje
        public override int SaveChanges()
        {
            try
            {
                foreach (
                    var entry in
                        ChangeTracker.Entries()
                            .Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                    }
                    if (entry.State == EntityState.Modified)
                    {
                        entry.Property("DataCadastro").IsModified = false;
                    }
                }
                return base.SaveChanges();
            }
            catch (DbEntityValidationException dbEntityValidationException)
            {
                StreamWriter sw =
                        new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "log " + DateTime.Now.ToString("ddMMyyyyhhmm") + ".txt");
                foreach (var eve in dbEntityValidationException.EntityValidationErrors)
                {
                    sw.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        sw.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                sw.Close();
                throw;
            }
        }
    }
}