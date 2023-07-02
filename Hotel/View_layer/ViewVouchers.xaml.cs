using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hotel.Bussines_Layer;
using Hotel.Entity_layer;

namespace Hotel.View_layer
{
    public partial class ViewVouchers : UserControl
    {
        private GastosBLL gastosBLL;
        public ViewVouchers()
        {
            InitializeComponent();
            gastosBLL = new GastosBLL();
        }
        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarCampos())
            {
                MessageBox.Show("Por favor, complete todos los campos");
                return;
            }
            string descripcion = txtDescripcion.Text;
            double monto = Convert.ToDouble(txtMonto.Text);
            string departamento = cmbDepartamento.SelectedValue.ToString();
            
            // Obtener los valores de los controles de entrada
            ValeCompra vale = new ValeCompra(0, DateTime.Now, descripcion, monto, ObtenerIDEmpleado(), departamento);
            gastosBLL.RegistrarValeCompra(vale);
            // Limpiar los controles de entrada después de la inserción exitosa
            LimpiarCampos();
        }
        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtDescripcion.Text) || string.IsNullOrEmpty(txtMonto.Text) || cmbDepartamento.SelectedItem == null)
            {
                return false;
            }
            // Validar el formato del campo Monto
            string montoText = txtMonto.Text;
            bool esFormatoValido = Regex.IsMatch(montoText, @"^\d+(\,\d+)?$");
            if (!esFormatoValido)
            {
                MessageBox.Show("El campo Monto debe ser un número entero o decimal válido");
                return false;
            }
            return true;
        }
        private int ObtenerIDEmpleado()
        {
            int idEmpleado = UsuarioSesion.IDuser;
            return idEmpleado;
        }
        private void LimpiarCampos()
        {
            txtDescripcion.Clear();
            txtMonto.Clear();
            cmbDepartamento.SelectedIndex = -1;
        }
    }
}
