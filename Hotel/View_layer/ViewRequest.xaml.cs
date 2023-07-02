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
using Hotel.Bussines_Layer;
using Hotel.Entity_layer;

namespace Hotel.View_layer
{
    public partial class ViewRequest : UserControl
    {
        private SolicitudBLL solicitudBLL;
        public ViewRequest()
        {
            InitializeComponent();
            solicitudBLL = new SolicitudBLL();
            LoadSolicitudes();
        }
        private void LoadSolicitudes()
        {
            List<Solicitud> solicitudes = solicitudBLL.GetAllSolicitudes();
            listBoxSolicitudes.ItemsSource = solicitudes;
        }
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxSolicitudes.SelectedItem != null)
            {
                Solicitud solicitudseleccionada = listBoxSolicitudes.SelectedItem as Solicitud;

                MessageBoxResult result = MessageBox.Show($"¿Estás seguro de eliminar la Solicitud?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    solicitudBLL.EliminarSolicitud(solicitudseleccionada.ID_Solicitud);

                    // Actualizar la lista de proveedores
                    LoadSolicitudes();
                }
            }
        }
        private void txtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBusqueda.Text.ToLower();

            // Obtén todos los proveedores y filtra según el texto ingresado
            List<Solicitud> solicitudesFiltradas = solicitudBLL.GetAllSolicitudes().Where(solicitud =>

                solicitud.Descripcion.ToLower().Contains(filtro) ||
                solicitud.Cantidad.ToLower().Contains(filtro) ||
                solicitud.Antecedentes.ToLower().Contains(filtro) ||
                solicitud.Precauciones.ToLower().Contains(filtro)
            ).ToList();

            // Actualiza la lista de proveedores mostrada en el ListBox
            listBoxSolicitudes.ItemsSource = solicitudesFiltradas;
        }
    }
}
