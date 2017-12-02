using System;

namespace TJ.Dominio.Entidades
{
    public class Cumprimento
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HorarioEntrada { get; set; }
        public TimeSpan HorarioSaida { get; set; }
        public TimeSpan HorarioEntradaAlmoco { get; set; }
        public TimeSpan HorarioSaidaAlmoco { get; set; }
        public TimeSpan DiferencaHoras { get; set; }
        public string Usuario { get; set; }
        public int CumprimentoMesId { get; set; }

        public CumprimentoMes cumprimentoMes { get; set; }

    }
}
