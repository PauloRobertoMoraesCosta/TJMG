using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TJ.Apresentacao;
using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;

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
                dgvCumprimento.ItemsSource = serviceCumprimentoMes.RetornarPorSentenciado(sentenciadoSelecionado.Id).ToList();
            }
        }

        
        private void calcularDiferença(string horaMinutoInicio, string horaMinutoFim)
        {
            string horaAtual = horaMinutoInicio.Substring(0, horaMinutoInicio.IndexOf(":"));
            string minutoAtual = horaMinutoInicio.Substring(horaMinutoInicio.IndexOf(":") + 1, 2);
            string horaFim = horaMinutoFim.Substring(0, horaMinutoFim.IndexOf(":"));
            string minutoFim = horaMinutoFim.Substring(horaMinutoFim.IndexOf(":") + 1, 2);

            int inicio = ((Convert.ToInt32(horaAtual) * 60) + Convert.ToInt32(minutoAtual));
            int fim = ((Convert.ToInt32(horaFim) * 60) + Convert.ToInt32(minutoFim));
            string diferencaHoras = ((inicio - fim) / 60).ToString();
            string diferencaMinutos = ((inicio - fim) % 60).ToString();

            horasMinutos.Clear();
            horasMinutos.Add(diferencaHoras.Length > 1 ? diferencaHoras : "0" + diferencaHoras);
            horasMinutos.Add(diferencaMinutos.Length > 1 ? diferencaMinutos : "0" + diferencaMinutos);
        }

        private void somarHoras(string horaMinutoAtual, string horaMinutoSomar)
        {
            string horaAtual = horaMinutoAtual.Substring(0, horaMinutoAtual.IndexOf(":"));
            string minutoAtual = horaMinutoAtual.Substring(horaMinutoAtual.IndexOf(":") + 1, 2);
            string horaSomar = horaMinutoSomar.Substring(0, horaMinutoSomar.IndexOf(":"));
            string minutoSomar = horaMinutoSomar.Substring(horaMinutoSomar.IndexOf(":") + 1, 2);

            int parcelaInicio = ((Convert.ToInt32(horaAtual) * 60) + Convert.ToInt32(minutoAtual));
            int parcelaFim = ((Convert.ToInt32(horaSomar) * 60) + Convert.ToInt32(minutoSomar));
            string totalSomaEmHoras = ((parcelaInicio + parcelaFim) / 60).ToString();
            string totalSomaEmMinutos = ((parcelaInicio + parcelaFim) % 60).ToString();

            horasMinutos.Clear();
            horasMinutos.Add(totalSomaEmHoras.Length > 1 ? totalSomaEmHoras : "0" + totalSomaEmHoras);
            horasMinutos.Add(totalSomaEmMinutos.Length > 1 ? totalSomaEmMinutos : "0" + totalSomaEmMinutos);
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
            lblTotalEmDias.Content = ((sentenciado.PenaAnos * 365) + (sentenciado.PenaMeses * 30) + sentenciado.PenaDias).ToString();
            lblDetracao.Content = sentenciado.Detracao.ToString();
            lblDetracao.ToolTip = sentenciado.DetracaoObservacao;
            lblSoma.Content = sentenciado.SomaDePena.ToString();
            lblSoma.ToolTip = sentenciado.SomaDePenaObservacao;

            carregarDadosCumprimento();
        }

        private void carregarDadosCumprimento()
        {
            string horasCumpridas = "00:00";

            //IEnumerable<Cumprimento> cumprimentos = _serviceCumprimento.RetornaTodos();
            //if (cumprimentos.Any())
            //{
            //    _serviceCumprimento.Reload(cumprimentos);
            //    cumprimentos = _serviceCumprimento.RetornaTodos(); //RetornarPorSentenciado(sentenciadoSelecionado.Id).ToList();
            //}
            //foreach (Cumprimento registro in cumprimentos)
            //{
            //    calcularDiferença(registro.FimHH + ":" + registro.FimMM, registro.InicioHH + ":" + registro.InicioMM);
            //    somarHoras(horasCumpridas, horasMinutos[0] + ":" + horasMinutos[1]);
            //    horasCumpridas = horasMinutos[0] + ":" + horasMinutos[1];
            //}

            if (sentenciadoSelecionado.Detracao > 0)
            {
                somarHoras(horasCumpridas, sentenciadoSelecionado.Detracao > 1 ? sentenciadoSelecionado.Detracao + ":00" : "0" + sentenciadoSelecionado.Detracao + ":00");
                lblHorasCumpridas.Content = horasMinutos[0] + ":" + horasMinutos[1];
            }
            else
            {
                lblHorasCumpridas.Content = horasCumpridas;
            }

            if (sentenciadoSelecionado.SomaDePena > 0)
            {
                somarHoras(lblTotalEmDias.Content + ":00", sentenciadoSelecionado.SomaDePena + ":00");
                calcularDiferença(horasMinutos[0] + ":" + horasMinutos[1], lblHorasCumpridas.Content.ToString());
                lblHorasCumprir.Content = horasMinutos[0] + ":" + horasMinutos[1];
            }
            else
            {
                calcularDiferença(lblTotalEmDias.Content + ":00", lblHorasCumpridas.Content.ToString());
                lblHorasCumprir.Content = horasMinutos[0] + ":" + horasMinutos[1];
            }
        }

        private void carregarTelaCumprimento(Cumprimento cumprimento)
        {
            //dtpDataCumprimento.SelectedDate = cumprimento.Data;
            //tbxInicioHH.Text = cumprimento.InicioHH;
            //tbxInicioMM.Text = cumprimento.InicioMM;
            //tbxFimHH.Text = cumprimento.FimHH;
            //tbxFimMM.Text = cumprimento.FimMM;
            //cbxEntidadeLancamento.ItemsSource = sentenciadoSelecionado.SentenciadoEntidades.Where(s => s.DataFim == null);
            //cbxEntidadeLancamento.DisplayMemberPath = "Entidade.Nome";
            //cbxEntidadeLancamento.SelectedIndex = cbxEntidadeLancamento.Items.Count > 0 ? 0 : -1;
            //cbxEntidadeLancamento.IsEnabled = cbxEntidadeLancamento.Items.Count > 1 ? true : false;
        }

        private void imgPesquisarNome_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (cbxLocalizarNome.SelectedIndex == -1)
                    Mensagens.MensagemAlertaOk("Selecione algum nome de reeducando para carregar.");
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
                Mensagens.MensagemErroOk("Ocorreu um erro inesperado: " + exception.Message);
            }
        }

        private void imgPesquisarProcesso_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (cbxLocalizarProcesso.SelectedIndex == -1)
                    Mensagens.MensagemAlertaOk("Selecione algum processo de reeducando para carregar.");
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
                Mensagens.MensagemErroOk("Ocorreu um erro inesperado: " + exception.Message);
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
                    Mensagens.MensagemAlertaOk("Para editar algum registro, clique duas vezes no mesmo!");
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Algo aconteceu errado ao clicar duas vezes no Grid: " + exception.Message);
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
                            //_serviceCumprimento.Remover((Cumprimento)dgvCumprimento.SelectedItems[i]);
                        }
                        carregarDgvCumprimento();
                        btnNovoCumprimento.IsEnabled = true;
                        carregarDadosCumprimento();
                    }
                }
                else
                    Mensagens.MensagemAlertaOk("Para excluir cumprimentos selecione pelo menos um!");
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro inesperado ao excluir: " + exception.Message);
            }
        }

        private void btnGravarCumprimento_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (true)
                {
                    //if (!Validacoes.validarCampos(new List<Control>() { dtpDataCumprimento, tbxInicioHH, tbxInicioMM, tbxFimHH, tbxFimMM, cbxEntidadeLancamento }))
                    //    Mensagens.MensagemAlertaOk("Favor informar os dados obrigatórios.");
                    //else
                    //{
                    //    //_serviceCumprimento.Adiciona(popularCumprimento(new Cumprimento()));
                    //    carregarDgvCumprimento();
                    //    incluindo = false;
                    //    btnNovoCumprimento.IsEnabled = true;
                    //    carregarDadosCumprimento();
                    //}
                }
                else
                {
                    //if (!Validacoes.validarCampos(new List<Control>() { dtpDataCumprimento, tbxInicioHH, tbxInicioMM, tbxFimHH, tbxFimMM, cbxEntidadeLancamento }))
                    //    Mensagens.MensagemAlertaOk("Favor informar os dados obrigatórios.");
                    //else
                    //{
                    //    if (cumprimentoSelecionado != null)
                    //    {
                    //        //Cumprimento cumprimentoBanco = _serviceCumprimento.RetornaPorId(cumprimentoSelecionado.Id);
                    //        //_serviceCumprimento.Alterar(popularCumprimento(cumprimentoBanco));
                    //        carregarDgvCumprimento();
                    //        btnNovoCumprimento.IsEnabled = true;
                    //        cumprimentoSelecionado = null;
                    //        carregarDadosCumprimento();
                    //    }
                    //}
                }
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro ao gravar a entidade: " + exception.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                carregarDadosCumprimento();
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro inesperado: " + exception.Message);
            }
        }

        private void tbxInicioHH_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
                {
                    if ((sender as TextBox).Text != "")
                    {
                        (sender as TextBox).BorderBrush = new SolidColorBrush(Colors.Blue);
                        if (Convert.ToInt32((sender as TextBox).Text) > 23)
                        {
                            (sender as TextBox).Text = "0";
                            Mensagens.MensagemAlertaOk("Atenção: Os campos de horas não podem ser maiores que 23");
                        }
                        if ((sender as TextBox).Text.Length >= 2)
                        {
                            var ex = e.OriginalSource as UIElement;
                            ex.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                        }
                    }
                }
            }
            catch (Exception Exception)
            {
                Mensagens.MensagemErroOk("Erro inesperado: " + Exception.Message);
            }
        }

        private void tbxInicioHH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                //tbxInicioMM.Focus();

                if ((!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) && e.Key != Key.Tab))
                    e.Handled = true;
        }

        private void tbxInicioMM_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
                {
                    if ((sender as TextBox).Text != "")
                    {
                        (sender as TextBox).BorderBrush = new SolidColorBrush(Colors.Blue);
                        if (Convert.ToInt32((sender as TextBox).Text) > 59)
                        {
                            (sender as TextBox).Text = "0";
                            Mensagens.MensagemAlertaOk("Atenção: Os campos de minutos não podem ser maiores que 59");
                        }
                        if ((sender as TextBox).Text.Length >= 2)
                        {
                            var ex = e.OriginalSource as UIElement;
                            ex.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                        }
                    }
                }
            }
            catch (Exception Exception)
            {
                Mensagens.MensagemErroOk("Erro inesperado: " + Exception.Message);
            }
        }

        private void dtpDataCumprimento_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as DatePicker).SelectedDate != null)
                (sender as DatePicker).BorderBrush = new SolidColorBrush(Colors.Blue);
        }

        private void dtpDataCumprimento_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                //tbxInicioHH.Focus();
            }
            catch (Exception ex)
            {
                Mensagens.MensagemErroOk(ex.Message);
            }
        }

        private void cbxEntidadeLancamento_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Source as ComboBox).SelectedIndex != -1)
                (e.Source as ComboBox).BorderBrush = new SolidColorBrush(Colors.Blue);
        }

        private void cbxEntidadeLancamento_DropDownClosed(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex != -1)
                (sender as ComboBox).BorderBrush = new SolidColorBrush(Colors.Blue);
        }

        private void tbxInicioMM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                //tbxFimHH.Focus();
                if ((!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) && e.Key != Key.Tab))
                    e.Handled = true;
        }

        private void tbxFimHH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                //tbxFimMM.Focus();
                if ((!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) && e.Key != Key.Tab))
                    e.Handled = true;
        }

        private void tbxFimMM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                //cbxEntidadeLancamento.Focus();
                if ((!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) && e.Key != Key.Tab))
                    e.Handled = true;
        }
    }
}
