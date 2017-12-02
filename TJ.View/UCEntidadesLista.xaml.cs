using Ninject;
using System;
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
    /// Interaction logic for UCMovimentacoesLencoes.xaml
    /// </summary>
    public partial class UCEntidadesLista : UserControl
    {
        private Usuario usuarioLogado;
        private Entidade entidadeSelecionada;
        
        #region "Construtores"
        public UCEntidadesLista(Usuario usuario)
        {
            usuarioLogado = usuario;
            InitializeComponent();
            carregarDgvEntidade();
        }
        #endregion

        #region "Mêtodos diversos"
        public void carregarDgvEntidade()
        {
            using (IAppServiceEntidade serviceEntidade = MinhaNinject.Kernel.Get<IAppServiceEntidade>())
            {
                dgvEntidades.ItemsSource = serviceEntidade.RetornaTodos().OrderBy(e => e.Nome).OrderBy(e => e.Bairro.Nome).ToList();
            }
        }
        #endregion

        #region "Eventos disparados"
        private void btnNovoEntidade_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WinCadEdEntidade winCadEdEntidade = new WinCadEdEntidade(usuarioLogado, this);
                winCadEdEntidade.ShowDialog();
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao iniciar a inclusão de nova entidade: " + exception.Message);
            }
        }

        private void btnExcluiEntidade_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgvEntidades.SelectedItems.Count > 0)
                {
                    if (Mensagens.MensagemConfirmOkCancel("Deseja excluir as entidades selecionadas caso isso seja possível?") == MessageBoxResult.OK)
                    {
                        using (IAppServiceEntidade serviceEntidade = MinhaNinject.Kernel.Get<IAppServiceEntidade>())
                        {
                            for (int i = 0; i < dgvEntidades.SelectedItems.Count; i++)
                            {
                                entidadeSelecionada = serviceEntidade.RetornaPorId(((Entidade)dgvEntidades.SelectedItems[i]).Id);
                                if (entidadeSelecionada.SentenciadoEntidades.Count > 0)
                                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation(String.Format("A entidade {0} foi utilizado em algum sentenciado, por isso não posso excluí-la.", entidadeSelecionada.Nome));
                                else
                                {
                                    serviceEntidade.Remover(entidadeSelecionada);
                                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowSuccess("Entidade excluida com sucesso.");
                                }
                            }
                        }
                        carregarDgvEntidade();
                    }
                }
                else
                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("Você deve selecionar ao menos uma entidade!");
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao exlcuir a entidade: " + exception.Message);
            }
        }

        private void dgvEntidades_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgvEntidades.SelectedItems.Count == 1)
                {
                    using (IAppServiceEntidade serviceEntidade = MinhaNinject.Kernel.Get<IAppServiceEntidade>())
                    {
                        entidadeSelecionada = serviceEntidade.RetornaPorId((dgvEntidades.SelectedItem as Entidade).Id);
                    }
                    WinCadEdEntidade winCadEdEntidade = new WinCadEdEntidade(usuarioLogado, this, entidadeSelecionada);
                    winCadEdEntidade.ShowDialog();
                }
                else
                    (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowInformation("Para editar alguma entidade, clique duas vezes na mesma!");
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao clicar duas vezes no Grid: " + exception.Message);
            }
        }
        #endregion

        private void dgvEntidades_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!btnExcluir.IsKeyboardFocused && !dgvEntidades.IsKeyboardFocusWithin)
                {
                    dgvEntidades.SelectedItem = null;
                }
            }
            catch (Exception exception)
            {
                (App.Current.MainWindow as WpfTelaPrincipal)._vm.ShowError("Ao perder o foco: " + exception.Message);
            }
        }
    }
}
