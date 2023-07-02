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
    public partial class ViewUsers : UserControl
    {
        private WorkerModel workerModel;
        public ViewUsers()
        {
            InitializeComponent();
            workerModel = new WorkerModel();
            MostrarEmpleados();
        }
        public void MostrarEmpleados()
        {
            List<UsuarioSesion> empleados = workerModel.ObtenerTodosEmpleados();
            // Asignar la lista de empleados a la propiedad ItemsSource del control de la tabla
            listBoxEmpleados.ItemsSource = empleados;
        }
        private void txtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBusqueda.Text.ToLower();

            // Obtén todos los usuarios y filtra según el texto ingresado
            List<UsuarioSesion> usauriosFiltrados = workerModel.ObtenerTodosEmpleados().Where(usaurio =>

                usaurio.NombreEmpleado.ToString().ToLower().Contains(filtro) ||
                usaurio.Apellidos.ToString().ToLower().Contains(filtro) ||
                usaurio.Direccion.ToString().ToLower().Contains(filtro) ||
                usaurio.Correo.ToString().ToLower().Contains(filtro) ||
                usaurio.Departamento.ToString().ToLower().Contains(filtro) ||
                usaurio.EstadoCuenta.ToString().ToLower().Contains(filtro) ||
                usaurio.Celular.ToString().Contains(filtro) ||
                usaurio.CI.ToString().Contains(filtro)
            ).ToList();

            // Actualiza la lista de usuarios mostrada en el ListBox
            listBoxEmpleados.ItemsSource = usauriosFiltrados;
        }
        private void CambiarEstadoCuentaButton_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un empleado
            if (listBoxEmpleados.SelectedItem is UsuarioSesion empleadoSeleccionado)
            {
                MessageBoxResult result = MessageBox.Show("¿Estás seguro de cambiar el Estado de Cuenta del Usaurio?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (workerModel.CambiarEstadoCuenta(empleadoSeleccionado))
                    {
                        MessageBox.Show("El Cambio se realizó correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        MostrarEmpleados();
                    }
                    else
                    {
                        MessageBox.Show("Error al cambiar el estado del usaurio.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Usuario de la Lista.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}

