using Hotel.Bussines_Layer;
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


namespace Hotel.View_layer
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txtuser.Text != "")
            {
                if (txtpass.Password != "")
                {
                    WorkerModel user = new WorkerModel();
                    var validLogin = user.LoginUser(txtuser.Text, txtpass.Password);
                    if(validLogin == true)
                    {
                        MenuPrincipal mainmenu = new MenuPrincipal();
                        mainmenu.Show();
                        this.Hide();
                    }
                    else
                    {
                        msgErrorUser("Nombre o usuario incorrecto, vuelve a intentar");
                        txtpass.Clear();
                        txtuser.Focus();
                    }
                }
                else msgErrorUser("Ingrese su contraseña");
            }
            else msgErrorUser("Ingrese su nombre de usuario");
        }
        private void msgErrorUser(string msg)
        {
            lblErrorUser.Content = " "+ msg;
            lblErrorUser.Visibility = Visibility;
        }
    }
}
