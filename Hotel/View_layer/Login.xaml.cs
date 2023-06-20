using Hotel.Bussines_Layer;
using Hotel.Entity_layer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


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
                MenuPrincipal mainmenu = new MenuPrincipal();
                // Obtener el tipo de usuario
                TipoUsuario tipoUsuario = UsuarioSesion.TipoUsuario;
                if (tipoUsuario == TipoUsuario.Administrador)
                {
                    mainmenu.CargarBotonesAdministrador();
                }
                else if (tipoUsuario == TipoUsuario.Usuario)
                {
                    mainmenu.CargarBotonesUsuario();
                }
                mainmenu.Show();
                mainmenu.Closed += Logout;
                Hide();
            }
            else
            {
                ShowErrorMessage("Nombre de usuario o contraseña incorrectos \n vuelva a intentar");
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
        private void OlvidoContraseña_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string NombreUsuario = txtuser.Text;
            if (string.IsNullOrWhiteSpace(NombreUsuario))
            {
                ShowErrorMessage("Ingrese su nombre de usuario");
                return;
            }
            WorkerModel workerModel = new WorkerModel();
            bool validUsuario = workerModel.VerifityUser(NombreUsuario);

            if (validUsuario)
            {
                ViewRecuperacion Recuperacion = new ViewRecuperacion();
                Recuperacion.lblPregunta.Content = UsuarioSesion.Question;
                Recuperacion.ShowDialog();
                ShowErrorMessage("");
            }
            else
            {
                ShowErrorMessage("Nombre de Usuario no Existe");
                txtpass.Clear();
                txtuser.Focus();
            } 
        }
    }
}
