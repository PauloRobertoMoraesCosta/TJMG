using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.IO;
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
    /// Interaction logic for WinCadEdReeducando.xaml
    /// </summary>
    public partial class WinCadEdReeducando : Window
    {
        private Usuario usuarioLogado;
        private Sentenciado sentenciadoSelecionado;
        private readonly UCReeducandoLista _pai;

        #region "Construtores" 
        public WinCadEdReeducando(Usuario usuario, UCReeducandoLista pai, Sentenciado sentenciado)
        {
            InitializeComponent();
            grdDadosComplementares.Height = 0;
            lblMostrarDadosComplementares.Content = "Mostrar dados complementares";
            Title = "Alteração de reeducando";
            btnGravar.Content = "Alterar";
            usuarioLogado = usuario;
            _pai = pai;
            sentenciadoSelecionado = sentenciado;
            carregaDadosIniciais();
            carregarDgvHistorico(sentenciadoSelecionado.Id);
            popularTelaSentenciado(sentenciadoSelecionado);
        }

        public WinCadEdReeducando(Usuario usuario, UCReeducandoLista pai)
        {
            usuarioLogado = usuario;
            _pai = pai;
            InitializeComponent();
            carregaDadosIniciais();
            lblInclusao.Visibility = Visibility.Hidden;
            lblUsuarioInclusao.Visibility = Visibility.Hidden;
            lblEdicao.Visibility = Visibility.Hidden;
            lblUsuarioEdicao.Visibility = Visibility.Hidden;
            gbxHistoricoEntidade.Height = 0;
        }
        #endregion

        #region "Eventos disparados"
        public void carregarDgvHistorico(int sentenciadoId)
        {
            using (IAppServiceSentenciadoEntidade serviceSentenciadoEntidade = MinhaNinject.Kernel.Get<IAppServiceSentenciadoEntidade>())
            {
                dgvHistorico.ItemsSource = serviceSentenciadoEntidade.RetornarPorSentenciado(sentenciadoId).OrderBy(mse => mse.DataFim);
            }
        }

        private void btnGravar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Validacoes.validarCampos(new List<Control>() { tbxNome, cbxSexo, tbxProcesso, cbxOrigem, tbxPenaDias, tbxEndereco, cbxBairro, cbxCidade, cbxEscolaridade, cbxResponsavel, dtpDataNascimento, cbxEstadoCivil }))
                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("Favor informar os dados obrigatórios.");
                else
                {
                    if (sentenciadoSelecionado == null)
                    {
                        using (IAppServiceSentenciado serviceSentenciado = MinhaNinject.Kernel.Get<IAppServiceSentenciado>())
                        {
                            serviceSentenciado.Adiciona(popularSentenciado(new Sentenciado()));
                        }
                        Close();
                        (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowSuccess("Cadastro realizado com sucesso.");
                        _pai.carregarDgvReeducando();
                    }
                    else
                    {
                        using (IAppServiceSentenciado serviceSentenciado = MinhaNinject.Kernel.Get<IAppServiceSentenciado>())
                        {
                            Sentenciado sentenciadoBanco = serviceSentenciado.RetornaPorId(sentenciadoSelecionado.Id);
                            serviceSentenciado.Alterar(popularSentenciado(sentenciadoBanco));
                        }
                        _pai.carregarDgvReeducando();
                        Close();
                        (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowSuccess("Alteração realizada com sucesso.");
                    }
                }
            }
            catch (DbEntityValidationException dbEntityValidationException)
            {
                StreamWriter sw = new StreamWriter(Validacoes.caminhoExe() + "log " + DateTime.Now.ToString("ddMMyyyyhhmm") + ".txt");
                foreach (var eve in dbEntityValidationException.EntityValidationErrors)
                {
                    sw.WriteLine("Entidade do tipo \"{0}\" no estado \"{1}\" teve a validação de erro:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        sw.WriteLine("- Propriedade: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                sw.Close();
                throw;
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao gravar o reeducando - " + exception.Message);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnNovoHistorico_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WinCadEdReeducandoEntidade winCadEdReeducandoEntidade = new WinCadEdReeducandoEntidade(this, sentenciadoSelecionado.Id);
                winCadEdReeducandoEntidade.ShowDialog();
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowSuccess("Ao clicar no histórico - " + exception.Message);
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
                            using (IAppServiceSentenciadoEntidade serviceSentenciadoEntidade = MinhaNinject.Kernel.Get<IAppServiceSentenciadoEntidade>())
                            {
                                SentenciadoEntidade sentenciadoEntidade = serviceSentenciadoEntidade.RetornaPorId((dgvHistorico.SelectedItems[i] as SentenciadoEntidade).Id);

                                if (sentenciadoEntidade.CumprimentosMes.Count > 0)
                                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("O registro foi utilizado em algum cumprimento, por isso não posso excluí-lo.");
                                else
                                {
                                    serviceSentenciadoEntidade.Remover(sentenciadoEntidade);
                                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowSuccess("Registro excluido com sucesso.");
                                }
                            }
                        }
                        carregarDgvHistorico(sentenciadoSelecionado.Id);
                    }
                }
                else
                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("Selecione pelo menos um histórico.");
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao excluir o histórico - " + exception.Message);
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
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(Exception.Message);
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
                        (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("O campo Meses não pode ser maior que 11");
                    }
                }
            }
            catch (Exception Exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(Exception.Message);
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
                        (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("O campo Dias não pode ser maior que 29");
                    }
                }
            }
            catch (Exception Exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(Exception.Message);
            }
        }

        private void tbxPenaAnos_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is TextBox)
                    (sender as TextBox).SelectAll();
            }
            catch (Exception Exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(Exception.Message);
            }
        }

        private void tbxNome_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if ((e.Source as TextBox).Text.Any())
                    (e.Source as TextBox).BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999"));
            }
            catch (Exception Exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(Exception.Message);
            }
        }

        private void cbxSexo_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.Source as ComboBox).SelectedIndex != -1)
                    (e.Source as ComboBox).BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999"));
            }
            catch (Exception Exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(Exception.Message);
            }
        }

        private void cbxSexo_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if ((sender as ComboBox).SelectedIndex != -1)
                    (sender as ComboBox).BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999"));
            }
            catch (Exception Exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(Exception.Message);
            }
        }

        private void dtpDataNascimento_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if ((sender as DatePicker).SelectedDate != null)
                    (sender as DatePicker).BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999"));
            }
            catch (Exception Exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(Exception.Message);
            }
        }

        private void dgvHistorico_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgvHistorico.SelectedItems.Count == 1)
                {
                    WinCadEdReeducandoEntidade winCadEdReeducandoEntidade = new WinCadEdReeducandoEntidade(this, sentenciadoSelecionado.Id, (dgvHistorico.SelectedItem as SentenciadoEntidade));
                    winCadEdReeducandoEntidade.ShowDialog();
                }
                else
                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("Para editar algum sentenciado, clique duas vezes no mesmo!");
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao tentar carregar a edição de reeducando: " + exception.Message);
            }
        }

        private void lblMostrarDadosComplementares_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                lblMostrarDadosComplementares.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0D9Cfa"));
                lblMostrarDadosComplementares.FontWeight = FontWeights.Normal;
            }
            catch (Exception ex)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(ex.Message);
            }
        }

        private void lblMostrarDadosComplementares_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                grdDadosComplementares.Height = grdDadosComplementares.Height == 0 ? double.NaN : 0;
                lblMostrarDadosComplementares.Content = lblMostrarDadosComplementares.Content.Equals("Ocultar dados complementares") ? "Mostrar dados complementares" : "Ocultar dados complementares";
            }
            catch (Exception ex)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(ex.Message);
            }
        }

        private void lblMostrarDadosComplementares_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                lblMostrarDadosComplementares.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0D9Cff"));
                lblMostrarDadosComplementares.FontWeight = FontWeights.Bold;
            }
            catch (Exception ex)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(ex.Message);
            }
        }

        private void dgvHistorico_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!btnExcluirHistorico.IsKeyboardFocused && !dgvHistorico.IsKeyboardFocusWithin)
                {
                    dgvHistorico.SelectedItem = null;
                }
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(exception.Message);
            }
        }

        private void lblCancelar_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                lblCancelar.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0D9Cfa"));
                lblCancelar.FontWeight = FontWeights.Normal;
            }
            catch (Exception ex)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(ex.Message);
            }
        }

        private void lblCancelar_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                lblCancelar.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0D9Cff"));
                lblCancelar.FontWeight = FontWeights.Bold;
            }
            catch (Exception ex)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(ex.Message);
            }
        }

        private void lblCancelar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(ex.Message);
            }
        }
        #endregion

        #region "Metodos avulsos"
        private void carregaDadosIniciais()
        {
            using (IAppServiceBairro serviceBairro = MinhaNinject.Kernel.Get<IAppServiceBairro>())
            {
                cbxBairro.ItemsSource = serviceBairro.RetornaTodosAsNoTracking().OrderBy(b => b.Nome);
            }
            cbxBairro.DisplayMemberPath = "Nome";
            using (IAppServiceCidade serviceCidade = MinhaNinject.Kernel.Get<IAppServiceCidade>())
            {
                cbxCidade.ItemsSource = serviceCidade.RetornaTodosAsNoTracking().OrderBy(c => c.Nome);
            }
            cbxCidade.DisplayMemberPath = "Nome";
            using (IAppServiceUsuario serviceUsuario = MinhaNinject.Kernel.Get<IAppServiceUsuario>())
            {
                cbxResponsavel.ItemsSource = serviceUsuario.RetornaTodosAsNoTracking().Where(u => u.Super.Equals("True", StringComparison.OrdinalIgnoreCase)).OrderBy(e => e.Nome);
            }
            cbxResponsavel.DisplayMemberPath = "Nome";
            cbxSexo.ItemsSource = new List<string>
            {
                "Masculino",
                "Feminino"
            };
            cbxOrigem.ItemsSource = new List<string>
            {
                "Vara de Execuções Penais",
                "1ª Vara Criminal",
                "2ª Vara Criminal",
                "3ª Vara Criminal",
                "1ª U.J. – 1° J.D.",
                "1ª U.J. – 2° J.D.",
                "1ª U.J. – 3° J.D."
            };
            cbxSituacaoPena.ItemsSource = new List<string>
            {
                "Ativa",
                "Convertida multa",
                "Convertida PPE",
                "Convertida PPL",
                "Convertida tratamento",
                "Cumprida",
                "Ext. punib. - falecimento",
                "Ext. punib. - presc. pret. punit.",
                "Indultada",
                "Suspensa",
                "Transferida outra comarca",
                "Outra"
            };
            cbxEstadoCivil.ItemsSource = new List<string>
            {
                "Amasiado(a)",
                "Casado(a)",
                "Divorciado(a)",
                "Separado(a) de Corpos",
                "Solteiro(a)",
                "Viúvo(a)",
                "Outro(a)"
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
                "Pós-Graduação Completa",
                "Mestrado Incompleto",
                "Mestrado Completo",
                "Doutorado Incompleto",
                "Doutorado Completo"
            };
        }
        private Sentenciado popularSentenciado(Sentenciado sentenciado)
        {
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
            sentenciado.Detracao = tbxDetracao.Text.Equals("") ? 0 : Convert.ToInt32(tbxDetracao.Text);
            sentenciado.DetracaoObservacao = tbxObservacaoDetracao.Text;
            sentenciado.SomaDePena = tbxSomaDePena.Text.Equals("") ? 0 : Convert.ToInt32(tbxSomaDePena.Text);
            sentenciado.SomaDePenaObservacao = tbxObservacaoSoma.Text;
            if (sentenciadoSelecionado == null)
                sentenciado.UsuarioCadastroId = usuarioLogado.Id;
            else
                sentenciado.UsuarioAlteracaoId = usuarioLogado.Id;
            return sentenciado;
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
            lblUsuarioInclusao.Content = sentenciado.UsuarioCadastro.Nome;
            lblUsuarioEdicao.Content = sentenciado.UsuarioAlteracao != null ? sentenciado.UsuarioAlteracao.Nome : "";
            tbxDetracao.Text = sentenciado.Detracao.ToString();
            tbxObservacaoDetracao.Text = sentenciado.DetracaoObservacao;
            tbxSomaDePena.Text = sentenciado.SomaDePena.ToString();
            tbxObservacaoSoma.Text = sentenciado.SomaDePenaObservacao;
            if (sentenciadoSelecionado.UsuarioAlteracaoId == null)
            {
                lblEdicao.Visibility = Visibility.Hidden;
                lblUsuarioEdicao.Visibility = Visibility.Hidden;
            }
        }
        #endregion
    }
}
