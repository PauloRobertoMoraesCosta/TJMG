using System;

namespace TJ.Dominio.Entidades
{
    public class Cumprimento
    {
        public int Id { get; set; }
        public int SentenciadoId { get; set; }
        public int EntidadeId { get; set; }
        public DateTime Data { get; set; }
        public string InicioHH { get; set; }
        public string InicioMM { get; set; }
        public string FimHH { get; set; }
        public string FimMM { get; set; }
        public string DiferencaHoras { get; set; }
        public string Usuario { get; set; }
        public virtual Sentenciado Sentenciado { get; set; }
        public virtual Entidade Entidade { get; set; }
    }
}
