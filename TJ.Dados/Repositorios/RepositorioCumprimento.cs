using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TJ.Dados.Verifications;
using TJ.Dominio.Entidades;
using TJ.Dominio.Interfaces.Repositorios;

namespace TJ.Dados.Repositorios
{
    public class RepositorioCumprimento : RepositorioBase<Cumprimento>, IRepositorioCumprimento
    {
        public IEnumerable<Cumprimento> RetornarPorSentenciado(int sentenciadoId)
        {
            try
            {
                    return
                        db.Cumprimentos.Where(
                            c => c.SentenciadoId == sentenciadoId).Include(c => c.Sentenciado).Include(c => c.Entidade);
            }
            catch (Exception exception)
            {
                throw new DadosException("Erro ao carregar os históricos: " + exception.Message);
            }
        }
    }
}
