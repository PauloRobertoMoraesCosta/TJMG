using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
                        MyTabItem tabItem = new MyTabItem();
                        tabItem.Header = header;
                        tabItem.Content = control;
                        tabItem.Foreground = Brushes.Blue;
                        TbcPrincipal.Items.Add(tabItem);
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
                throw new Exception("Erro inesperado: " + ex.Message);
            }
        }

        private void lblUsuarios_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UCUsuarios ucUsuario = MinhaNinject.Kernel.Get<UCUsuarios>();
                InitializeTab(ucUsuario, "Usuários");
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk(exception.Message);
            }
        }
    }
}
