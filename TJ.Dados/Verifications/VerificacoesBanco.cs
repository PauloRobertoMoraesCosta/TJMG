using System;
using System.Linq;

namespace TJ.Dados.Verifications
{
    public class VerificacoesBanco
    {
        public static string LimpaCaracteresEspeciais(string palavraInicial)
        {
            String[] especiais = new String[] { "'", "=", ",", ";", "+", "\\.", ",", "-", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°" };

            try
            {
                return especiais.Aggregate(palavraInicial, (current, letra) => current.Replace(letra, ""));
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao limpar caracteres especiais" + ex.Message);
            }
        }
    }
}
