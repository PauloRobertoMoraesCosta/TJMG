using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TJ.Dominio.Entidades;

namespace TJ.View
{
    /// <summary>
    /// Interaction logic for wpfTelaPrincipal.xaml
    /// </summary>
    public partial class WpfTelaPrincipal : Window
    {
        protected readonly Usuario usuarioLogado ;
        public WpfTelaPrincipal()
        {
            InitializeComponent();
            
        }

        public WpfTelaPrincipal(Usuario usuario)
        {
            InitializeComponent();
            usuarioLogado = usuario;
            updateDadosTela();
            UCMovimentacoesLencoes ucMovimentacoesLencoes = new UCMovimentacoesLencoes();
            TabItem tabItemMovimentacoesLencoes = new TabItem();
            tabItemMovimentacoesLencoes.Header = "Movimentações Lenções";
            tabItemMovimentacoesLencoes.Content = ucMovimentacoesLencoes;
            TbcPrincipal.Items.Add(tabItemMovimentacoesLencoes);
        }

        public void updateDadosTela()
        {
            this.lblDataHora.Content = DateTime.Now.Date.ToString().Substring(0, 10);
        }
    }
}
