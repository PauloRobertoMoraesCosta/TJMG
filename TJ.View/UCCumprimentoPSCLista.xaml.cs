using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TJ.Apresentacao;
using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;
using static TJ.View.Enumeracoes;

namespace TJ.View
{
    /// <summary>
    /// Interaction logic for UCCumprimentoPSC.xaml
    /// </summary>
    public partial class UCCumprimentoPSCLista : UserControl
    {
        private Usuario usuarioLogado;
        private CumprimentoMes cumprimentoMesSelecionado = null;
        private Sentenciado sentenciadoSelecionado = null;
        private List<string> horasMinutos = new List<string>();

        public UCCumprimentoPSCLista(Usuario usuario)
        {
            usuarioLogado = usuario;
            InitializeComponent();
            carregarDadosIniciais();
        }

        private void carregarDadosIniciais()
        {
            using (IAppServiceSentenciado serviceSentenciado = MinhaNinject.Kernel.Get<IAppServiceSentenciado>())
            {
                cbxLocalizarNome.ItemsSource = serviceSentenciado.RetornaTodos().OrderBy(s => s.Nome);
                cbxLocalizarProcesso.ItemsSource = serviceSentenciado.RetornaTodos().OrderBy(s => s.Processo);
            }
            cbxLocalizarNome.DisplayMemberPath = "Nome";
            cbxLocalizarProcesso.DisplayMemberPath = "Processo";
            dplTelaDados.Height = 0;
        }

        public void carregarDgvCumprimento()
        {
            using (IAppServiceCumprimentoMes serviceCumprimentoMes = MinhaNinject.Kernel.Get<IAppServiceCumprimentoMes>())
            {
                dgvCumprimento.ItemsSource = serviceCumprimentoMes.RetornarPorSentenciado(sentenciadoSelecionado.Id).ToList().OrderBy(c => c.Ano).ThenBy(c => Enum.Parse(typeof(Meses), c.Mes));
            }
        }

        private void carregarTelaReeducando(Sentenciado sentenciado)
        {
            lblNome.Text = sentenciado.Nome;
            lblFiliacao.Text = sentenciado.Filiacao;
            lblOrigem.Text = sentenciado.Origem;
            lblSituacao.Text = sentenciado.StatusPena;
            lblDataEntrada.Text = sentenciado.DataCadastro.ToString();
            lblTelefone.Text = sentenciado.Telefone;
            lblObservacao.Text = sentenciado.Observacao;
            lblResponsavelSetor.Text = sentenciado.ResponsavelSetor;
            lblProcesso.Text = sentenciado.Processo;
            lblPenaAnos.Content = sentenciado.PenaAnos.ToString();
            lblPenaMeses.Content = sentenciado.PenaMeses.ToString();
            lblPenaDias.Content = sentenciado.PenaDias.ToString();
            lblTotalEmDias.Content = ((sentenciado.PenaAnos * 365) + (sentenciado.PenaMeses * 30) + sentenciado.PenaDias + sentenciado.SomaDePena - sentenciado.Detracao).ToString();
            lblDetracao.Content = sentenciado.Detracao.ToString();
            lblDetracao.ToolTip = sentenciado.DetracaoObservacao;
            lblSoma.Content = sentenciado.SomaDePena.ToString();
            lblSoma.ToolTip = sentenciado.SomaDePenaObservacao;

            carregarDadosCumprimento();
        }

        public void carregarDadosCumprimento()
        {
            carregarDgvCumprimento();
            TimeSpan horasTotaisCumpridas = new TimeSpan();
            for (int i = 0; i < dgvCumprimento.Items.Count; i++)
            {
                string[] listaHoras = (dgvCumprimento.Items[i] as CumprimentoMes).TempoCumprido.Split(':');
                horasTotaisCumpridas = horasTotaisCumpridas.Add(new TimeSpan(Convert.ToInt32(listaHoras[0]), Convert.ToInt32(listaHoras[1]), Convert.ToInt32(listaHoras[2])));
            }
            lblHorasCumpridas.Content = String.Format("{0}:{1}", (int)horasTotaisCumpridas.TotalHours > 9 ? ((int)horasTotaisCumpridas.TotalHours).ToString() : "0" + (int)horasTotaisCumpridas.TotalHours, horasTotaisCumpridas.Minutes > 9 ? horasTotaisCumpridas.Minutes.ToString() : "0" + horasTotaisCumpridas.Minutes);
            //lblTotalEmDias
            TimeSpan tempoCumprir = (new TimeSpan(Convert.ToInt32(lblTotalEmDias.Content),0,0)) - horasTotaisCumpridas;
            lblHorasCumprir.Content = String.Format("{0}:{1:mm}", (int)tempoCumprir.TotalHours > 9 ? ((int)tempoCumprir.TotalHours).ToString() : "0" + (int)tempoCumprir.TotalHours, tempoCumprir);
            
        }

