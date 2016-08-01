using System.Windows;

namespace TJ.View
{
    public class Mensagens
    {
        public static void MensagemAlertaOkCancel(string mensagem)
        {
            MessageBox.Show(mensagem, "Alerta", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
        }
        public static void MensagemAlertaOk(string mensagem)
        {
            MessageBox.Show(mensagem, "Alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
