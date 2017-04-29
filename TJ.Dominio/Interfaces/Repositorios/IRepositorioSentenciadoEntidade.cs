using System.Collections.Generic;
using TJ.Dominio.Entidades;

namespace TJ.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioSentenciadoEntidade : IRepositorioBase<SentenciadoEntidade>
    {
        IEnumerable<SentenciadoEntidade> RetornarPorSentenciado(int sentenciadoId);
    }
}
