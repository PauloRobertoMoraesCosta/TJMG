using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TJ.View
{
    class MyTabItem : TabItem
    {
        public MyTabItem()
        {
            var myHeader = new UCHeader();
            this.Header = myHeader;
        }

        public string Title
        {
            set
            {
                ((UCHeader)this.Header).lblTabTitle.Content = value;
            }
        }

        //protected override void OnSelected(RoutedEventArgs e)
        //{
        //    base.OnSelected(e);
        //    ((UCHeader)this.Header).btnClose.Visibility = Visibility.Visible;
        //}

        //// Override OnUnSelected - Hide the Close Button
        //protected override void OnUnselected(RoutedEventArgs e)
        //{
        //    base.OnUnselected(e);
        //    ((UCHeader)this.Header).btnClose.Visibility = Visibility.Hidden;
        //}
    }
}
