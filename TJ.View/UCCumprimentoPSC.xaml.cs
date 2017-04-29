using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;

namespace TJ.View
{
    /// <summary>
    /// Interaction logic for UCCumprimentoPSC.xaml
    /// </summary>
    public partial class UCCumprimentoPSC : UserControl
    {
        protected readonly IAppServiceSentenciado _serviceSentenciado;
        protected readonly IAppServiceCumprimento _serviceCumprimento;
        protected readonly IAppServiceSentenciadoEntidade _serviceSentenciadoEntidade;
        private Usuario usuarioLogado;
        private bool incluindo;
        private Cumprimento cumprimentoSelecionado = null;
        private Sentenciado sentenciadoSelecionado = null;
        private List<string> horasMinutos = new List<string>();

        public UCCumprimentoPSC(IAppServiceSentenciado serviceSentenciado, IAppServiceCumprimento serviceCumprimento, IAppServiceSentenciadoEntidade serviceSentenciadoEntidade, Usuario usuario)
        {
            _serviceSentenciado = serviceSentenciado;
            _serviceCumprimento = serviceCumprimento;
            _serviceSentenciadoEntidade = serviceSentenciadoEntidade;
            usuarioLogado = usuario;
            InitializeComponent();
            carregarDadosIniciais();
            alterEnableForm(false, gridDadosReeducando);
            alterEnableForm(false, gridDadosCumprimento);
            alterEnableBotoes(false, gridDadosCumprimento);
        }

        private void carregarDadosIniciais()
        {
            cbxLocalizarNome.ItemsSource = _serviceSentenciado.RetornaTodos().OrderBy(s => s.Nome);
            cbxLocalizarNome.DisplayMemberPath = "Nome";
            cbxLocalizarProcesso.ItemsSource = _serviceSentenciado.RetornaTodos().OrderBy(s => s.Processo);
            cbxLocalizarProcesso.DisplayMemberPath = "Processo";
        }

        private void carregarDgvCumprimento()
        {
            dgvCumprimento.ItemsSource = null;
            dgvCumprimento.ItemsSource = _serviceCumprimento.RetornarPorSentenciado(sentenciadoSelecionado.Id).ToList();
        }

        private void alterEnableForm(bool status, Grid gridAlterar)
        {
            for (int i = 0; i < gridAlterar.Children.Count; i++)
            {
                if (gridAlterar.Children[i] is TextBox || gridAlterar.Children[i] is DatePicker || gridAlterar.Children[i] is ComboBox)
                    gridAlterar.Children[i].IsEnabled = status;
            }
        }

        private void alterEnableBotoes(bool status, Grid gridAlterar)
        {
            for (int i = 0; i < gridAlterar.Children.Count; i++)
            {
                if (gridAlterar.Children[i] is Button)
                    gridAlterar.Children[i].IsEnabled = status;
            }
        }

        private void limpaTexto(Grid gridLimpar)
        {
            for (int i = 0; i < gridLimpar.Children.Count; i++)
            {
                if (gridLimpar.Children[i] is TextBox)
                {
                    (gridLimpar.Children[i] as TextBox).Text = "";
                    (gridLimpar.Children[i] as TextBox).BorderBrush = new SolidColorBrush(Colors.Blue);
                }
                if (gridLimpar.Children[i] is DatePicker)
                {
                    (gridLimpar.Children[i] as DatePicker).SelectedDate = null;
                    (gridLimpar.Children[i] as DatePicker).BorderBrush = new SolidColorBrush(Colors.Blue);
                }
                if (gridLimpar.Children[i] is ComboBox)
                {
                    (gridLimpar.Children[i] as ComboBox).SelectedIndex = -1;
                    (gridLimpar.Children[i] as ComboBox).BorderBrush = new SolidColorBrush(Colors.Blue);
                }
            }
        }

