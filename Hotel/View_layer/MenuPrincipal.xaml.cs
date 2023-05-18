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
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Hotel.Entity_layer;

namespace Hotel.View_layer
{
    /// <summary>
    /// Lógica de interacción para MenuPrincipal.xaml
    /// </summary>
    /// 

    
    public partial class MenuPrincipal : Window
    {
        
        public MenuPrincipal()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            cargardatosusuario();
            
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void PanelDeControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161,2,0);
        }
       private void PanelDeControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }

        private void btnDevoluciones_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void btncontrolhome_Click(object sender, RoutedEventArgs e)
        {
            //contentControl.Content = controlHome;
            DataContext = new ViewHome();
        }
        private void btncontrolquote_Click(object sender, RoutedEventArgs e)
        {
            //contentControl.Content = controlQuote;
            DataContext = new ViewQuote();
        }


        private void btnLogout_click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("¿Estás seguro de que quieres cerrar la sesión?", "Warning",MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

      
        private void cargardatosusuario()
        {
            txtNombreUsuario.Text = UsuarioSesion.Nameuser;
            txtApellidoUsuario.Text = UsuarioSesion.Lastnameuser;
        }

    }
}
