using System;
using System.Collections.Generic;
using System.Linq;

namespace TJ.Dominio.Entidades
{
    public class SentenciadoEntidade
    {
        public int Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string HorarioDeCumprimento { get; set; }
        public string SituacaoCumprimento { get; set; }
        public string AtividadePSC { get; set; }
        public int SentenciadoId { get; set; }
        public int EntidadeId { get; set; }
        public virtual Sentenciado Sentenciado { get; set; }
        public virtual Entidade Entidade { get; set; }

        public virtual ICollection<CumprimentoMes> CumprimentosMes { get; set; }

        public SentenciadoEntidade()
        {
            CumprimentosMes = new HashSet<CumprimentoMes>();
        }
    }
}