        private Cumprimento popularCumprimento(Cumprimento cumprimento)
        {
            cumprimento.Data = dtpDataCumprimento.SelectedDate.Value;
            cumprimento.InicioHH = tbxInicioHH.Text;
            cumprimento.InicioMM = tbxInicioMM.Text;
            cumprimento.FimHH = tbxFimHH.Text;
            cumprimento.FimMM = tbxFimMM.Text;
            calcularDiferença(cumprimento.FimHH + ":" + cumprimento.FimMM, cumprimento.InicioHH + ":" + cumprimento.InicioMM);
            cumprimento.DiferencaHoras = horasMinutos[0] + ":" + horasMinutos[1];
            cumprimento.SentenciadoId = sentenciadoSelecionado.Id;
            cumprimento.EntidadeId = SentenciadoEntidade.EntidadeAtual(_serviceSentenciadoEntidade.RetornaTodosAsNoTracking()).EntidadeId;
            cumprimento.Usuario = usuarioLogado.Login;
            return cumprimento;
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
            tbxNome.Text = sentenciado.Nome;
            tbxFiliacao.Text = sentenciado.Filiacao;
            tbxOrigem.Text = sentenciado.Origem;
            tbxSituacao.Text = sentenciado.StatusPena;
            dtpDataEntrada.SelectedDate = sentenciado.DataCadastro;
            tbxTelefone.Text = sentenciado.Telefone;
            tbxObservacao.Text = sentenciado.Observacao;
            tbxResponsavelSetor.Text = sentenciado.ResponsavelSetor;
            tbxProcesso.Text = sentenciado.Processo;
            tbxPenaAnos.Text = sentenciado.PenaAnos.ToString();
            tbxPenaMeses.Text = sentenciado.PenaMeses.ToString();
            tbxPenaDias.Text = sentenciado.PenaDias.ToString();
            tbxTotalEmDias.Text = ((sentenciado.PenaAnos * 365) + (sentenciado.PenaMeses * 30) + sentenciado.PenaDias).ToString();
            tbxDetracao.Text = sentenciado.Detracao.ToString();
            tbxComutacao.Text = sentenciado.Comutacao.ToString();
            tbxDetracaoObservacao.Text = sentenciado.DetracaoObservacao;
            tbxComutacaoObservacao.Text = sentenciado.ComutacaoObservacao;
            carregarDadosCumprimento();
        }

        private void carregarDadosCumprimento()
        {
            string horasCumpridas = "00:00";

            IEnumerable<Cumprimento> cumprimentos = _serviceCumprimento.RetornarPorSentenciado(sentenciadoSelecionado.Id).ToList();
            if (cumprimentos.Any())
            {
                _serviceCumprimento.Reload(cumprimentos);
                cumprimentos = _serviceCumprimento.RetornarPorSentenciado(sentenciadoSelecionado.Id).ToList();
            }
            foreach (Cumprimento registro in cumprimentos)
            {
                calcularDiferença(registro.FimHH + ":" + registro.FimMM, registro.InicioHH + ":" + registro.InicioMM);
                somarHoras(horasCumpridas, horasMinutos[0] + ":" + horasMinutos[1]);
                horasCumpridas = horasMinutos[0] + ":" + horasMinutos[1];
            }

            if (Convert.ToInt32(tbxDetracao.Text) > 0)
            {
                somarHoras(horasCumpridas, tbxDetracao.Text.Length > 1 ? tbxDetracao.Text + ":00" : "0" + tbxDetracao.Text + ":00");
                lblHorasCumpridas.Content = horasMinutos[0] + ":" + horasMinutos[1];
            }
            else
            {
                lblHorasCumpridas.Content = horasCumpridas;
            }

            if (Convert.ToInt32(tbxComutacao.Text) > 0)
            {
                somarHoras(tbxTotalEmDias.Text + ":00", tbxComutacao.Text + ":00");
                calcularDiferença(horasMinutos[0] + ":" + horasMinutos[1], lblHorasCumpridas.Content.ToString());
                lblHorasCumprir.Content = horasMinutos[0] + ":" + horasMinutos[1];
            }
            else
            {
                calcularDiferença(tbxTotalEmDias.Text + ":00", lblHorasCumpridas.Content.ToString());
                lblHorasCumprir.Content = horasMinutos[0] + ":" + horasMinutos[1];
            }
        }

        private void carregarTelaCumprimento(Cumprimento cumprimento)
        {
            dtpDataCumprimento.SelectedDate = cumprimento.Data;
            tbxInicioHH.Text = cumprimento.InicioHH;
            tbxInicioMM.Text = cumprimento.InicioMM;
            tbxFimHH.Text = cumprimento.FimHH;
            tbxFimMM.Text = cumprimento.FimMM;
            cbxEntidadeLancamento.ItemsSource =
                sentenciadoSelecionado.SentenciadoEntidades.Where(s => s.DataFim == null);
            cbxEntidadeLancamento.DisplayMemberPath = "Entidade.Nome";
            cbxEntidadeLancamento.SelectedIndex = cbxEntidadeLancamento.Items.Count > 0 ? 0 : -1;
            cbxEntidadeLancamento.IsEnabled = cbxEntidadeLancamento.Items.Count > 1 ? true : false;
        }

