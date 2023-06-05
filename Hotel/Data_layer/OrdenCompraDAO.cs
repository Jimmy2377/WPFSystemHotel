using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Entity_layer;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Windows;

namespace Hotel.Data_layer
{
    public class OrdenCompraDAO
    {
        
            private ConnectionToMysql connection;

            public OrdenCompraDAO()
            {
                connection = new ConnectionToMysql();
            }

        public int InsertOrdenCompra(OrdenCompra ordenCompra)
        {
            string query = "INSERT INTO ordencompra (Fecha, TiempoEntrega, MontoTotal, Estado, Departamento, TipoCompra, Empleado_ID_Empleado) VALUES (@Fecha, @TiempoEntrega, @MontoTotal, @Estado, @Departamento, @TipoCompra, @ID_Empleado)";

            using (MySqlConnection con = connection.GetConnection())
            {
                con.Open();

                using (MySqlCommand command = new MySqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Fecha", ordenCompra.Fecha);
                    command.Parameters.AddWithValue("@TiempoEntrega", ordenCompra.TiempoEntrega);
                    command.Parameters.AddWithValue("@MontoTotal", ordenCompra.MontoTotal);
                    command.Parameters.AddWithValue("@Estado", ordenCompra.Estado);
                    command.Parameters.AddWithValue("@Departamento", ordenCompra.Departamento);
                    command.Parameters.AddWithValue("@TipoCompra", ordenCompra.TipoCompra);
                    command.Parameters.AddWithValue("@ID_Empleado", ordenCompra.ID_Empleado);

                    command.ExecuteNonQuery();

                    // Obtener el ID de la orden de compra insertada
                    int idOrdenCompra = Convert.ToInt32(command.LastInsertedId);

                    return idOrdenCompra;
                }
            }
        }

        public void InsertDetallesCompra(List<DetalleCompra> detallesCompra)
        {
            string query = "INSERT INTO detallecompra (ID_OrdenCompra, ID_Producto, Cantidad) VALUES (@ID_OrdenCompra, @ID_Producto, @Cantidad)";

            using (MySqlConnection con = connection.GetConnection())
            {
                con.Open();

                foreach (DetalleCompra detalle in detallesCompra)
                {
                    using (MySqlCommand command = new MySqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@ID_OrdenCompra", detalle.ID_OrdenCompra);
                        command.Parameters.AddWithValue("@ID_Producto", detalle.ID_Producto);
                        command.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                        command.ExecuteNonQuery();
                    }
                }
            }
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
                                int idOrdenCompra = Convert.ToInt32(reader["ID_OrdenCompra"]);
                                DateTime fecha = Convert.ToDateTime(reader["Fecha"]);
                                int tiempoEntrega = Convert.ToInt32(reader["TiempoEntrega"]);
                                double montoTotal = Convert.ToDouble(reader["MontoTotal"]);
                                string estado = reader["Estado"].ToString();
                                string departamento = reader["Departamento"].ToString();
                                string tipoCompra = reader["TipoCompra"].ToString();
                                int idEmpleado = Convert.ToInt32(reader["ID_Empleado"]);

                            OrdenCompra ordenCompra = new OrdenCompra(fecha, tiempoEntrega, montoTotal, estado, departamento, tipoCompra, idEmpleado);
                                
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
