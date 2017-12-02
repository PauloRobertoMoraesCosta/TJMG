using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TJ.Dados.Contexto;
using TJ.Dados.Verifications;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Repositorios;

namespace TJ.Dados.Repositorios
{
    public class RepositorioCumprimentoMes : RepositorioBase<CumprimentoMes>, IRepositorioCumprimentoMes
    {
        public IEnumerable<CumprimentoMes> RetornarPorSentenciadoEntidade(int sentenciadoEntidadeId)
        {
            try
            {
                return
                    db.CumprimentoMes.Where(
                        c => c.SentenciadoEntidadeId == sentenciadoEntidadeId).Include(c => c.sentenciadoEntidade).Include(c => c.Cumprimentos);
            }
            catch (Exception exception)
            {
                throw new DadosException("Erro ao retornar cumprimentoMes: " + exception.Message);
            }
        }
        public IEnumerable<CumprimentoMes> RetornarPorSentenciado(int sentenciadoId)
        {
            try
            {
                return
                    db.CumprimentoMes.Where(
                        c => c.sentenciadoEntidade.SentenciadoId == sentenciadoId).Include(c => c.sentenciadoEntidade).Include(c => c.Cumprimentos).Include(c => c.usuario).Include(c => c.sentenciadoEntidade.Entidade);
            }
            catch (Exception exception)
            {
                throw new DadosException("Erro ao retornar cumprimentoMes por sentenciado: " + exception.Message);
            }
        }

        public CumprimentoMes RetornaPorId(int Id)
        {
            try
            {
                return (db.CumprimentoMes.Where(c => c.Id == Id).Include(c => c.sentenciadoEntidade).Include(c => c.Cumprimentos).Include(c => c.usuario).Include(c => c.sentenciadoEntidade.Entidade) as CumprimentoMes);
            }
            catch (Exception exception)
            {
                throw new DadosException("Erro ao retornar cumprimentoMes por sentenciado: " + exception.Message);
            }
        }

    }
}
