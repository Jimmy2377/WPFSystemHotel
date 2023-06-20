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
using Hotel.Bussines_Layer;

namespace Hotel.View_layer
{
    public partial class ViewRecuperacion : Window
    {
        private WorkerModel workerModel;
        public ViewRecuperacion()
        {
            InitializeComponent();
            workerModel = new WorkerModel();
        }
        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void BtnRecuperarContraseña_Click(object sender, RoutedEventArgs e)
        {
            string ci = txtCI.Text;
            string respuesta = txtRespuesta.Text;
            string nuevaContraseña = txtNewPass.Password;

            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(respuesta) || string.IsNullOrWhiteSpace(nuevaContraseña))
            {
                MessageBox.Show("Ingrese todos los campos requeridos");
                return;
            }

            WorkerModel workerModel = new WorkerModel();
            bool passwordReset = workerModel.VerifyUserAndResetPassword(ci, respuesta, nuevaContraseña);

            if (passwordReset)
            {
                MessageBox.Show("Contraseña actualizada con éxito");
                // Cerrar la ventana de recuperación
                Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar la contraseña. Verifique los datos ingresados.");
            }
        }

    }
}
