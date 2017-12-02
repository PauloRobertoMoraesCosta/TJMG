using System.Collections.Generic;

namespace TJ.Dominio.Entidades
{
    public class Bairro
    {
        public Bairro()
        {
            Sentenciados = new HashSet<Sentenciado>();
            Entidades = new HashSet<Entidade>();
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Sentenciado> Sentenciados { get; set; }
        public virtual ICollection<Entidade> Entidades { get; set; }
    }
}
