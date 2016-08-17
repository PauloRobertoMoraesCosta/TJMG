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

        public static void MensagemErroOk(string mensagem)
        {
            MessageBox.Show(mensagem, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static MessageBoxResult MensagemConfirmOkCancel(string mensagem)
        {
            return MessageBox.Show(mensagem, "Dúvida", MessageBoxButton.OKCancel, MessageBoxImage.Question);
        }
    }
}
