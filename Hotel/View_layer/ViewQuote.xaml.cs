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

namespace Hotel.View_layer
{
    public partial class ViewQuote : UserControl
    {
        public ViewQuote()
        {
            InitializeComponent();
        }
        private void btncontrolproduct_Click(object sender, RoutedEventArgs e)
        {
            ViewProduct viewProduct = new ViewProduct();
            contentControl.Content = viewProduct;
        }

        private void btncontrolcategory_Click(object sender, RoutedEventArgs e)
        {
            ViewCategory viewCategory = new ViewCategory();
            contentControl.Content = viewCategory;
        }
    }
}
