using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Entity_layer;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Hotel.Data_layer
{
    public class OrdenCompraDAO
    {
        private ConnectionToMysql connection;

        public OrdenCompraDAO()
        {
            connection = new ConnectionToMysql();
        }

        public int InsertOrdenCompra(OrdenCompra ordenCompra, List<DetalleCompra> detallesCompra)
        {
            int ordenCompraId = 0;
            MySqlTransaction transaction = null;

            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();

                    // Inicia la transacción
                    transaction = con.BeginTransaction();

                    // Inserta la Orden de Compra
                    string ordenCompraQuery = "INSERT INTO ordencompra (Fecha, TiempoEntrega, MontoTotal, Estado, Departamento, TipoCompra, ID_Empleado) " +
                        "VALUES (@Fecha, @TiempoEntrega, @MontoTotal, @Estado, @Departamento, @TipoCompra, @ID_Empleado)";
                    MySqlCommand ordenCompraCmd = new MySqlCommand(ordenCompraQuery, con, transaction);
                    ordenCompraCmd.Parameters.AddWithValue("@Fecha", ordenCompra.Fecha);
                    ordenCompraCmd.Parameters.AddWithValue("@TiempoEntrega", ordenCompra.TiempoEntrega);
                    ordenCompraCmd.Parameters.AddWithValue("@MontoTotal", ordenCompra.MontoTotal);
                    ordenCompraCmd.Parameters.AddWithValue("@Estado", ordenCompra.Estado);
                    ordenCompraCmd.Parameters.AddWithValue("@Departamento", ordenCompra.Departamento);
                    ordenCompraCmd.Parameters.AddWithValue("@TipoCompra", ordenCompra.TipoCompra);
                    ordenCompraCmd.Parameters.AddWithValue("@ID_Empleado", ordenCompra.ID_Empleado);
                    ordenCompraCmd.ExecuteNonQuery();

                    // Obtiene el ID de la Orden de Compra insertada
                    ordenCompraId = (int)ordenCompraCmd.LastInsertedId;

                    // Inserta los detalles de la compra
                    string detalleCompraQuery = "INSERT INTO detallecompra (ID_OrdenCompra, ID_Cotizacion, Cantidad) VALUES (@ID_OrdenCompra, @ID_Cotizacion, @Cantidad)";
                    MySqlCommand detalleCompraCmd = new MySqlCommand(detalleCompraQuery, con, transaction);
                    detalleCompraCmd.Parameters.AddWithValue("@ID_OrdenCompra", ordenCompraId);

                    foreach (DetalleCompra detalle in detallesCompra)
                    {
                        detalleCompraCmd.Parameters.Clear();
                        detalleCompraCmd.Parameters.AddWithValue("@ID_Cotizacion", detalle.ID_Cotizacion);
                        detalleCompraCmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                        detalleCompraCmd.ExecuteNonQuery();
                    }

                    // Confirma la transacción
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                // En caso de error, se realiza un rollback de la transacción
                if (transaction != null)
                    transaction.Rollback();

                Console.WriteLine("Error al insertar la Orden de Compra: " + ex.Message);
            }

            return ordenCompraId;
        }

        public List<OrdenCompra> GetAllOrdenCompras()
        {
            List<OrdenCompra> ordenCompras = new List<OrdenCompra>();

            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "SELECT * FROM ordencompra";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int iD_OrdenCompra = Convert.ToInt32(reader["ID_OrdenCompra"]);
                            DateTime fecha = Convert.ToDateTime(reader["Fecha"]);
                            int tiempoEntrega = Convert.ToInt32(reader["TiempoEntrega"]);
                            double montoTotal = Convert.ToDouble(reader["MontoTotal"]);
                            string estado = reader["Estado"].ToString();
                            string departamento = reader["Departamento"].ToString();
                            string tipoCompra = reader["TipoCompra"].ToString();
                            int idEmpleado = Convert.ToInt32(reader["ID_Empleado"]);

                            OrdenCompra ordenCompra = new OrdenCompra(iD_OrdenCompra, fecha, tiempoEntrega, montoTotal, estado, departamento, tipoCompra, idEmpleado);


                            ordenCompras.Add(ordenCompra);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener las Órdenes de Compra: " + ex.Message);
            }

            return ordenCompras;
        }
    }
}
