using System.Collections.Generic;

namespace TJ.Dominio.Entidades
{
    public class Estado
    {
        //public Estado()
        //{
        //    this.Cidades = new List<Cidade>();
        //}
        public string Sigla { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Cidade> Cidades { get; set; }

        public Estado()
        {
            Cidades = new HashSet<Cidade>();
        }
    }
}
