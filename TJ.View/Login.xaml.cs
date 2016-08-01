using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;

namespace TJ.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        protected readonly IAppServiceUsuario _serviceUsuario;
        public Login(IAppServiceUsuario serviceUsuario)
        {
            _serviceUsuario = serviceUsuario;
            InitializeComponent();
        }

        private void btnLogar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Usuario usu = _serviceUsuario.logaUsuario(cbxUsuario.Text, pswSenha.Password);
                if (usu != null)
                {
                    App.Current.MainWindow = new wpfTelaPrincipal();
                    App.Current.MainWindow.Show();
                    this.Close();
                }
            }
            catch (Exception exception)
            {
                Mensagens.MensagemAlertaOk("Algo de errado aconteceu: " + exception.Message);
            }
        }

        private void habilitaBotaoLogar()
        {
            if (cbxUsuario.Text.Any() && pswSenha.Password.Any())
                btnLogar.IsEnabled = true;
            else
                btnLogar.IsEnabled = false;
        }

        private void pswSenha_PasswordChanged(object sender, RoutedEventArgs e)
        {
            habilitaBotaoLogar();
        }

        public void inicializar()
        {
            var usu = _serviceUsuario.RetornaTodosAsNoTracking();
            foreach (Usuario usuario in usu)
            {
                cbxUsuario.Items.Add(usuario.Login);
            }
            lblInicial.Visibility = Visibility.Hidden;
            cbxUsuario.Focus();
        }

        private void cbxUsuario_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            habilitaBotaoLogar();
        }

        private void cbxUsuario_DropDownClosed(object sender, EventArgs e)
        {
            habilitaBotaoLogar();
        }
    }
}
