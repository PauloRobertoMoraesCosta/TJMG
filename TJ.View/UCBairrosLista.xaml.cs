using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TJ.Apresentacao.InterfacesApp;
using TJ.Dominio.Entidades;
using System.Data.Entity.Infrastructure;
using TJ.Apresentacao;
using Ninject;

namespace TJ.View
{
    /// <summary>
    /// Interaction logic for UCBairrosLista.xaml
    /// </summary>
    public partial class UCBairrosLista : UserControl
    {
        private Bairro bairroSelecionado;

        #region "Construtores"
        public UCBairrosLista()
        {
            InitializeComponent();
            carregaGridBairro();
        }
        #endregion

        #region "Eventos disparados"
        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WinCadEdBairro cadEdBairro = new WinCadEdBairro(this);
                cadEdBairro.ShowDialog();
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao iniciar a inclusão de novo bairro: " + exception.Message);
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgvBairros.SelectedItems.Count > 0)
                {
                    if (Mensagens.MensagemConfirmOkCancel("Deseja excluir os bairros selecionados caso isso seja possível?") == MessageBoxResult.OK)
                    {
                        for (int i = 0; i < dgvBairros.SelectedItems.Count; i++)
                        {
                            using (IAppServiceBairro serviceBairro = MinhaNinject.Kernel.Get<IAppServiceBairro>())
                            {
                                bairroSelecionado = serviceBairro.RetornaPorId((dgvBairros.SelectedItems[i] as Bairro).Id);

                                if (bairroSelecionado.Sentenciados.Count() > 0 || bairroSelecionado.Entidades.Count > 0)
                                {
                                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation(string.Format("O bairro {0} foi utilizado em algum reeducando ou entidade, por isso não posso excluí-lo.", bairroSelecionado.Nome));
                                }
                                else
                                {
                                    serviceBairro.Remover(bairroSelecionado);
                                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowSuccess("Bairro excluido com sucesso.");
                                }
                            }
                        }
                        carregaGridBairro();
                    }
                    else
                        dgvBairros.SelectedItem = null;
                }
                else
                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("Você deve selecionar ao menos um bairro.");
            }
            catch (DbUpdateException dbUpdateException)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao excluir bairro: O mesmo deve ter sido utilizado em algum cadastro." + dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao excluir bairro: " + exception.Message);
            }
        }

        private void dgvBairros_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgvBairros.SelectedItems.Count == 1)
                {
                    bairroSelecionado = dgvBairros.SelectedItem as Bairro;
                    WinCadEdBairro cadEdBairro = new WinCadEdBairro(bairroSelecionado, this);
                    cadEdBairro.ShowDialog();
                }
                else
                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("Para editar algum bairro, clique duas vezes no mesmo!");
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao clicar duas vezes na lista: " + exception.Message);
            }
        }

        private void dgvBairros_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!btnExcluir.IsKeyboardFocused && !dgvBairros.IsKeyboardFocusWithin)
                {
                    dgvBairros.SelectedItem = null;
                }
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao perder o foco: " + exception.Message);
            }
        }
        #endregion

        #region "Mêtodos diversos"

        public void carregaGridBairro()
        {
            using (IAppServiceBairro serviceBairro = MinhaNinject.Kernel.Get<IAppServiceBairro>())
            {
                dgvBairros.ItemsSource = serviceBairro.RetornaTodos().OrderBy(u => u.Nome).ToList();
            }
        }
        #endregion

    }
}