        private void imgPesquisarNome_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (cbxLocalizarNome.SelectedIndex == -1)
                    Mensagens.MensagemAlertaOk("Selecione algum nome de reeducando para carregar.");
                else
                {
                    sentenciadoSelecionado = cbxLocalizarNome.SelectedItem as Sentenciado;
                    carregarTelaReeducando(sentenciadoSelecionado);
                    carregarDgvCumprimento();
                    alterEnableBotoes(true, gridBotoesCumprimento);
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
                    alterEnableBotoes(true, gridBotoesCumprimento);
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
                    cumprimentoSelecionado = (Cumprimento)dgvCumprimento.SelectedItem;
                    carregarTelaCumprimento(cumprimentoSelecionado);
                    alterEnableForm(true, gridDadosCumprimento);
                    alterEnableBotoes(true, gridBotoesCumprimento);
                    btnNovoCumprimento.IsEnabled = false;
                    incluindo = false;
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
            incluindo = true;
            limpaTexto(gridDadosCumprimento);
            cbxEntidadeLancamento.ItemsSource = sentenciadoSelecionado.SentenciadoEntidades.Where(se => se.DataFim == null);
            cbxEntidadeLancamento.DisplayMemberPath = "Entidade.Nome";
            cbxEntidadeLancamento.SelectedIndex = 0;
            alterEnableForm(true, gridDadosCumprimento);
            btnNovoCumprimento.IsEnabled = false;
            dtpDataCumprimento.Focus();
        }

        private void btnCancelarCumprimento_Click(object sender, RoutedEventArgs e)
        {
            limpaTexto(gridDadosCumprimento);
            alterEnableForm(false, gridDadosCumprimento);
            btnNovoCumprimento.IsEnabled = true;
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
                            _serviceCumprimento.Remover((Cumprimento)dgvCumprimento.SelectedItems[i]);
                        }
                        carregarDgvCumprimento();
                        btnNovoCumprimento.IsEnabled = true;
                        limpaTexto(gridDadosCumprimento);
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
                if (incluindo)
                {
                    if (!Validacoes.validarCampos(new List<Control>() { dtpDataCumprimento, tbxInicioHH, tbxInicioMM, tbxFimHH, tbxFimMM, cbxEntidadeLancamento }))
                        Mensagens.MensagemAlertaOk("Favor informar os dados obrigatórios.");
                    else
                    {
                        _serviceCumprimento.Adiciona(popularCumprimento(new Cumprimento()));
                        if (!(sentenciadoSelecionado.Detracao.ToString().Equals(tbxDetracao.Text))
                            || !(sentenciadoSelecionado.Comutacao.ToString().Equals(tbxComutacao.Text))
                            || !(sentenciadoSelecionado.DetracaoObservacao.Equals(tbxDetracaoObservacao.Text))
                            || !(sentenciadoSelecionado.ComutacaoObservacao.Equals(tbxComutacaoObservacao.Text)))
                        {
                            sentenciadoSelecionado.Detracao = Convert.ToInt16(tbxDetracao.Text);
                            sentenciadoSelecionado.Comutacao = Convert.ToInt16(tbxComutacao.Text);
                            sentenciadoSelecionado.DetracaoObservacao = tbxDetracaoObservacao.Text;
                            sentenciadoSelecionado.ComutacaoObservacao = tbxComutacaoObservacao.Text;
                            _serviceSentenciado.Alterar(sentenciadoSelecionado);
                        }
                        carregarDgvCumprimento();
                        incluindo = false;
                        alterEnableForm(false, gridDadosCumprimento);
                        btnNovoCumprimento.IsEnabled = true;
                        limpaTexto(gridDadosCumprimento);
                        carregarDadosCumprimento();
                    }
                }
                else
                {
                    if (!Validacoes.validarCampos(new List<Control>() { dtpDataCumprimento, tbxInicioHH, tbxInicioMM, tbxFimHH, tbxFimMM, cbxEntidadeLancamento }))
                        Mensagens.MensagemAlertaOk("Favor informar os dados obrigatórios.");
                    else
                    {
                        if (cumprimentoSelecionado != null)
                        {
                            if (!(sentenciadoSelecionado.Detracao.ToString().Equals(tbxDetracao.Text))
                                || !(sentenciadoSelecionado.Comutacao.ToString().Equals(tbxComutacao.Text))
                                || !(sentenciadoSelecionado.DetracaoObservacao.Equals(tbxDetracaoObservacao.Text))
                                || !(sentenciadoSelecionado.ComutacaoObservacao.Equals(tbxComutacaoObservacao.Text)))
                            {
                                sentenciadoSelecionado.Detracao = Convert.ToInt16(tbxDetracao.Text);
                                sentenciadoSelecionado.Comutacao = Convert.ToInt16(tbxComutacao.Text);
                                sentenciadoSelecionado.DetracaoObservacao = tbxDetracaoObservacao.Text;
                                sentenciadoSelecionado.ComutacaoObservacao = tbxComutacaoObservacao.Text;
                                _serviceSentenciado.Alterar(sentenciadoSelecionado);
                            }
                            Cumprimento cumprimentoBanco = _serviceCumprimento.RetornaPorId(cumprimentoSelecionado.Id);
                            _serviceCumprimento.Alterar(popularCumprimento(cumprimentoBanco));
                            carregarDgvCumprimento();
                            alterEnableForm(false, gridDadosCumprimento);
                            limpaTexto(gridDadosCumprimento);
                            btnNovoCumprimento.IsEnabled = true;
                            cumprimentoSelecionado = null;
                            carregarDadosCumprimento();
                        }
                        else
                        {
                            if (!(sentenciadoSelecionado.Detracao.ToString().Equals(tbxDetracao.Text))
                                || !(sentenciadoSelecionado.Comutacao.ToString().Equals(tbxComutacao.Text))
                                || !(sentenciadoSelecionado.DetracaoObservacao.Equals(tbxDetracaoObservacao.Text))
                                || !(sentenciadoSelecionado.ComutacaoObservacao.Equals(tbxComutacaoObservacao.Text)))
                            {
                                sentenciadoSelecionado.Detracao = Convert.ToInt16(tbxDetracao.Text);
                                sentenciadoSelecionado.Comutacao = Convert.ToInt16(tbxComutacao.Text);
                                sentenciadoSelecionado.DetracaoObservacao = tbxDetracaoObservacao.Text;
                                sentenciadoSelecionado.ComutacaoObservacao = tbxComutacaoObservacao.Text;
                                _serviceSentenciado.Alterar(sentenciadoSelecionado);
                            }
                        }
                    }
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
                if ((sender as TextBox).Text != "")
                {
                    (sender as TextBox).BorderBrush = new SolidColorBrush(Colors.Blue);
                    if (Convert.ToInt32((sender as TextBox).Text) > 23)
                    {
                        (sender as TextBox).Text = "0";
                        Mensagens.MensagemAlertaOk("Atenção: Os campos de horas não podem ser maiores que 23");
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
                tbxInicioMM.Focus();
            if ((!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) && e.Key != Key.Tab))
                e.Handled = true;
        }

        private void tbxInicioMM_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if ((sender as TextBox).Text != "")
                {
                    (sender as TextBox).BorderBrush = new SolidColorBrush(Colors.Blue);
                    if (Convert.ToInt32((sender as TextBox).Text) > 59)
                    {
                        (sender as TextBox).Text = "0";
                        Mensagens.MensagemAlertaOk("Atenção: Os campos de minutos não podem ser maiores que 59");
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
                tbxFimHH.Focus();
            if ((!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) && e.Key != Key.Tab))
                e.Handled = true;
        }

        private void tbxFimHH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                tbxFimMM.Focus();
            if ((!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) && e.Key != Key.Tab))
                e.Handled = true;
        }

