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
    public partial class ViewProductUser : UserControl
    {
        private CotizacionBLL cotizacionBLL;
        public ViewProductUser()
        {
            InitializeComponent();
            cotizacionBLL = new CotizacionBLL();
            LoadCotizaciones();
        }
        private void txtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBusqueda.Text.ToLower();

            // Obtén todos los proveedores y filtra según el texto ingresado
            List<Cotizacion> cotizacionesFiltrados = cotizacionBLL.GetAllCotizacionesCondicionadas("0").Where(cotizacion =>

                cotizacion.NombreProducto.ToLower().Contains(filtro) ||
                cotizacion.Descripcion.ToLower().Contains(filtro) ||
                cotizacion.Tamaño.ToLower().Contains(filtro) ||
                cotizacion.PrecioUnit.ToString().Contains(filtro) ||
                cotizacion.Proveedor.ToString().Contains(filtro) ||
                cotizacion.Categoria.NombreCategoria.ToString().Contains(filtro)

            ).ToList();

            // Actualiza la lista de proveedores mostrada en el ListBox
            listBoxCotizaciones.ItemsSource = cotizacionesFiltrados;
        }
        private void LoadCotizaciones()
        {
            List<Cotizacion> cotizaciones = cotizacionBLL.GetAllCotizacionesCondicionadas("'0'");
            listBoxCotizaciones.ItemsSource = cotizaciones;
            listBoxCotizaciones.DisplayMemberPath = "NombreProducto";
        }
        private void CambiarEstadoCotizacionButton_Click(object sender, EventArgs e)
        {
            // Obtener la cotización seleccionada
            Cotizacion cotizacionSeleccionada = listBoxCotizaciones.SelectedItem as Cotizacion;

            if (cotizacionSeleccionada != null)
            {
                    // Cambiar el estado de la cotización
                    bool resultado = cotizacionBLL.CambiarEstadoCotizacion(cotizacionSeleccionada);

                    if (resultado)
                    {
                        MessageBox.Show("Aprobado con exito, Ahora aparecera en orden compra", "Cambio de Estado", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadCotizaciones();
                    }
                    else
                    {
                        MessageBox.Show("Aprobado con exito, Ahora aparecera en orden compra", "Cambio de Estado", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
            }
            else
            {
                MessageBox.Show("Aprobado con exito, Ahora aparecera en orden compra", "Cambio de Estado", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

    }
}