        private void imgPesquisarNome_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (cbxLocalizarNome.SelectedIndex == -1)
                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("Selecione algum nome de reeducando para carregar.");
                else
                {
                    dplTelaDados.Height = double.NaN;
                    sentenciadoSelecionado = cbxLocalizarNome.SelectedItem as Sentenciado;
                    carregarTelaReeducando(sentenciadoSelecionado);
                    carregarDgvCumprimento();
                    cbxLocalizarProcesso.SelectedIndex = -1;
                }
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao carregar o(a) sentenciado(a): " + exception.Message);
            }
        }

        private void imgPesquisarProcesso_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (cbxLocalizarProcesso.SelectedIndex == -1)
                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("Selecione algum processo de reeducando para carregar.");
                else
                {
                    sentenciadoSelecionado = cbxLocalizarProcesso.SelectedItem as Sentenciado;
                    carregarTelaReeducando(sentenciadoSelecionado);
                    carregarDgvCumprimento();
                    cbxLocalizarNome.SelectedIndex = -1;
                }
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao carregar o(a) sentenciado(a): " + exception.Message);
            }
        }

        private void dgvCumprimento_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgvCumprimento.SelectedItems.Count == 1)
                {
                    winCadEdCumprimento winCadEdCumprimento = new winCadEdCumprimento(usuarioLogado, sentenciadoSelecionado, this, (CumprimentoMes)dgvCumprimento.SelectedItem);
                    winCadEdCumprimento.ShowDialog();
                }
                else
                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("Para editar algum registro, clique duas vezes no mesmo!");
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao clicar duas vezes no Grid: " + exception.Message);
            }
        }

        private void btnNovoCumprimento_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                winCadEdCumprimento winCadEdCumprimento = new winCadEdCumprimento(usuarioLogado, sentenciadoSelecionado, this);
                winCadEdCumprimento.ShowDialog();
            }
            catch (Exception ex)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao clicar em novo: " + ex.Message);
            }
        }


        private void btnExcluirCumprimento_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgvCumprimento.SelectedItems.Count > 0)
                {
                    if (Mensagens.MensagemConfirmOkCancel("Quer excluir o(s) cumprimento(s) selecionado(s) caso isso seja possível?") == MessageBoxResult.OK)
                    {
                        for (int i = 0; i < dgvCumprimento.SelectedItems.Count; i++)
                        {
                            using (IAppServiceCumprimentoMes serviceCumprimentoMes = MinhaNinject.Kernel.Get<IAppServiceCumprimentoMes>())
                            {
                                cumprimentoMesSelecionado = serviceCumprimentoMes.RetornaPorId((dgvCumprimento.SelectedItems[i] as CumprimentoMes).Id);
                            }
                            using (IAppServiceCumprimento serviceCumprimento = MinhaNinject.Kernel.Get<IAppServiceCumprimento>())
                            {
                                for (int n = 0; n < cumprimentoMesSelecionado.Cumprimentos.Count; n++)
                                {
                                    serviceCumprimento.Remover(serviceCumprimento.RetornaPorId(cumprimentoMesSelecionado.Cumprimentos.ElementAt(n).Id));
                                }
                            }
                            using (IAppServiceCumprimentoMes serviceCumprimentoMes = MinhaNinject.Kernel.Get<IAppServiceCumprimentoMes>())
                            {
                                cumprimentoMesSelecionado = serviceCumprimentoMes.RetornaPorId((dgvCumprimento.SelectedItems[i] as CumprimentoMes).Id);
                                serviceCumprimentoMes.Remover(cumprimentoMesSelecionado);
                            }
                            cumprimentoMesSelecionado = null;
                        }
                        carregarDadosCumprimento();
                        (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowSuccess("Cumprimento(s) excluido(s) com sucesso!");
                    }
                }
                else
                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("Para excluir cumprimentos selecione pelo menos um!");
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Erro inesperado ao excluir: " + exception.Message);
            }
        }

        private void dgvCumprimento_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!btnExcluirCumprimento.IsKeyboardFocused && !dgvCumprimento.IsKeyboardFocusWithin)
                {
                    dgvCumprimento.SelectedItem = null;
                }
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao perder o foco: " + exception.Message);
            }
        }
    }
}
