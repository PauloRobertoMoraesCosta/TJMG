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
using static TJ.View.Enumeracoes;

namespace TJ.View
{
    /// <summary>
    /// Interaction logic for RelatorioEncaminhamento.xaml
    /// </summary>
    public partial class UCRelatorioCumprimento : UserControl
    {
        ReportDocument relatorio = new ReportDocument();

        public UCRelatorioCumprimento()
        {
            try
            {
                InitializeComponent();
                using (IAppServiceSentenciado serviceSentenciado = MinhaNinject.Kernel.Get<IAppServiceSentenciado>())
                {
                    cbxSentenciado.ItemsSource = serviceSentenciado.RetornaTodos().OrderBy(s => s.Nome).ToList();
                }
                cbxSentenciado.DisplayMemberPath = "Nome";
                Array a = Enum.GetValues(typeof(Meses));
                for (int i = 0; i < a.Length; i++)
                {
                    cbxMes.Items.Add(a.GetValue(i).ToString());
                }
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
                CrystalReportsViewer.Visibility = Visibility.Visible;

                relatorio.Load(String.Format(@"{0}Cumprimento.rpt", Validacoes.caminhoExe()));

                string[] connectionString = ConfigurationManager.ConnectionStrings["ExecucoesPenais2"].ConnectionString.Split(';');
                string servidor = connectionString[0].Split('=')[1];
                string banco = connectionString[1].Split('=')[1];
                string usuario = connectionString[3].Split('=')[1];
                string senha = connectionString[4].Split('=')[1];

                relatorio.DataSourceConnections[0].SetConnection(servidor, banco, usuario, senha);

                relatorio.SetParameterValue("SentenciadoId", cbxSentenciado.SelectedItem != null ? (cbxSentenciado.SelectedItem as Sentenciado).Id : 0);
                relatorio.SetParameterValue("Ano", !String.IsNullOrEmpty(tbxAno.Text) ? Convert.ToInt32(tbxAno.Text) : 0);
                relatorio.SetParameterValue("Mes", cbxMes.SelectedItem != null ? String.Format("'{0}'", cbxMes.SelectedValue) : "null");

                CrystalReportsViewer.Owner = Window.GetWindow(this);
                CrystalReportsViewer.ViewerCore.ReportSource = relatorio;
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Ocorreu umproblema - Ao imprimir o relatório: " + exception.Message);
            }
        }

        private void tbxAno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) && e.Key != Key.Tab)
                    e.Handled = true;
            }
            catch (Exception Exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(Exception.Message);
            }
        }
    }
}
