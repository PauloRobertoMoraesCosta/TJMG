using Ninject;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TJ.Apresentacao;
using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;

namespace TJ.View
{
    /// <summary>
    /// Interaction logic for UCReeducando.xaml
    /// </summary>
    public partial class UCReeducandoLista : UserControl
    {
        private Usuario usuarioLogado;
        public Sentenciado sentenciadoSelecionado;

        #region "Construtores"
        public UCReeducandoLista(Usuario usuario)
        {
            InitializeComponent();
            //this.Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            usuarioLogado = usuario;
            carregarDgvReeducando();
        }
        #endregion

        #region "Eventos disparados"
        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WinCadEdReeducando cadEdReeducando = new WinCadEdReeducando(usuarioLogado, this);
                cadEdReeducando.ShowDialog();
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao iniciar o cadastro de reeducando: " + exception.Message);
            }
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
                            using (IAppServiceSentenciado serviceSentenciado = MinhaNinject.Kernel.Get<IAppServiceSentenciado>())
                            {
                                Sentenciado sentenciadoAtual = serviceSentenciado.RetornaPorId((dgvReeducando.SelectedItems[i] as Sentenciado).Id);

                                if (sentenciadoAtual.SentenciadoEntidades.Count() > 0)
                                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("O sentenciado está vinculado a alguma instituição, por isso não posso excluí-lo.");
                                else
                                {
                                    serviceSentenciado.Remover(sentenciadoAtual);
                                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowSuccess("Exclusão realizada com sucesso");
                                }
                            }
                        }
                        carregarDgvReeducando();
                    }
                }
                else
                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("Você deve selecionar ao menos um reeducando para excluí-lo");
            }
            catch (DbEntityValidationException dbEntityValidationException)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao excluir o reeducando: " + dbEntityValidationException.Message);
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao excluir o reeducando: " + exception.Message);
            }
        }
        private void dgvReeducando_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgvReeducando.SelectedItems.Count == 1)
                {
                    using (IAppServiceSentenciado serviceSentenciado = MinhaNinject.Kernel.Get<IAppServiceSentenciado>())
                    {
                        sentenciadoSelecionado = serviceSentenciado.RetornaPorId((dgvReeducando.SelectedItem as Sentenciado).Id);
                    }
                    WinCadEdReeducando cadEdReeducando = new WinCadEdReeducando(usuarioLogado, this, sentenciadoSelecionado);
                    cadEdReeducando.ShowDialog();
                }
                else
                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("Para editar algum sentenciado, clique duas vezes no mesmo!");
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao tentar carregar a edição de reeducando: " + exception.Message);
            }
        }
        private void dgvReeducando_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!btnExcluir.IsKeyboardFocused && !dgvReeducando.IsKeyboardFocusWithin)
                    dgvReeducando.SelectedItem = null;
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError(exception.Message);
            }
        }
        #endregion

        #region "Metodos avulsos"
        public void carregarDgvReeducando()
        {
            using (IAppServiceSentenciado serviceSentenciado = MinhaNinject.Kernel.Get<IAppServiceSentenciado>())
            {
                dgvReeducando.ItemsSource = serviceSentenciado.RetornaTodos().OrderBy(r => r.Nome);
            }
        }
    }
    #endregion
}

