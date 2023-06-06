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

        public void InsertOrdenCompra(OrdenCompra ordenCompra)
        {
            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "INSERT INTO ordencompra (Fecha, TiempoEntrega, MontoTotal, Estado, Departamento, TipoCompra, Empleado_ID_Empleado) " +
                                   "VALUES (@Fecha, @TiempoEntrega, @MontoTotal, @Estado, @Departamento, @TipoCompra, @ID_Empleado)";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Fecha", ordenCompra.Fecha);
                    cmd.Parameters.AddWithValue("@TiempoEntrega", ordenCompra.TiempoEntrega);
                    cmd.Parameters.AddWithValue("@MontoTotal", ordenCompra.MontoTotal);
                    cmd.Parameters.AddWithValue("@Estado", ordenCompra.Estado);
                    cmd.Parameters.AddWithValue("@Departamento", ordenCompra.Departamento);
                    cmd.Parameters.AddWithValue("@TipoCompra", ordenCompra.TipoCompra);
                    cmd.Parameters.AddWithValue("@ID_Empleado", ordenCompra.ID_Empleado);
                    cmd.ExecuteNonQuery();

                    // Obtener el último ID insertado
                    ordenCompra.ID_OrdenCompra = Convert.ToInt32(cmd.LastInsertedId);

                    // Insertar los detalles de compra
                    foreach (DetalleCompra detalle in ordenCompra.DetallesCompra)
                    {
                        InsertDetalleCompra(detalle, ordenCompra.ID_OrdenCompra);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar la Orden de Compra: " + ex.Message);
                MessageBox.Show("Error al insertar la Orden de Compra." + ex.Message);
            }
        }

        private void InsertDetalleCompra(DetalleCompra detalle, int idOrdenCompra)
        {
            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "INSERT INTO detallecompra (ID_OrdenCompra, ID_Producto, Cantidad) " +
                                   "VALUES (@ID_OrdenCompra, @ID_Producto, @Cantidad)";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID_OrdenCompra", idOrdenCompra);
                    cmd.Parameters.AddWithValue("@ID_Producto", detalle.ID_Producto);
                    cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar el Detalle de Compra: " + ex.Message);
                MessageBox.Show("Error al insertar el Detalle de Compra." + ex.Message);
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
                                int idEmpleado = Convert.ToInt32(reader["Empleado_ID_Empleado"]);

                            OrdenCompra ordenCompra = new OrdenCompra(fecha, tiempoEntrega, montoTotal, estado, departamento, tipoCompra, idEmpleado);
                                
                                ordenCompras.Add(ordenCompra);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener las Órdenes de Compra: " + ex.Message);
                MessageBox.Show("Error al obtener las Órdenes de Compra: " + ex.Message);
            }

                return ordenCompras;
            }
        
    }
}
