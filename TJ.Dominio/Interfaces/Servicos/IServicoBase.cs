using System;
using System.Collections.Generic;

namespace TJ.Dominio.Interfaces.Servicos
{
    public interface IServicoBase<TEntity> : IDisposable where TEntity : class 
    {
        void Adiciona(TEntity objeto);
        TEntity RetornaPorId(int Id);
        IEnumerable<TEntity> RetornaTodos();
        IEnumerable<TEntity> RetornaTodosAsNoTracking();
        void Alterar(TEntity objeto);
        void Remover(TEntity objeto);
        void Dispose();
        void ReloadElement(TEntity objeto);
        void Reload(IEnumerable<TEntity> objetos);
    }
}
