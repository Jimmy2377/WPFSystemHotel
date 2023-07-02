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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Hotel.View_layer
{
    public partial class ViewNewRequest : UserControl
    {
        private SolicitudBLL solicitudBLL;
        public ViewNewRequest()
        {
            InitializeComponent();
            solicitudBLL = new SolicitudBLL();
        }
        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            string descripcion = txtDescripcion.Text;
            string cantidad = txtCantidad.Text;
            string precioProveedor = txtAntecedentes.Text;
            string precauciones = txtPrecauciones.Text;
            if (!ValidarCampos())
            {
                MessageBox.Show("Por favor, complete todos los campos marcado con *.");
                return;
            }
            // Obtener los valores de los controles de entrada
            Solicitud solicitud = new Solicitud(0, descripcion, cantidad, precioProveedor, precauciones);
            solicitudBLL.RegistrarSolicitud(solicitud);
            // Limpiar los controles de entrada después de la inserción exitosa
            LimpiarCampos();
        }
        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                return false;
            }

            return true;
        }
        private void LimpiarCampos()
        {
            txtDescripcion.Clear();
            txtCantidad.Clear();
            txtAntecedentes.Clear();
            txtPrecauciones.Clear();
        }
    }
}
