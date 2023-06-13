using Hotel.Bussines_Layer;
using Hotel.Entity_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Hotel.View_layer
{
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

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            string username = txtuser.Text;
            string password = txtpass.Password;

            if (string.IsNullOrWhiteSpace(username))
            {
                ShowErrorMessage("Ingrese su nombre de usuario");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ShowErrorMessage("Ingrese su contraseña");
                return;
            }

            WorkerModel workerModel = new WorkerModel();
            bool validLogin = workerModel.LoginUser(username, password);

            if (validLogin)
            {
                if (UsuarioSesion.TipoUsuario == TipoUsuario.Administrador)
                {
                    MenuPrincipal mainmenu = new MenuPrincipal();
                    mainmenu.Show();
                    mainmenu.Closed += Logout;
                    Hide();
                }
                else if (UsuarioSesion.TipoUsuario == TipoUsuario.Usuario)
                {
                    MenuUsuario otraVentana = new MenuUsuario();
                    otraVentana.Show();
                    otraVentana.Closed += Logout;
                    Hide();
                }
            }
            else
            {
                ShowErrorMessage("Nombre de usuario o contraseña incorrectos, vuelva a intentar");
                txtpass.Clear();
                txtuser.Focus();
            }
        }

        private void ShowErrorMessage(string message)
        {
            lblErrorUser.Content = " " + message;
            lblErrorUser.Visibility = Visibility.Visible;
        }

        private void Logout(object sender, EventArgs e)
        {
            txtpass.Clear();
            txtuser.Clear();
            lblErrorUser.Visibility = Visibility.Collapsed;
            Show();
            txtuser.Focus();
        }
        private void SingUP_Click(object sender, EventArgs e)
        {
            SignUP ventanaRegistro = new SignUP();
            ventanaRegistro.ShowDialog();
        }

    }
}
