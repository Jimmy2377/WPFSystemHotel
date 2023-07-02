using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Data_layer;
using Hotel.Entity_layer;
using System.Windows;

namespace Hotel.Bussines_Layer
{
    internal class GastosBLL
    {
        GastosDAO gastosDAO = new GastosDAO();
        public void RegistrarValeCompra(ValeCompra valeCompra)
        {
            try
            {
                gastosDAO.InsertarValeCompra(valeCompra);
                MessageBox.Show("Vale Compra Registrada", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Solicitud no se pudo enviar: " + ex.Message);
                MessageBox.Show("No se pudo Registrar", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public List<ValeCompra> GetAllVales()
        {
            return gastosDAO.GetAllValesCompra();
        }
    }
}
