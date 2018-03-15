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
    /// Interaction logic for WinCadEdReeducandoEntidade.xaml
    /// </summary>
    public partial class WinCadEdReeducandoEntidade : Window
    {
        private int sentenciadoSelecionadoId;
        private SentenciadoEntidade sentenciadoEntidadeSelecionado;
        private readonly WinCadEdReeducando _pai;

        #region "Construtores" 
        public WinCadEdReeducandoEntidade(WinCadEdReeducando pai, int sentenciadoId, SentenciadoEntidade sentenciadoEntidade)
        {
            InitializeComponent();
            Title = "Alteração de Entidade-Sentenciado";
            btnGravar.Content = "Alterar";
            _pai = pai;
            sentenciadoEntidadeSelecionado = sentenciadoEntidade;
            sentenciadoSelecionadoId = sentenciadoId;
            carregaDadosIniciais();
            carregaTelaSentenciadoEntidade(sentenciadoEntidadeSelecionado);
        }

        public WinCadEdReeducandoEntidade(WinCadEdReeducando pai, int sentenciadoId)
        {
            try
            {
                _pai = pai;
                sentenciadoSelecionadoId = sentenciadoId;
                InitializeComponent();
                carregaDadosIniciais();
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao carregar o cadastro de reeducando: " + exception.Message);
            }
        }

        #endregion

        #region "Eventos disparados"
        private void cbxEntidade_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.Source as ComboBox).SelectedIndex != -1)
                    (e.Source as ComboBox).BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999"));
            }
            catch (Exception ex)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(ex.Message);
            }
        }

        private void cbxEntidade_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if ((sender as ComboBox).SelectedIndex != -1)
                    (sender as ComboBox).BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999"));
            }
            catch (Exception ex)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(ex.Message);
            }
        }

        private void dtpDataInicial_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if ((sender as DatePicker).SelectedDate != null)
                    (sender as DatePicker).BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999"));
            }
            catch (Exception ex)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(ex.Message);
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

        private void btnGravar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Validacoes.validarCampos(new List<Control>() { cbxEntidade, cbxSituacaoComprimento, dtpDataInicial }))
                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("Favor informar os dados obrigatórios.");
                else
                {
                    if (sentenciadoEntidadeSelecionado == null)
                    {
                        using (IAppServiceSentenciadoEntidade serviceSentenciadoEntidade = MinhaNinject.Kernel.Get<IAppServiceSentenciadoEntidade>())
                        {
                            serviceSentenciadoEntidade.Adiciona(popularSentenciadoEntidade(new SentenciadoEntidade()));
                        }
                        (_pai as WinCadEdReeducando).carregarDgvHistorico(sentenciadoSelecionadoId);
                        (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowSuccess("Cadastro realizado com sucesso");
                        Close();
                    }
                    else
                    {
                        using (IAppServiceSentenciadoEntidade serviceSentenciadoEntidade = MinhaNinject.Kernel.Get<IAppServiceSentenciadoEntidade>())
                        {
                            serviceSentenciadoEntidade.Alterar(popularSentenciadoEntidade(serviceSentenciadoEntidade.RetornaPorId(sentenciadoEntidadeSelecionado.Id)));
                        }
                        (_pai as WinCadEdReeducando).carregarDgvHistorico(sentenciadoSelecionadoId);
                        (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowSuccess("Alteração realizada com sucesso");
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(sentenciadoEntidadeSelecionado == null ? "Ao gravar o usuário: " + ex.Message : "Ao alterar o usuário: " + ex.Message);
            }
        }

        #endregion

        #region "Metodos avulsos"
        private void carregaDadosIniciais()
        {
            using (IAppServiceEntidade serviceEntidade = MinhaNinject.Kernel.Get<IAppServiceEntidade>())
            {
                cbxEntidade.ItemsSource = serviceEntidade.RetornaEntidadesAtivas().OrderBy(e => e.Nome);
            }
            cbxEntidade.DisplayMemberPath = "Nome";
            cbxSituacaoComprimento.ItemsSource = new List<string> {
                    "Regular (Mínimo 24 horas mês)",
                    "Irregular (Entre 1 e 23 horas mês)",
                    "Descumprimento",
                    "Encaminhado(a)",
                    "PSC iniciada"};
        }

        private void carregaTelaSentenciadoEntidade(SentenciadoEntidade sentenciadoEntidade)
        {
            for (int i = 0; i < cbxEntidade.Items.Count; i++)
            {
                if ((cbxEntidade.Items[i] as Entidade).Id == sentenciadoEntidade.EntidadeId)
                {
                    cbxEntidade.SelectedItem = cbxEntidade.Items[i];
                    i = cbxEntidade.Items.Count;
                }
            }
            dtpDataFinal.SelectedDate = sentenciadoEntidade.DataFim;
            dtpDataInicial.SelectedDate = sentenciadoEntidade.DataInicio;
            cbxSituacaoComprimento.Text = sentenciadoEntidade.SituacaoCumprimento;
            tbxSentenciadoDiasHorarios.Text = sentenciadoEntidade.HorarioDeCumprimento;
            tbxAtividadePSC.Text = sentenciadoEntidade.AtividadePSC;
        }

        private SentenciadoEntidade popularSentenciadoEntidade(SentenciadoEntidade sentenciadoEntidade)
        {
            sentenciadoEntidade.DataFim = null;
            sentenciadoEntidade.DataFim = dtpDataFinal.SelectedDate != null ? dtpDataFinal.SelectedDate.Value : sentenciadoEntidade.DataFim;
            sentenciadoEntidade.DataInicio = dtpDataInicial.SelectedDate.Value;
            sentenciadoEntidade.EntidadeId = ((Entidade)cbxEntidade.SelectedItem).Id;
            sentenciadoEntidade.SituacaoCumprimento = cbxSituacaoComprimento.Text;
            sentenciadoEntidade.HorarioDeCumprimento = tbxSentenciadoDiasHorarios.Text;
            sentenciadoEntidade.SentenciadoId = sentenciadoSelecionadoId;
            sentenciadoEntidade.AtividadePSC = tbxAtividadePSC.Text;
            return sentenciadoEntidade;
        }
        #endregion
    }
}
