using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;

namespace TJ.View
{
    /// <summary>
    /// Interaction logic for UCReeducando.xaml
    /// </summary>
    public partial class UCReeducando : UserControl
    {
        protected readonly IAppServiceBairro _serviceBairro;
        protected readonly IAppServiceCidade _serviceCidade;
        protected readonly IAppServiceSentenciado _serviceSentenciado;
        protected readonly IAppServiceEntidade _serviceEntidade;
        protected readonly IAppServiceSentenciadoEntidade _serviceCumprimento;
        protected readonly IAppServiceUsuario _serviceUsuario;
        private Usuario usuarioLogado;
        private bool incluindo;
        public Sentenciado sentenciadoSelecionado;
        public SentenciadoEntidade movimentacaoSelecionada;

        public UCReeducando(IAppServiceBairro serviceBairro, IAppServiceCidade serviceCidade, IAppServiceSentenciado serviceSentenciado, IAppServiceSentenciadoEntidade serviceCumprimento, IAppServiceEntidade serviceEntidade, IAppServiceUsuario serviceUsuario, Usuario usuario)
        {
            try
            {
                _serviceBairro = serviceBairro;
                _serviceCidade = serviceCidade;
                _serviceSentenciado = serviceSentenciado;
                _serviceCumprimento = serviceCumprimento;
                _serviceEntidade = serviceEntidade;
                _serviceUsuario = serviceUsuario;
                InitializeComponent();
                this.Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
                usuarioLogado = usuario;
                carregaDadosIniciais();
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro inesperado: " + exception.Message);
            }
        }

        private void carregaDadosIniciais()
        {
            cbxBairro.ItemsSource = _serviceBairro.RetornaTodosAsNoTracking().OrderBy(b => b.Nome);
            cbxBairro.DisplayMemberPath = "Nome";
            cbxCidade.ItemsSource = _serviceCidade.RetornaTodosAsNoTracking().OrderBy(c => c.Nome);
            cbxCidade.DisplayMemberPath = "Nome";
            cbxEntidade.ItemsSource = _serviceEntidade.RetornaTodosAsNoTracking().OrderBy(e => e.Nome);
            cbxEntidade.DisplayMemberPath = "Nome";
            cbxResponsavel.ItemsSource = _serviceUsuario.RetornaTodosAsNoTracking().Where(u => u.Super.Equals("True")).OrderBy(e => e.Nome);
            cbxResponsavel.DisplayMemberPath = "Nome";
            cbxSexo.ItemsSource = new List<string>
            {
                "Masculino",
                "Feminino"
            };

            cbxOrigem.ItemsSource = new List<string>
            {
                "VEP", 
                "1ª Crim", 
                "2ª Crim", 
                "3ª Crim", 
                "1ª U.J. – 1° J.D", 
                "1ª U.J. – 2° J.D", 
                "1ª U.J. – 3° J.D"
            };

            cbxSituacaoPena.ItemsSource = new List<string>
            {
                "Cumprida",
                "Suspensa",
                "Convertida PPE",
                "Convertida PPL",
                "Convertida Tratamento",
                "Ativa",
                "Indultada",
                "Transferida Outra Comarca",
                "Outra"
            };
            cbxEstadoCivil.ItemsSource = new List<string>
            {
                "Solteiro(a)",
                "Casado(a)",
                "Divorciado(a)",
                "Amasiado(a)",
                "Viúvo(a)",
                "Separado(a) de Corpos",
                "Outro (a)"
            };
            cbxEscolaridade.ItemsSource = new List<string>
            {
                "Analfabeto",
                "Analfabeto Funcional",
                "Ensino Fundamental Incompleto",
                "Ensino Fundamental Completo",
                "Ensino Médio Incompleto",
                "Ensino Médio Completo",
                "Ensino Superior Incompleto",
                "Ensino Superior Completo",
                "Pós-Graduação Incompleta",
                "Pós-Graduação Completa"
            };
            cbxSituacaoComprimento.ItemsSource = new List<string>
            {
                "Regular (Mínimo 24 horas mês)",
                "Irregular (Entre 1 e 23 horas mês)",
                "Descumprimento"
            };
            carregarDgvReeducando();
            alterEnableForm(false, gridDadosSentenciado);
            alterEnableForm(false, gridDadosPena);
            alterEnableForm(false, gridDadosHistorico);
            alterEnableBotoes(false, gridBotoesHistorico);
        }

