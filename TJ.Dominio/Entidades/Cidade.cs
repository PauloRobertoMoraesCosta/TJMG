namespace TJ.Dominio.Entidades
{
    public class Cidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual Estado Estado { get; set; }
    }
}
