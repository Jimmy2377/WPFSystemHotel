using Hotel.Data_layer;
using Hotel.Entity_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                MessageBox.Show("Cotizacion No agregado", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
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

    }
}
