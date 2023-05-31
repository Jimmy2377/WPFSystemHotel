using Hotel.Entity_layer;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Data_layer
{
    public class DetalleCompraDAO
    {
        private ConnectionToMysql connection;

        public DetalleCompraDAO()
        {
            connection = new ConnectionToMysql();
        }
        public void InsertarDetalleCompra(DetalleCompra detalleCompra)
        {
            using (MySqlConnection con = connection.GetConnection())
            {
                con.Open();

                // Insertar el detalle de compra en la tabla detallecompra
                string insertQuery = "INSERT INTO detallecompra (ID_OrdenCompra, ID_Producto, Cantidad) " +
                    "VALUES (@ID_OrdenCompra, @ID_Producto, @Cantidad)";

                using (MySqlCommand command = new MySqlCommand(insertQuery, con))
                {
                    command.Parameters.AddWithValue("@ID_OrdenCompra", detalleCompra.ID_OrdenCompra);
                    command.Parameters.AddWithValue("@ID_Producto", detalleCompra.ID_Producto);
                    command.Parameters.AddWithValue("@Cantidad", detalleCompra.Cantidad);

                    command.ExecuteNonQuery();
                }
            }
        }



        //public List<DetalleCompra> ObtenerDetallesCompra(int idOrdenCompra)
        //{
        //    // Código para obtener todos los detalles de compra de una orden específica de la base de datos
        //    return new List<DetalleCompra>();
        //}
    }
}
