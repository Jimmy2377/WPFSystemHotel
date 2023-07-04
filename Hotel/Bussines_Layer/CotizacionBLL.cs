using Hotel.Data_layer;
using Hotel.Entity_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Hotel.Entity_layer.Cotizacion;

namespace Hotel.Bussines_Layer
{
    public class CotizacionBLL
    {
        private CotizacionDAO cotizacionDAO;
        public CotizacionBLL()
        {
            cotizacionDAO = new CotizacionDAO();
        }

        public List<Cotizacion> GetAllCotizaciones()
        {
            return cotizacionDAO.GetAllCotizaciones();
        }
        public List<Cotizacion> GetAllCotizacionesCondicionadas(string condicional)
        {
            return cotizacionDAO.GetAllCotizacionesCondicionada(condicional);
        }
        public void InsertCotizacion(Cotizacion cotizacion)
        {
            try
            {
                cotizacionDAO.InsertCotizacion(cotizacion);
                MessageBox.Show("Cotizacion agregado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar el Cotizacion: " + ex.Message);
                MessageBox.Show("Cotizacion No agregado: " + ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void EliminarCotizacion(int idCotizacion)
        {
            try
            {
                cotizacionDAO.EliminarCotizacion(idCotizacion);
                MessageBox.Show("Cotizacion Eliminado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el cotizacion: " + ex.Message);
                MessageBox.Show("Cotizacion No Eliminado", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void ModificarCotizacion(Cotizacion cotizacion)
        {
            try
            {
                cotizacionDAO.ModificarCotizacion(cotizacion);
                MessageBox.Show("Cotizacion Modificado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al modificar el Cotizacion: " + ex.Message);
            }
        }
        public bool CambiarEstadoCotizacion(Cotizacion cotizacion)
        {
            // Verificar el estado actual de la cotización
            EstadoCotizacion estadoActual = cotizacion.Estado;

            // Determinar el siguiente estado
            EstadoCotizacion siguienteEstado = estadoActual == EstadoCotizacion.Pendiente ? EstadoCotizacion.Aprobado : EstadoCotizacion.Pendiente;

            // Solicitar confirmación para cambiar de estado
            MessageBoxResult confirmacion = MessageBox.Show($"¿Estás seguro de cambiar el estado de la cotización de {estadoActual} a {siguienteEstado}?", "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmacion == MessageBoxResult.Yes)
            {
                // Realizar la modificación en la base de datos
                bool resultado = cotizacionDAO.CambiarEstadoCotizacion(cotizacion, siguienteEstado, ObtenerIDEmpleado());

                if (resultado)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
        private int ObtenerIDEmpleado()
        {
            int idEmpleado = UsuarioSesion.IDuser;
            return idEmpleado;
        }


    }
}
