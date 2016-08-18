namespace TJ.Dominio.Entidades
{
    public class Telefone
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Descricao { get; set; }
        public virtual Endereco Endereco { get; set; }
    }
}
