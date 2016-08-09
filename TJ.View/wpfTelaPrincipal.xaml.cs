using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Ninject;
using TJ.Apresentacao;
using TJ.Dominio.Entidades;

namespace TJ.View
{
    /// <summary>
    /// Interaction logic for wpfTelaPrincipal.xaml
    /// </summary>
    public partial class WpfTelaPrincipal : Window
    {
        protected readonly Usuario usuarioLogado;

        public WpfTelaPrincipal()
        {
            InitializeComponent();
            updateDadosTela();
        }

        public WpfTelaPrincipal(Usuario usuario)
        {
            InitializeComponent();
            usuarioLogado = usuario;
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
                    TabItem tabItemMovimentacoesLencoes = new TabItem();
                    tabItemMovimentacoesLencoes.Header = header;
                    tabItemMovimentacoesLencoes.Content = control;
                    TbcPrincipal.Items.Add(tabItemMovimentacoesLencoes);
                    tabItemMovimentacoesLencoes.Focus();
                }
                else
                {
                    if (!FindTab(control))
                    {
                        TabItem tabItemMovimentacoesLencoes = new TabItem();
                        tabItemMovimentacoesLencoes.Header = header;
                        tabItemMovimentacoesLencoes.Content = control;
                        TbcPrincipal.Items.Add(tabItemMovimentacoesLencoes);
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
                for (int i = 0; i < TbcPrincipal.Items.Count + 1; i++)
                {
                    if ((TbcPrincipal.Items[i] as TabItem).Content.GetType() == control.GetType())
                    {
                        (TbcPrincipal.Items[i] as TabItem).Focus();
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
                throw new Exception("Erro inesperado: " + ex.Message);
            }
        }

        private void lblUsuarios_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UCUsuarios ucUsuario = MinhaNinject.Kernel.Get<UCUsuarios>();
            InitializeTab(ucUsuario, "Usuarios");
        }
    }
}
