using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TJ.View
{
    /// <summary>
    /// Interaction logic for CloseableHeader.xaml
    /// </summary>
    public partial class UCHeader : UserControl
    {
        public UCHeader()
        {
            InitializeComponent();
        }
        
        private void lbTabTitle_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            btnClose.Margin = new Thickness(lblTabTitle.ActualWidth + 5, 3, 4, 0);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            ((this.Parent as MyTabItem).Parent as TabControl).Items.Remove(this.Parent as MyTabItem);
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            btnClose.Visibility = Visibility.Hidden;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            btnClose.Visibility = Visibility.Visible;
        }

        private void btnClose_MouseEnter(object sender, MouseEventArgs e)
        {
            btnClose.Foreground = Brushes.DarkRed;
        }

        private void btnClose_MouseLeave(object sender, MouseEventArgs e)
        {
            btnClose.Foreground = Brushes.Black;
        }

    }
}
