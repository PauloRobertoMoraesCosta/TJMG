using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TJ.Dominio.Entidades
{
    public class CumprimentoMes
    {
        public int Id { get; set; }
        public string Mes { get; set; }
        public int Ano { get; set; }
        public string Observacao { get; set; }
        public string TempoCumprido { get; set; }
        public int SentenciadoEntidadeId { get; set; }
        public int UsuarioId { get; set; }
        
        public SentenciadoEntidade sentenciadoEntidade { get; set; }
        public Usuario usuario { get; set; }
        public virtual ICollection<Cumprimento> Cumprimentos { get; set; }

        public bool verificarDuplicidade(CumprimentoMes cumprimento, IEnumerable<CumprimentoMes> cumprimentos )
        {
            foreach (CumprimentoMes cumprimentoMes in cumprimentos)
            {
                if (cumprimentoMes.SentenciadoEntidadeId == cumprimento.SentenciadoEntidadeId && cumprimentoMes.Mes == cumprimento.Mes && cumprimentoMes.Ano == cumprimento.Ano && cumprimentoMes.Id != cumprimento.Id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