        private void tbxFimMM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                cbxEntidadeLancamento.Focus();
            if ((!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) && e.Key != Key.Tab))
                e.Handled = true;
        }

        private void tbxComutacao_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbxComutacao.Text != sentenciadoSelecionado.Comutacao.ToString())
            {
                sentenciadoSelecionado.Comutacao = Convert.ToInt32(tbxComutacao.Text);
                _serviceSentenciado.Alterar(sentenciadoSelecionado);
            }
        }

        private void tbxDetracao_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbxDetracao.Text != sentenciadoSelecionado.Detracao.ToString())
            {
                sentenciadoSelecionado.Detracao = Convert.ToInt32(tbxDetracao.Text);
                _serviceSentenciado.Alterar(sentenciadoSelecionado);
            }
        }

        private void tbxComutacaoObservacao_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbxComutacaoObservacao.Text != sentenciadoSelecionado.ComutacaoObservacao)
            {
                sentenciadoSelecionado.ComutacaoObservacao = tbxComutacaoObservacao.Text;
                _serviceSentenciado.Alterar(sentenciadoSelecionado);
            }
        }

        private void tbxDetracaoObservacao_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbxComutacaoObservacao.Text != sentenciadoSelecionado.ComutacaoObservacao)
            {
                sentenciadoSelecionado.ComutacaoObservacao = tbxComutacaoObservacao.Text;
                _serviceSentenciado.Alterar(sentenciadoSelecionado);
            }
        }
    }
}
