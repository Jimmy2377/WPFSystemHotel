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
    public partial class VentanaModalCantidad : Window
    {
        public int Cantidad { get; private set; }

        public VentanaModalCantidad()
        {
            InitializeComponent();
            // Configurar la inicialización de la ventana modal aquí
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            // Obtener la cantidad ingresada y asignarla a la propiedad Cantidad
            Cantidad = int.Parse(txtCantidad.Text);
            DialogResult = true; // Establecer el resultado de la ventana modal como verdadero
            Close(); // Cerrar la ventana modal
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Establecer el resultado de la ventana modal como falso
            Close(); // Cerrar la ventana modal
        }
    }
}
