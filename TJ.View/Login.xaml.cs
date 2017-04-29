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
        protected readonly IAppServiceUsuario _serviceUsuario;
        public Login(IAppServiceUsuario serviceUsuario)
        {
            _serviceUsuario = serviceUsuario;
            InitializeComponent();
        }

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
                IEnumerable<Usuario> usuarios = _serviceUsuario.RetornaUsuariosAtivosAsNoTracking().ToList();
                cbxUsuario.ItemsSource = usuarios;
                cbxUsuario.DisplayMemberPath = "Login";
                lblInicial.Content = "Favor selecionar o usuário e fornecer a senha de acesso.";
                lblInicial.Foreground = new SolidColorBrush(Colors.Blue);
                cbxUsuario.Focus();
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Algo inesperado aconteceu: " + exception.Message);
            }
        }

        private void btnLogar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Usuario usuario = _serviceUsuario.logaUsuario(cbxUsuario.Text, pswSenha.Password);
                if (usuario != null)
                {
                    IAppServiceCidade serviceCidade = MinhaNinject.Kernel.Get<IAppServiceCidade>();
                    IAppServiceBairro serviceBairro = MinhaNinject.Kernel.Get<IAppServiceBairro>();
                    IAppServiceCumprimento serviceCumprimento = MinhaNinject.Kernel.Get<IAppServiceCumprimento>();
                    IAppServiceEntidade serviceEntidade = MinhaNinject.Kernel.Get<IAppServiceEntidade>();
                    IAppServiceEstado serviceEstado = MinhaNinject.Kernel.Get<IAppServiceEstado>();
                    IAppServiceSentenciado serviceSentenciado = MinhaNinject.Kernel.Get<IAppServiceSentenciado>();
                    IAppServiceSentenciadoEntidade serviceSentenciadoEntidade = MinhaNinject.Kernel.Get<IAppServiceSentenciadoEntidade>();
                    IAppServiceJesp serviceJesp = MinhaNinject.Kernel.Get<IAppServiceJesp>();

                    App.Current.MainWindow = new WpfTelaPrincipal(serviceCidade, serviceBairro, serviceCumprimento, serviceEntidade, serviceEstado, serviceSentenciado, serviceSentenciadoEntidade, _serviceUsuario, serviceJesp, usuario);
                    App.Current.MainWindow.Show();
                    this.Close();
                }
            }
            catch (Exception exception)
            {
                Mensagens.MensagemAlertaOk("Algo inesperado aconteceu: " + exception.Message);
            }
        }

        private void cbxUsuario_KeyUp(object sender, KeyEventArgs e)
        {
            habilitaBotaoLogar();
            if (e.Key.Equals(Key.Enter))
                pswSenha.Focus();
        }

        private void cbxUsuario_DropDownClosed(object sender, EventArgs e)
        {
            habilitaBotaoLogar();
        }

        private void pswSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
                btnLogar.Focus();
        }
        private void pswSenha_PasswordChanged(object sender, RoutedEventArgs e)
        {
            habilitaBotaoLogar();
        }

    }
}
