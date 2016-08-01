using System;

namespace TJ.Dominio.Entidades
{
    /// <summary>
    /// Classe com as regras para entidade Usuario
    /// </summary>
    public class Usuario
    {

        #region "Atributos"
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public DateTime DataCadastro{ get; set; }
        public string Super { get; set; }
        public string Ativo { get; set; }
        
        #endregion

    }
}
