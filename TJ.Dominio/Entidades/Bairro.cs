using System.Collections.Generic;

namespace TJ.Dominio.Entidades
{
    public class Bairro
    {
        public Bairro()
        {
            Sentenciados = new List<Sentenciado>();
            Entidades = new List<Entidade>();
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Sentenciado> Sentenciados { get; set; }
        public virtual ICollection<Entidade> Entidades { get; set; }
    }
}
