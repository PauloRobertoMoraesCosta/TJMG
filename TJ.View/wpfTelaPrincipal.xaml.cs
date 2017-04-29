using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ninject;
using TJ.Apresentacao;
using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;

namespace TJ.View
{
    /// <summary>
    /// Interaction logic for wpfTelaPrincipal.xaml
    /// </summary>
    public partial class WpfTelaPrincipal : Window
    {
        protected readonly Usuario usuarioLogado;
        protected readonly IAppServiceCidade _serviceCidade;
        protected readonly IAppServiceBairro _serviceBairro;
        protected readonly IAppServiceCumprimento _serviceCumprimento;
        protected readonly IAppServiceEntidade _serviceEntidade;
        protected readonly IAppServiceEstado _serviceEstado;
        protected readonly IAppServiceSentenciado _serviceSentenciado;
        protected readonly IAppServiceSentenciadoEntidade _serviceSentenciadoEntidade;
        protected readonly IAppServiceUsuario _serviceUsuario;
        protected readonly IAppServiceJesp _serviceJesp;

        public WpfTelaPrincipal(IAppServiceCidade serviceCidade, IAppServiceBairro serviceBairro, IAppServiceCumprimento serviceCumprimento, IAppServiceEntidade serviceEntidade, IAppServiceEstado serviceEstado, IAppServiceSentenciado serviceSentenciado, IAppServiceSentenciadoEntidade serviceSentenciadoEntidade, IAppServiceUsuario serviceUsuario, IAppServiceJesp serviceJesp, Usuario usuario)
        {
            InitializeComponent();
            _serviceCidade = serviceCidade;
            _serviceBairro = serviceBairro;
            _serviceCumprimento = serviceCumprimento;
            _serviceEntidade = serviceEntidade;
            _serviceEstado = serviceEstado;
            _serviceSentenciado = serviceSentenciado;
            _serviceSentenciadoEntidade = serviceSentenciadoEntidade;
            _serviceUsuario = serviceUsuario;
            _serviceJesp = serviceJesp;
            usuarioLogado = usuario;
            
            if (usuarioLogado.Super.Equals("false", StringComparison.CurrentCultureIgnoreCase))
            {
                this.lblUsuarios.IsEnabled = false;
                this.lblUsuarios.Opacity = 0.5;
                this.lblDadosJesp.IsEnabled = false;
                this.lblDadosJesp.Opacity = 0.5;
            }
            updateDadosTela();
        }

        private void updateDadosTela()
        {
            this.lblDataHora.Content = DateTime.Now.Date.ToString().Substring(0, 10);
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
                throw new Exception("Erro inesperado: " + exception.Message);
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
                throw new NullReferenceException("Abas ainda não criadas: " + nullReference.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Algo inesperado aconteceu: " + ex.Message);
            }
        }

        private void lblUsuarios_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UCUsuarios ucUsuario = new UCUsuarios(_serviceUsuario);
                InitializeTab(ucUsuario, "Usuários");
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Algo inesperado aconteceu: " + exception.Message);
            }
        }

        private void lblReeducando_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UCReeducando ucReeducando = new UCReeducando(_serviceBairro, _serviceCidade, _serviceSentenciado, _serviceSentenciadoEntidade, _serviceEntidade, _serviceUsuario, usuarioLogado);
                InitializeTab(ucReeducando, "Reeducando");
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk(exception.Message);
            }
        }

        private void lblInstituicoes_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UCEntidades ucEntidade = new UCEntidades(_serviceEntidade, _serviceCidade, _serviceBairro, usuarioLogado);
                InitializeTab(ucEntidade, "Entidades");
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk(exception.Message);
            }
        }

        private void lblLancamentos_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UCCumprimentoPSC ucCumprimentoPSC = new UCCumprimentoPSC(_serviceSentenciado, _serviceCumprimento, _serviceSentenciadoEntidade, usuarioLogado);
                InitializeTab(ucCumprimentoPSC, "Cumprimento");
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk(exception.Message);
            }
        }

        private void lblTtrocarUsuario_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Login login = MinhaNinject.Kernel.Get<Login>();
            login.Show();
            login.inicializar();
            this.Close();
        }

        private void lblSair_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mensagens.MensagemConfirmOkCancel("Deseja sair do sistema?") == MessageBoxResult.OK)
                this.Close();
        }

        private void wpfTelaPrincipal_Closing(object sender, CancelEventArgs e)
        {
            if (Mensagens.MensagemConfirmOkCancel("Deseja sair do sistema?") != MessageBoxResult.OK)
                e.Cancel = true;
        }

        private void lblDadosJesp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UCDadosJesp ucDadosJesp = new UCDadosJesp(_serviceJesp, _serviceBairro, _serviceCidade);
                InitializeTab(ucDadosJesp, "Dados Jesp");
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk(exception.Message);
            }
        }

        private void Encaminhamento_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UCRelatorioEncaminhamento ucRelatorio = new UCRelatorioEncaminhamento(_serviceSentenciado, _serviceEntidade, _serviceSentenciadoEntidade, usuarioLogado);
                InitializeTab(ucRelatorio, "Encaminhamento");
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk(exception.Message);
            }
        }

        private void Frequencia_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UCRelatorioFrequencia ucRelatorio = new UCRelatorioFrequencia(_serviceSentenciado, _serviceEntidade, _serviceSentenciadoEntidade, usuarioLogado);
                InitializeTab(ucRelatorio, "Folha de Frequência");
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk(exception.Message);
            }
        }
    }
}
