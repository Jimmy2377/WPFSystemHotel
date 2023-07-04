using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Hotel.Entity_layer;
using Microsoft.VisualBasic.Logging;
using Microsoft.Win32;
using System.Security.Cryptography;

namespace Hotel.View_layer
{
    public partial class SignUP : Window
    {
        private WorkerModel workerModel;
        public SignUP()
        {
            InitializeComponent();
            workerModel = new WorkerModel();
;        }
        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que todos los campos estén llenos
            if (!ValidarCampos())
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }
            // Obtener los valores de los controles de entrada
            string nombre = txtNombre.Text;
            string apellidos = txtApellido.Text;
            int ci = int.Parse(txtCI.Text);
            string direccion = txtDireccion.Text;
            int celular = int.Parse(txtCelular.Text);
            string correo = txtCorreo.Text;
            string nombreUsuario = txtNombreUsuario.Text;
            string claveUsuario = txtClaveUsuario.Password;
            string pregunta = txtPregunta.Text;
            string respuesta = txtRespuesta.Text;
            string departamento = cmbDepartamento.SelectedValue.ToString();

            // Encriptar la contraseña utilizando SHA256
            string claveEncriptada;
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convierte la contraseña en un array de bytes
                byte[] bytesClave = Encoding.UTF8.GetBytes(claveUsuario);

                // Calcula el hash utilizando SHA256
                byte[] hashBytes = sha256.ComputeHash(bytesClave);

                // Convierte el hash en una cadena hexadecimal
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    stringBuilder.Append(hashBytes[i].ToString("x2"));
                }

                // Obtiene la contraseña encriptada
                claveEncriptada = stringBuilder.ToString();
            }
            
            // Verificar si solo se permiten letras en el nombre y apellidos
            if (!EsSoloLetras(nombre) || !EsSoloLetras(apellidos))
            {
                MessageBox.Show("El nombre y los apellidos solo deben contener letras y espacios en blanco.");
                return;
            }

            // Verificar si solo se permiten números en el CI y celular
            if (!EsSoloNumeros(ci.ToString()) || !EsSoloNumeros(celular.ToString()))
            {
                MessageBox.Show("El CI y el celular solo deben contener números.");
                return;
            }

            // Obtener los valores de los controles de entrada
            UsuarioSesion usuarioSesion = new UsuarioSesion(0, nombre, apellidos, ci, direccion, celular, correo, nombreUsuario, claveEncriptada, pregunta, respuesta,2, departamento, 0, "");
            workerModel.RegistrarUsuario(usuarioSesion);
            // Limpiar los controles de entrada después de la inserción exitosa
            LimpiarCampos();
            Close();
        }
        public bool EsSoloLetras(string cadena)
        {
            return cadena.All(c => char.IsLetter(c) || c == ' ');
        }

        // Método para verificar si una cadena solo contiene números
        public bool EsSoloNumeros(string cadena)
        {
            return cadena.All(char.IsDigit);
        }
        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text) ||
                string.IsNullOrEmpty(txtCI.Text) || string.IsNullOrEmpty(txtDireccion.Text) ||
                string.IsNullOrEmpty(txtCelular.Text) || string.IsNullOrEmpty(txtCorreo.Text) ||
                string.IsNullOrEmpty(txtNombreUsuario.Text) || string.IsNullOrEmpty(txtClaveUsuario.Password) ||
                string.IsNullOrEmpty(txtPregunta.Text) || string.IsNullOrEmpty(txtRespuesta.Text) ||
                cmbDepartamento.SelectedItem == null)
            {
                return false;
            }

            return true;
        }
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtCI.Clear();
            txtDireccion.Clear();
            txtCelular.Clear();
            txtCorreo.Clear();
            txtNombreUsuario.Clear();
            txtClaveUsuario.Clear();
            txtPregunta.Clear();
            txtRespuesta.Clear();
            cmbDepartamento.SelectedIndex = -1;
        }

    }
}
