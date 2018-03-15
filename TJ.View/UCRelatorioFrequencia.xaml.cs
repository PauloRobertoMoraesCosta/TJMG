using System;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CrystalDecisions.CrystalReports.Engine;
using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;
using TJ.Apresentacao;
using Ninject;

namespace TJ.View
{
    /// <summary>
    /// Interaction logic for RelatorioEncaminhamento.xaml
    /// </summary>
    public partial class UCRelatorioFrequencia : UserControl
    {
        private Usuario usuarioLogado;
        ReportDocument relatorio = new ReportDocument();

        public UCRelatorioFrequencia(Usuario UsuarioLogado)
        {
            try
            {
                InitializeComponent();
                usuarioLogado = UsuarioLogado;
                using (IAppServiceSentenciado serviceSentenciado = MinhaNinject.Kernel.Get<IAppServiceSentenciado>())
                {
                    cbxSentenciado.ItemsSource = serviceSentenciado.RetornaTodos().OrderBy(s => s.Nome).ToList();
                }
                cbxSentenciado.DisplayMemberPath = "Nome";
                CrystalReportsViewer.Owner = Window.GetWindow(this);
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk(exception.Message);
            }

        }

        private void BtnImprimir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((cbxSentenciado.SelectedItem as Sentenciado).Origem != "")
                {
                    CrystalReportsViewer.Visibility = Visibility.Visible;

                    relatorio.Load(String.Format(@"{0}FolhaFrequencia.rpt", Validacoes.caminhoExe()));

                    string[] connectionString = ConfigurationManager.ConnectionStrings["ExecucoesPenais2"].ConnectionString.Split(';');
                    string servidor = connectionString[0].Split('=')[1];
                    string banco = connectionString[1].Split('=')[1];
                    string usuario = connectionString[3].Split('=')[1];
                    string senha = connectionString[4].Split('=')[1];

                    relatorio.DataSourceConnections[0].SetConnection(servidor, banco, usuario, senha);

                    relatorio.SetParameterValue("IdSentenciado", (cbxSentenciado.SelectedItem as Sentenciado).Id);
                    relatorio.SetParameterValue("Id_Instituicao", (cbxInstituicao.SelectedItem as SentenciadoEntidade).EntidadeId);

                    CrystalReportsViewer.Owner = Window.GetWindow(this);
                    CrystalReportsViewer.ViewerCore.ReportSource = relatorio;
                }
                else
                {
                    Mensagens.MensagemAlertaOk("O sentenciado não possui uma origem selecionada, favor corrigir no cadastro do sentenciado.");
                }
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Ocorreu umproblema - Ao imprimir o relatório: " + exception.Message);
            }
        }

        private void cbx_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if ((sender as ComboBox).SelectedIndex != -1)
                    (sender as ComboBox).BorderBrush = new SolidColorBrush(Colors.Blue);
            }
            catch (Exception ex)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("No relatório de frequência" + ex.Message);
            }

        }

        private void cbx_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if ((sender as ComboBox).SelectedIndex != -1)
                    (sender as ComboBox).BorderBrush = new SolidColorBrush(Colors.Blue);
            }
            catch (Exception ex)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("No relatório de frequência" + ex.Message);
            }
        }

        private void cbxSentenciado_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if ((sender as ComboBox).SelectedIndex != -1)
                {
                    (sender as ComboBox).BorderBrush = new SolidColorBrush(Colors.Blue);

                    using (IAppServiceSentenciadoEntidade serviceSentenciadoEntidade = MinhaNinject.Kernel.Get<IAppServiceSentenciadoEntidade>())
                    {
                        cbxInstituicao.ItemsSource = serviceSentenciadoEntidade.RetornarPorSentenciado((cbxSentenciado.SelectedItem as Sentenciado).Id).ToList();
                    }
                    cbxInstituicao.DisplayMemberPath = "Nome";
                }
            }
            catch (Exception ex)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("No relatório de frequência" + ex.Message);
            }
        }

        private void cbxSentenciado_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if ((sender as ComboBox).SelectedIndex != -1)
                {
                    (sender as ComboBox).BorderBrush = new SolidColorBrush(Colors.Blue);
                    using (IAppServiceSentenciadoEntidade serviceSentenciadoEntidade = MinhaNinject.Kernel.Get<IAppServiceSentenciadoEntidade>())
                    {
                        cbxInstituicao.ItemsSource = serviceSentenciadoEntidade.RetornarPorSentenciado((cbxSentenciado.SelectedItem as Sentenciado).Id).ToList().Where(se => se.DataFim == null);
                    }
                    cbxInstituicao.DisplayMemberPath = "Entidade.Nome";
                }
            }
            catch (Exception ex)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("No relatório de frequência" + ex.Message);
            }
        }
    }
}
