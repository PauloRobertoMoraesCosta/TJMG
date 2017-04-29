using System.Collections.Generic;

namespace TJ.Dominio.Entidades
{
    public class Cidade
    {
        public Cidade()
        {
            Sentenciados = new List<Sentenciado>();
            Entidades = new List<Entidade>();
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string EstadoSigla { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual ICollection<Sentenciado> Sentenciados { get; set; }
        public virtual ICollection<Entidade> Entidades { get; set; }
    }
}
