using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TJ.Dados.Contexto;
using TJ.Dominio.Interfaces.Repositorios;

namespace TJ.Dados.Repositorios
{
    /// <summary>
    /// Classe com o CRUD básico
    /// </summary>
    /// <typeparam name="TEntity">Informar a classe a ser usada</typeparam>
    public class RepositorioBase<TEntity> : IDisposable, IRepositorioBase<TEntity> where TEntity : class
    {
        protected Context db = new Context();

        public void Adiciona(TEntity objeto)
        {
            db.Set<TEntity>().Add(objeto);
            db.SaveChanges();
        }

        public TEntity RetornaPorId(int Id)
        {
            return db.Set<TEntity>().Find(Id);
        }


        public IEnumerable<TEntity> RetornaTodos()
        {
            return db.Set<TEntity>();
        }

        public IEnumerable<TEntity> RetornaTodosAsNoTracking()
        {
            return db.Set<TEntity>().AsNoTracking();
        }

        public void Alterar(TEntity objeto)
        {
            db.Entry(objeto).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Remover(TEntity objeto)
        {
            db.Set<TEntity>().Remove(objeto);
            db.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException("Método dispose da classe RepositorioBase ainda não implementado");
        }

        public void ReloadElement(TEntity objeto)
        {
            db.Entry(objeto).Reload();
        }

        public void Reload(IEnumerable<TEntity> objetos)
        {
            for (int i = 0; i < objetos.Count(); i++)
            {
                db.Entry(objetos.ElementAt(i)).Reload();
            }
        }
    }
}
