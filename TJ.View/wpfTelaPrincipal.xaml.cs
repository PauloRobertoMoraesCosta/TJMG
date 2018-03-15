using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ninject;
using TJ.Apresentacao;
using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;
using System.Timers;
using System.Windows.Media;
using ToastNotifications.Core;

namespace TJ.View
{
    /// <summary>
    /// Interaction logic for wpfTelaPrincipal.xaml
    /// </summary>
    public partial class WpfTelaPrincipal : Window
    {
        private Usuario usuarioLogado;
        private bool mensagemDeFechamento = true;
        public ToastViewModel _vm = new ToastViewModel();

        #region "Contrutores"
        public WpfTelaPrincipal(Usuario usuario)
        {
            try
            {
                InitializeComponent();
                usuarioLogado = usuario;

                if (usuarioLogado.Super.Equals("false", StringComparison.CurrentCultureIgnoreCase))
                {
                    lblUsuarios.IsEnabled = false;
                    lblUsuarios.Opacity = 0.5;
                    lblDadosJesp.IsEnabled = false;
                    lblDadosJesp.Opacity = 0.5;
                }
                updateDadosTela();
            }
            catch (Exception exception)
            {
                _vm.ShowError("Ao iniciar a tela: " + exception.Message);
            }
        }

        #endregion

        private void updateDadosTela()
        {
            try
            {
                lblDataHora.Content = DateTime.Now.Date.ToString().Substring(0, 10);
            }
            catch (Exception exception)
            {
                _vm.ShowError("Ao carregar os dados da tela: " + exception.Message);
            }

        }

        protected void InitializeTab(UserControl control, string header)
        {
            try
            {
                if (TbcPrincipal.Items.Count == 0)
                {
                    MyTabItem ucHeader = new MyTabItem();
                    ucHeader.Title = header;
                    ucHeader.Content = control;
                    TbcPrincipal.Items.Add(ucHeader);
                    TbcPrincipal.BorderThickness = new Thickness(0, 1, 0, 1);
                    ucHeader.Focus();
                }
                else
                {
                    if (!FindTab(control))
                    {
                        MyTabItem ucHeader = new MyTabItem();
                        ucHeader.Title = header;
                        ucHeader.Content = control;
                        TbcPrincipal.Items.Add(ucHeader);
                        ucHeader.Focus();
                    }
                }
            }
            catch (Exception exception)
            {
                _vm.ShowError("Ao inicializar a aba: " + exception.Message);
            }
        }

        private bool FindTab(UserControl control)
        {
            try
            {
                for (int i = 0; i < TbcPrincipal.Items.Count; i++)
                {
                    if ((TbcPrincipal.Items[i] as MyTabItem).Content.GetType() == control.GetType())
                    {
                        (TbcPrincipal.Items[i] as MyTabItem).Focus();
                        return true;
                    }
                }
                return false;
            }
            catch (NullReferenceException nullReference)
            {
                _vm.ShowError("Abas ainda não criadas: " + nullReference.Message);
                return false;
            }
            catch (Exception exception)
            {
                _vm.ShowError(exception.Message);
                return false;
            }
        }

        private void lblUsuarios_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UCUsuariosLista ucUsuario = new UCUsuariosLista();
                InitializeTab(ucUsuario, "Usuários");
            }
            catch (Exception exception)
            {
                _vm.ShowError("Ao iniciar a tela de usuários: " + exception.Message);
            }
        }

        private void lblBairro_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UCBairrosLista ucBairro = new UCBairrosLista();
                InitializeTab(ucBairro, "Bairros");
            }
            catch (Exception exception)
            {
                _vm.ShowError("Ao iniciar a tela de bairros: " + exception.Message);
            }
        }

        private void lblReeducando_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UCReeducandoLista ucReeducando = new UCReeducandoLista(usuarioLogado);
                InitializeTab(ucReeducando, "Reeducando");
            }
            catch (Exception exception)
            {
                _vm.ShowError("Ao iniciar a tela de reeducando: " + exception.Message);
            }
        }

        private void lblInstituicoes_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UCEntidadesLista ucEntidade = new UCEntidadesLista(usuarioLogado);
                InitializeTab(ucEntidade, "Entidades");
            }
            catch (Exception exception)
            {
                _vm.ShowError("Ao iniciar a tela de entidades: " + exception.Message);
            }
        }

        private void lblLancamentos_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UCCumprimentoPSCLista ucCumprimentoPSC = new UCCumprimentoPSCLista(usuarioLogado);
                InitializeTab(ucCumprimentoPSC, "Cumprimento PSC");
            }
            catch (Exception exception)
            {
                _vm.ShowError("Ao iniciar a tela de cumprimentos: " + exception.Message);
            }
        }
        private void lblDadosJesp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                winDadosJesp winDadosJesp = new winDadosJesp();
                winDadosJesp.ShowDialog();
            }
            catch (Exception exception)
            {
                _vm.ShowError("Ao iniciar a tela de dados do Jesp: " + exception.Message);
            }
        }

        private void Encaminhamento_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UCRelatorioEncaminhamento ucRelatorio = new UCRelatorioEncaminhamento(usuarioLogado);
                InitializeTab(ucRelatorio, "Encaminhamento");
            }
            catch (Exception exception)
            {
                _vm.ShowError("Ao iniciar a tela do encaminhamento: " + exception.Message);
            }
        }

        private void Frequencia_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UCRelatorioFrequencia ucRelatorio = new UCRelatorioFrequencia(usuarioLogado);
                InitializeTab(ucRelatorio, "Folha de Frequência");
            }
            catch (Exception exception)
            {
                _vm.ShowError("Ao iniciar a tela da folha de frequência: " + exception.Message);
            }
        }

        private void RelCumprimento_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UCRelatorioCumprimento ucRelatorio = new UCRelatorioCumprimento();
                InitializeTab(ucRelatorio, "Relatório de cumprimentos");
            }
            catch (Exception exception)
            {
                _vm.ShowError("Ao iniciar a tela do relatório de cumprimento: " + exception.Message);
            }
        }

        private void lblTtrocarUsuario_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (Mensagens.MensagemConfirmOkCancel("Deseja trocar de usuário?") == MessageBoxResult.OK)
                {
                    mensagemDeFechamento = false;
                    Login login = new Login();//MinhaNinject.Kernel.Get<Login>();
                    login.Show();
                    login.inicializar();
                    Close();
                }
            }
            catch (Exception exception)
            {
                _vm.ShowError("Ao iniciar a tela do encaminhamento: " + exception.Message);
            }
        }

        private void lblSair_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception exception)
            {
                _vm.ShowError("Ao iniciar a tela do encaminhamento: " + exception.Message);
            }
        }

        private void wpfTelaPrincipal_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                if (mensagemDeFechamento)
                {
                    if (Mensagens.MensagemConfirmOkCancel("Deseja sair do sistema?") != MessageBoxResult.OK)
                        e.Cancel = true;
                }
                else
                    mensagemDeFechamento = true;
            }
            catch (Exception exception)
            {
                _vm.ShowError("Ao iniciar a tela do encaminhamento: " + exception.Message);
            }
        }
    }
}