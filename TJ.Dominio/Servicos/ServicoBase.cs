using System;
using System.Collections.Generic;
using TJ.Dominio.Interfaces.Repositorios;
using TJ.Dominio.Interfaces.Servicos;

namespace TJ.Dominio.Servicos
{
    public class ServicoBase<TEntity> : IServicoBase<TEntity> where TEntity : class
    {
        private readonly IRepositorioBase<TEntity> _repositorio;

        public ServicoBase(IRepositorioBase<TEntity> repositorio)
        {
            _repositorio = repositorio;
        }


        public void Adiciona(TEntity objeto)
        {
            _repositorio.Adiciona(objeto);
        }

        public TEntity RetornaPorId(int Id)
        {
            return _repositorio.RetornaPorId(Id);
        }

        public IEnumerable<TEntity> RetornaTodos()
        {
            return _repositorio.RetornaTodos();
        }

        public IEnumerable<TEntity> RetornaTodosAsNoTracking()
        {
            return _repositorio.RetornaTodosAsNoTracking();
        }

        public void Alterar(TEntity objeto)
        {
            _repositorio.Alterar(objeto);
        }

        public void Remover(TEntity objeto)
        {
            _repositorio.Remover(objeto);
        }

        public void Dispose()
        {
            _repositorio.Dispose();
        }

        public void ReloadElement(TEntity objeto)
        {
            _repositorio.ReloadElement(objeto);
        }

        public void Reload(IEnumerable<TEntity> objetos)
        {
            _repositorio.Reload(objetos);
        }
    }
}