        private void limpaTexto(Grid grid)
        {
            for (int i = 0; i < grid.Children.Count; i++)
            {
                if (grid.Children[i] is TextBox)
                {
                    (grid.Children[i] as TextBox).Text = "";
                    (grid.Children[i] as TextBox).BorderBrush = new SolidColorBrush(Colors.Blue);
                }
                if (grid.Children[i] is DatePicker)
                {
                    (grid.Children[i] as DatePicker).SelectedDate = null;
                    (grid.Children[i] as DatePicker).BorderBrush = new SolidColorBrush(Colors.Blue);
                }
                if (grid.Children[i] is ComboBox)
                {
                    (grid.Children[i] as ComboBox).SelectedIndex = -1;
                    (grid.Children[i] as ComboBox).BorderBrush = new SolidColorBrush(Colors.Blue);
                }
            }
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

        private void carregarDgvReeducando()
        {
            dgvReeducando.ItemsSource = _serviceSentenciado.RetornaTodos().OrderBy(r => r.Nome);
        }

        private void carregarDgvHistorico(int sentenciadoId)
        {
            dgvHistorico.ItemsSource = _serviceCumprimento.RetornarPorSentenciado(sentenciadoId).OrderBy(mse => mse.DataFim);
        }

        private Sentenciado popularSentenciado(Sentenciado sentenciado)
        {
            sentenciado.AtividadePSC = tbxAtividadePSC.Text;
            sentenciado.BairroId = ((Bairro)(cbxBairro.SelectedItem)).Id;
            sentenciado.CidadeId = ((Cidade)(cbxCidade.SelectedItem)).Id;
            sentenciado.DataNascimento = dtpDataNascimento.SelectedDate.Value;
            sentenciado.Endereco = tbxEndereco.Text;
            sentenciado.Escolaridade = cbxEscolaridade.Text;
            sentenciado.EstadoCivil = cbxEstadoCivil.Text;
            sentenciado.Filiacao = tbxFiliacao.Text;
            sentenciado.Naturalidade = tbxNaturalidade.Text;
            sentenciado.Nome = tbxNome.Text;
            sentenciado.Observacao = tbxObservacao.Text;
            sentenciado.OcupacaoExperiencia = tbxOcupacaoExperiencia.Text;
            sentenciado.Origem = cbxOrigem.Text;
            sentenciado.PenaAnos = tbxPenaAnos.Text != "" ? Convert.ToInt32(tbxPenaAnos.Text) : 0;
            sentenciado.PenaMeses = tbxPenaMeses.Text != "" ? Convert.ToInt32(tbxPenaMeses.Text) : 0;
            sentenciado.PenaDias = Convert.ToInt16(tbxPenaDias.Text);
            sentenciado.StatusPena = cbxSituacaoPena.Text;
            sentenciado.PontoReferencia = tbxPontoReferência.Text;
            sentenciado.Processo = tbxProcesso.Text;
            sentenciado.ResponsavelSetor = (cbxResponsavel.SelectedItem as Usuario).Login;
            sentenciado.Sexo = cbxSexo.Text;
            sentenciado.Telefone = tbxTelefone.Text;
            if (incluindo)
                sentenciado.UsuarioCadastroLogin = usuarioLogado.Login;
            else
                sentenciado.UsuarioAlteracaoLogin = usuarioLogado.Login;
            return sentenciado;
        }

        private SentenciadoEntidade popularCumprimento(SentenciadoEntidade cumprimento)
        {
            cumprimento.DataFim = null;
            if (dtpDataFinal.SelectedDate != null)
                cumprimento.DataFim = dtpDataFinal.SelectedDate.Value;

            cumprimento.DataInicio = dtpDataInicial.SelectedDate.Value;
            cumprimento.EntidadeId = ((Entidade)cbxEntidade.SelectedItem).Id;
            cumprimento.SituacaoCumprimento = cbxSituacaoComprimento.Text;
            cumprimento.HorarioDeCumprimento = tbxSentenciadoDiasHorarios.Text;
            cumprimento.SentenciadoId = sentenciadoSelecionado.Id;
            return cumprimento;
        }

        private void popularTelaSentenciado(Sentenciado sentenciado)
        {
            for (int i = 0; i < cbxBairro.Items.Count; i++)
            {
                if ((cbxBairro.Items[i] as Bairro).Id == sentenciado.BairroId)
                {
                    cbxBairro.SelectedItem = cbxBairro.Items[i];
                    i = cbxBairro.Items.Count;
                }
            }
            for (int i = 0; i < cbxCidade.Items.Count; i++)
            {
                if ((cbxCidade.Items[i] as Cidade).Id == sentenciado.CidadeId)
                {
                    cbxCidade.SelectedItem = cbxCidade.Items[i];
                    i = cbxCidade.Items.Count;
                }
            }
            for (int i = 0; i < cbxResponsavel.Items.Count; i++)
            {
                if ((cbxResponsavel.Items[i] as Usuario).Login == sentenciado.ResponsavelSetor)
                {
                    cbxResponsavel.SelectedItem = cbxResponsavel.Items[i];
                    i = cbxResponsavel.Items.Count;
                }
            }
            cbxSexo.SelectedItem = sentenciado.Sexo;
            cbxSituacaoPena.SelectedItem = sentenciado.StatusPena;
            cbxEscolaridade.SelectedItem = sentenciado.Escolaridade;
            cbxEstadoCivil.SelectedItem = sentenciado.EstadoCivil;
            cbxOrigem.SelectedItem = sentenciado.Origem;
            tbxAtividadePSC.Text = sentenciado.AtividadePSC;
            tbxEndereco.Text = sentenciado.Endereco;
            tbxFiliacao.Text = sentenciado.Filiacao;
            tbxNaturalidade.Text = sentenciado.Naturalidade;
            tbxNome.Text = sentenciado.Nome;
            tbxObservacao.Text = sentenciado.Observacao;
            tbxOcupacaoExperiencia.Text = sentenciado.OcupacaoExperiencia;
            tbxPenaAnos.Text = sentenciado.PenaAnos.ToString();
            tbxPenaMeses.Text = sentenciado.PenaMeses.ToString();
            tbxPenaDias.Text = sentenciado.PenaDias.ToString();
            tbxPontoReferência.Text = sentenciado.PontoReferencia;
            tbxProcesso.Text = sentenciado.Processo;
            tbxTelefone.Text = sentenciado.Telefone;
            dtpDataNascimento.SelectedDate = sentenciado.DataNascimento.Date;
            tbxUsuarioInclusao.Text = sentenciado.UsuarioCadastroLogin;
            tbxUsuarioEdicao.Text = sentenciado.UsuarioAlteracaoLogin;
        }

        private void popularTelaHistorico(SentenciadoEntidade cumprimento)
        {
            for (int i = 0; i < cbxEntidade.Items.Count; i++)
            {
                if ((cbxEntidade.Items[i] as Entidade).Id == cumprimento.EntidadeId)
                {
                    cbxEntidade.SelectedItem = cbxEntidade.Items[i];
                    i = cbxEntidade.Items.Count;
                }
            }
            dtpDataInicial.SelectedDate = cumprimento.DataInicio;
            dtpDataFinal.SelectedDate = cumprimento.DataFim;
            cbxSituacaoComprimento.SelectedItem = cumprimento.SituacaoCumprimento;
            tbxSentenciadoDiasHorarios.Text = cumprimento.HorarioDeCumprimento;
        }


        private void btnGravar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (incluindo)
                {
                    if (
                        !Validacoes.validarCampos(new List<Control>()
                        {
                            tbxNome,
                            cbxSexo,
                            tbxProcesso,
                            tbxPenaDias,
                            dtpDataNascimento,
                            cbxEstadoCivil,
                            tbxEndereco,
                            cbxBairro,
                            cbxCidade,
                            cbxEscolaridade,
                            cbxResponsavel,
                            cbxOrigem,
                            tbxAtividadePSC
                        }))
                        Mensagens.MensagemAlertaOk("Favor informar os dados obrigatórios.");
                    else
                    {
                        _serviceSentenciado.Adiciona(popularSentenciado(new Sentenciado()));
                        carregarDgvReeducando();
                        incluindo = false;
                        alterEnableForm(false, gridDadosSentenciado);
                        alterEnableForm(false, gridDadosPena);
                        alterEnableForm(false, gridDadosHistorico);
                        alterEnableForm(false, gridBotoesHistorico);
                        btnNovo.IsEnabled = true;
                        limpaTexto(gridDadosSentenciado);
                        limpaTexto(gridDadosPena);
                        dgvHistorico.ItemsSource = null;
                    }
                }
                else
                {
                    if (
                        !Validacoes.validarCampos(new List<Control>()
                        {
                            tbxNome,
                            cbxSexo,
                            tbxProcesso,
                            tbxPenaDias,
                            dtpDataNascimento,
                            cbxEstadoCivil,
                            tbxEndereco,
                            cbxBairro,
                            cbxCidade,
                            cbxEscolaridade,
                            cbxResponsavel,
                            cbxOrigem,
                            tbxAtividadePSC
                        }))
                        Mensagens.MensagemAlertaOk("Favor informar os dados obrigatórios.");
                    else
                    {
                        Sentenciado sentenciadoBanco = _serviceSentenciado.RetornaPorId(sentenciadoSelecionado.Id);
                        _serviceSentenciado.Alterar(popularSentenciado(sentenciadoBanco));
                        carregarDgvReeducando();
                        alterEnableForm(false, gridDadosSentenciado);
                        alterEnableForm(false, gridDadosPena);
                        alterEnableForm(false, gridDadosHistorico);
                        alterEnableForm(false, gridBotoesHistorico);
                        limpaTexto(gridDadosSentenciado);
                        limpaTexto(gridDadosPena);
                        btnNovo.IsEnabled = true;
                        dgvHistorico.ItemsSource = null;
                    }
                }
            }
            catch (DbEntityValidationException dbEntityValidationException)
            {
                StreamWriter sw =
                        new StreamWriter(Validacoes.caminhoExe() + "log " + DateTime.Now.ToString("ddMMyyyyhhmm") + ".txt");
                foreach (var eve in dbEntityValidationException.EntityValidationErrors)
                {
                    sw.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        sw.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                sw.Close();
                throw;
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk(exception.Message);
            }
        }

        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            incluindo = true;
            limpaTexto(gridDadosSentenciado);
            alterEnableForm(true, gridDadosSentenciado);
            alterEnableForm(true, gridDadosPena);
            btnNovo.IsEnabled = false;
            tbxNome.Focus();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            limpaTexto(gridDadosSentenciado);
            limpaTexto(gridDadosPena);
            alterEnableForm(false, gridDadosSentenciado);
            alterEnableForm(false, gridDadosPena);
            alterEnableBotoes(false, gridBotoesHistorico);
            alterEnableForm(false, gridDadosHistorico);
            dgvHistorico.ItemsSource = null;
            btnNovo.IsEnabled = true;

        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgvReeducando.SelectedItems.Count > 0)
                {
                    if (Mensagens.MensagemConfirmOkCancel("Quer excluir o(s) reeducando(s) selecionado(s) caso isso seja possível?") == MessageBoxResult.OK)
                    {
                        for (int i = 0; i < dgvReeducando.SelectedItems.Count; i++)
                        {
                            _serviceSentenciado.Remover((Sentenciado)dgvReeducando.SelectedItems[i]);
                        }
                        carregarDgvReeducando();
                        btnNovo.IsEnabled = true;
                        limpaTexto(gridDadosSentenciado);
                    }
                }
                else
                    Mensagens.MensagemAlertaOk("Para excluir reeducando selecione pelo menos um!");
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro inesperado ao excluir: " + exception.Message);
            }
        }

        private void dgvReeducando_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgvReeducando.SelectedItems.Count == 1)
                {
                    sentenciadoSelecionado = (Sentenciado)dgvReeducando.SelectedItem;
                    popularTelaSentenciado(sentenciadoSelecionado);
                    carregarDgvHistorico(sentenciadoSelecionado.Id);
                    alterEnableForm(true, gridDadosSentenciado);
                    alterEnableForm(true, gridDadosPena);
                    alterEnableBotoes(true, gridBotoesHistorico);
                    btnNovo.IsEnabled = false;
                    incluindo = false;
                }
                else
                    Mensagens.MensagemAlertaOk("Para editar algum sentenciado, clique duas vezes no mesmo!");
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Algo aconteceu errado ao clicar duas vezes no Grid: " + exception.Message);
            }
        }

        private void btnNovoHistorico_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnNovoHistorico.IsEnabled = false;
                alterEnableForm(true, gridDadosHistorico);
                incluindo = true;
                cbxEntidade.Focus();
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro inesperado: " + exception.Message);
            }
        }

        private void btnCancelarHistorico_Click(object sender, RoutedEventArgs e)
        {
            btnNovoHistorico.IsEnabled = true;
            alterEnableForm(false, gridDadosHistorico);
            limpaTexto(gridDadosHistorico);
        }

        private void dgvHistorico_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgvReeducando.SelectedItems.Count == 1)
                {
                    movimentacaoSelecionada = (SentenciadoEntidade)dgvHistorico.SelectedItem;
                    popularTelaHistorico(movimentacaoSelecionada);
                    carregarDgvHistorico(sentenciadoSelecionado.Id);
                    alterEnableForm(true, gridDadosHistorico);
                    alterEnableBotoes(true, gridBotoesHistorico);
                    btnNovoHistorico.IsEnabled = false;
                    incluindo = false;
                }
                else
                    Mensagens.MensagemAlertaOk("Para editar algum sentenciado, clique duas vezes no mesmo!");
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro inesperado ao dar duplo clique no grid de históricos: " + exception.Message);
            }
        }

        private void btnExcluirHistorico_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgvHistorico.SelectedItems.Count > 0)
                {
                    if (Mensagens.MensagemConfirmOkCancel("Quer excluir o(s) históricos(s) selecionado(s) caso isso seja possível?") == MessageBoxResult.OK)
                    {
                        for (int i = 0; i < dgvHistorico.SelectedItems.Count; i++)
                        {
                            _serviceCumprimento.Remover((SentenciadoEntidade)dgvHistorico.SelectedItems[i]);
                        }
                        carregarDgvHistorico(sentenciadoSelecionado.Id);
                        btnNovoHistorico.IsEnabled = true;
                        limpaTexto(gridDadosHistorico);
                    }
                }
                else
                    Mensagens.MensagemAlertaOk("Para excluir histórico selecione pelo menos um!");
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro inesperado ao excluir histórico: " + exception.Message);
            }
        }

        private void btnGravarHistorico_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (incluindo)
                {
                    if (!Validacoes.validarCampos(new List<Control>()
                    {
                        cbxEntidade,
                        dtpDataInicial,
                        cbxSituacaoComprimento
                    }))
                        Mensagens.MensagemAlertaOk("Favor informar os dados obrigatórios.");
                    else
                    {
                        _serviceCumprimento.Adiciona(popularCumprimento(new SentenciadoEntidade()));
                        carregarDgvHistorico(sentenciadoSelecionado.Id);
                        incluindo = false;
                        alterEnableForm(false, gridDadosHistorico);
                        btnNovoHistorico.IsEnabled = true;
                        limpaTexto(gridDadosHistorico);
                    }
                }
                else
                {
                    if (!Validacoes.validarCampos(new List<Control>()
                    {
                        cbxEntidade,
                        dtpDataInicial,
                        cbxSituacaoComprimento
                    }))
                        Mensagens.MensagemAlertaOk("Favor informar os dados obrigatórios.");
                    else
                    {
                        SentenciadoEntidade movimentacaoBanco =
                            _serviceCumprimento.RetornaPorId(movimentacaoSelecionada.Id);
                        _serviceCumprimento.Alterar(popularCumprimento(movimentacaoBanco));
                        carregarDgvHistorico(sentenciadoSelecionado.Id);
                        alterEnableForm(false, gridDadosHistorico);
                        limpaTexto(gridDadosHistorico);
                        btnNovoHistorico.IsEnabled = true;
                    }
                }
            }
            catch (DbUpdateException dboException)
            {
                Mensagens.MensagemErroOk("Erro no update: " + dboException.Message);
            }
            catch (Exception exception)
            {
                Mensagens.MensagemErroOk("Erro inesperado ao gravar: " + exception.Message);
            }
        }

        private void tbxPenaAnos_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) && e.Key != Key.Tab)
                    e.Handled = true;
            }
            catch (Exception Exception)
            {
                Mensagens.MensagemErroOk("Erro inesperado: " + Exception.Message);
            }
        }

        private void tbxPenaMeses_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (tbxPenaMeses.Text != "")
                {
                    if (Convert.ToInt32(tbxPenaMeses.Text) > 11)
                    {
                        tbxPenaMeses.Text = "0";
                        Mensagens.MensagemAlertaOk("Atenção: O campo Meses não pode ser maior que 11");
                    }
                }
            }
            catch (Exception Exception)
            {
                Mensagens.MensagemErroOk("Erro inesperado: " + Exception.Message);
            }
        }

        private void tbxPenaDias_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (tbxPenaDias.Text != "")
                {
                    tbxPenaDias.BorderBrush = new SolidColorBrush(Colors.Blue);
                    if (Convert.ToInt32(tbxPenaDias.Text) > 29)
                    {
                        tbxPenaDias.Text = "0";
                        Mensagens.MensagemAlertaOk("Atenção: O campo Dias não pode ser maior que 29");
                    }
                }
            }
            catch (Exception Exception)
            {
                Mensagens.MensagemErroOk("Erro inesperado: " + Exception.Message);
            }
        }

        private void tbxPenaAnos_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox)
                (sender as TextBox).SelectAll();
        }

        private void tbxNome_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((e.Source as TextBox).Text.Any())
                (e.Source as TextBox).BorderBrush = new SolidColorBrush(Colors.Blue);
        }

        private void cbxSexo_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Source as ComboBox).SelectedIndex != -1)
                (e.Source as ComboBox).BorderBrush = new SolidColorBrush(Colors.Blue);
        }

        private void cbxSexo_DropDownClosed(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex != -1)
                (sender as ComboBox).BorderBrush = new SolidColorBrush(Colors.Blue);
        }

        private void dtpDataNascimento_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as DatePicker).SelectedDate != null)
                (sender as DatePicker).BorderBrush = new SolidColorBrush(Colors.Blue);
        }
    }
}
