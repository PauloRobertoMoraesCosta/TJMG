using System.Collections.Generic;

namespace TJ.Dominio.Entidades
{
    public class Cidade
    {
        public Cidade()
        {
            Sentenciados = new HashSet<Sentenciado>();
            Entidades = new HashSet<Entidade>();
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string EstadoSigla { get; set; }
        public Estado Estado { get; set; }
        public virtual ICollection<Sentenciado> Sentenciados { get; set; }
        public virtual ICollection<Entidade> Entidades { get; set; }
    }
}
