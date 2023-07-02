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
    internal class SolicitudBLL
    {
        SolicitudDAO solicitudDAO = new SolicitudDAO();
        public void RegistrarSolicitud(Solicitud solicitud)
        {
            try
            {
                solicitudDAO.InsertarSolicitud(solicitud);
                MessageBox.Show("Solicitud Enviada", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Solicitud no se pudo enviar: " + ex.Message);
                MessageBox.Show("Solicitud no se pudo enviar", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public void EliminarSolicitud(int idSolicitud)
        {
            solicitudDAO.DeleteSolicitud(idSolicitud);
        }
        public List<Solicitud> GetAllSolicitudes()
        {
            return solicitudDAO.GetAllSolicitudes();
        }
    }
}
