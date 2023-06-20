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
    }
}
