using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Ninject;
using TJ.Apresentacao;
using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;

namespace TJ.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        private ICollection<Usuario> usuarios;
        private Usuario usuario;

        #region "Contrutores"
        public Login()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Mensagens.MensagemErroOk("Ocorreu um problema: " + ex.Message);
            }
        }
        #endregion

        #region "Metodos avulsos"
        private void habilitaBotaoLogar()
        {
            if (cbxUsuario.Text.Any() && pswSenha.Password.Any())
                btnLogar.IsEnabled = true;
            else
                btnLogar.IsEnabled = false;
        }

        public void inicializar()
        {
            try
            {
                using (IAppServiceUsuario _serviceUsuario = MinhaNinject.Kernel.Get<IAppServiceUsuario>())
                {
                    usuarios = _serviceUsuario.RetornaUsuariosAtivosAsNoTracking().ToList();
                }
                cbxUsuario.ItemsSource = usuarios;
                cbxUsuario.DisplayMemberPath = "Login";
                lblInicial.Content = "Favor selecionar o usuário e fornecer a senha de acesso.";
                lblInicial.Foreground = new SolidColorBrush(Colors.Blue);
                cbxUsuario.Focus();
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Ocorreu um problema ao iniciar: " + exception.Message);
            }
        }

        #endregion

        #region "Eventos"
        private void btnLogar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (IAppServiceUsuario _serviceUsuario = MinhaNinject.Kernel.Get<IAppServiceUsuario>())
                {
                    usuario = _serviceUsuario.logaUsuario(cbxUsuario.Text, pswSenha.Password);
                }
                if (usuario != null)
                {
                    App.Current.MainWindow = new WpfTelaPrincipal(usuario);
                    App.Current.MainWindow.Show();
                    Close();
                }
            }
            catch (Exception exception)
            {
                Mensagens.MensagemAlertaOk("Ocorreu um problema ao logar: " + exception.Message);
            }
        }

        private void cbxUsuario_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                habilitaBotaoLogar();
                if (e.Key.Equals(Key.Enter))
                    pswSenha.Focus();
            }
            catch (Exception ex)
            {
                Mensagens.MensagemErroOk("Ocorreu um problema: " + ex.Message);
            }
        }

        private void cbxUsuario_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                habilitaBotaoLogar();
            }
            catch (Exception ex)
            {
                Mensagens.MensagemErroOk("Ocorreu um problema: " + ex.Message);
            }
        }

        private void pswSenha_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key.Equals(Key.Enter))
                    btnLogar.Focus();
            }
            catch (Exception ex)
            {
                Mensagens.MensagemErroOk("Ocorreu um problema: " + ex.Message);
            }
        }
        private void pswSenha_PasswordChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                habilitaBotaoLogar();
            }
            catch (Exception ex)
            {
                Mensagens.MensagemErroOk("Ocorreu um problema: " + ex.Message);
            }
        }
        #endregion
    }
}
