using System;
using System.Collections;
using System.Collections.Generic;

namespace TJ.Dominio.Entidades
{
    public class CumprimentoMes
    {
        public int Id { get; set; }
        public string Mes { get; set; }
        public int Ano { get; set; }
        public string Observacao { get; set; }
        public TimeSpan TempoCumprido { get; set; }
        public int SentenciadoEntidadeId { get; set; }
        public int UsuarioId { get; set; }
        
        public SentenciadoEntidade sentenciadoEntidade { get; set; }
        public Usuario usuario { get; set; }
        public virtual ICollection<Cumprimento> Cumprimentos { get; set; }
    }
}
