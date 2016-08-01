using System;

namespace TJ.Dominio.verifications
{
    public class DominioException : ApplicationException
    {
        public DominioException(string mensagem) : base(mensagem)
        {

        }
    }
}
