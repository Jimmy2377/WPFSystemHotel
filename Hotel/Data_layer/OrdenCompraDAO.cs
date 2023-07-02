using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Entity_layer;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Windows;
using static Hotel.Entity_layer.OrdenCompra;

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

        public List<OrdenCompra> GetAllOrdenCompras(string condicional)
        {
                List<OrdenCompra> ordenCompras = new List<OrdenCompra>();

                try
                {
                    using (MySqlConnection con = connection.GetConnection())
                    {
                        con.Open();
                        string query = "SELECT * FROM ordencompra " + condicional;
                        MySqlCommand cmd = new MySqlCommand(query, con);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idOrdenCompra = Convert.ToInt32(reader["ID_OrdenCompra"]);
                                DateTime fecha = Convert.ToDateTime(reader["Fecha"]);
                                int tiempoEntrega = Convert.ToInt32(reader["TiempoEntrega"]);
                                double montoTotal = Convert.ToDouble(reader["MontoTotal"]);
                                EstadoOrdenCompra estado = (EstadoOrdenCompra)Enum.Parse(typeof(EstadoOrdenCompra), reader["Estado"].ToString());
                                string departamento = reader["Departamento"].ToString();
                                string tipoCompra = reader["TipoCompra"].ToString();
                                int idEmpleado = Convert.ToInt32(reader["Empleado_ID_Empleado"]);
                                
                            DateTime? fechaEntrega = null; // Inicializar como nulo

                            if (!reader.IsDBNull(reader.GetOrdinal("FechaTerminada")))
                            {
                                fechaEntrega = Convert.ToDateTime(reader["FechaTerminada"]);
                            }

                            OrdenCompra ordenCompra = new OrdenCompra(idOrdenCompra,fecha, tiempoEntrega, montoTotal, estado, departamento, tipoCompra, idEmpleado, fechaEntrega);
                                
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

        public void EliminarOrdenCompra(int idOrdenCompra)
        {
            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string deleteDetalleQuery = "DELETE FROM detallecompra WHERE ID_OrdenCompra = @ID_OrdenCompra";
                    MySqlCommand deleteDetalleCmd = new MySqlCommand(deleteDetalleQuery, con);
                    deleteDetalleCmd.Parameters.AddWithValue("@ID_OrdenCompra", idOrdenCompra);
                    deleteDetalleCmd.ExecuteNonQuery();

                    string query = "DELETE FROM ordencompra WHERE ID_OrdenCompra = @ID_OrdenCompra";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID_OrdenCompra", idOrdenCompra);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Orden Compra Cancelada");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar la Orden de Compra: " + ex.Message);
                MessageBox.Show("Error al eliminar la Orden de Compra:" + ex.Message);
                throw;
            }
        }

        public List<DetalleCompra> ObtenerDetallesCompra(int idOrdenCompra)
        {
            List<DetalleCompra> detallesCompra = new List<DetalleCompra>();

            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "SELECT d.ID_Producto, d.Cantidad, p.NombreProducto , d.ID_OrdenCompra FROM detallecompra d INNER JOIN producto p ON d.ID_Producto = p.ID_Producto WHERE ID_OrdenCompra = @ID_OrdenCompra";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID_OrdenCompra", idOrdenCompra);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idProducto = Convert.ToInt32(reader["ID_Producto"]);
                            int cantidad = Convert.ToInt32(reader["Cantidad"]);
                            string nombreProducto = reader["NombreProducto"].ToString();
                            int idcompra = Convert.ToInt32(reader["ID_OrdenCompra"]);

                            // Crear instancia de DetalleCompra y agregar a la lista
                            DetalleCompra detalle = new DetalleCompra(idProducto, cantidad, nombreProducto, idcompra);
                            detallesCompra.Add(detalle);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los detalles de compra: " + ex.Message);
            }

            return detallesCompra;
        }

        public void ModificarOrdenCompra(OrdenCompra ordenCompra)
        {
                try
                {
                    using (MySqlConnection con = connection.GetConnection())
                    {
                    con.Open();

                    // Actualizar el estado de la orden de compra
                    string query = "UPDATE ordencompra SET Estado = @Estado, FechaTerminada = @FechaEstado WHERE ID_OrdenCompra = @ID_OrdenCompra";
                        MySqlCommand cmd = new MySqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@Estado", ordenCompra.Estado.ToString());
                        cmd.Parameters.AddWithValue("@FechaEstado", ordenCompra.FechaEntrega);
                        cmd.Parameters.AddWithValue("@ID_OrdenCompra", ordenCompra.ID_OrdenCompra);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al modificar la Orden de Compra: " + ex.Message);
                }
        }
        public void EliminarDetalleCompra(int idProducto, int idOrdenCompra)
        {
            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "DELETE FROM detallecompra WHERE ID_Producto = @ID_Producto AND ID_OrdenCompra = @ID_OrdenCompra";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID_Producto", idProducto);
                    cmd.Parameters.AddWithValue("@ID_OrdenCompra", idOrdenCompra);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Producto eliminado del detalle de compra: ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el detalle de compra: " + ex.Message);
                throw;
            }
        }
        public void ActualizarMontoTotal(int idOrdenCompra)
        {
            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();
                    string query = "UPDATE ordencompra SET MontoTotal = (SELECT SUM(dc.Cantidad * p.PrecioUnit) FROM detallecompra dc INNER JOIN producto p ON dc.ID_Producto = p.ID_Producto WHERE dc.ID_OrdenCompra = @ID_OrdenCompra) WHERE ID_OrdenCompra = @ID_OrdenCompra";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID_OrdenCompra", idOrdenCompra);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Monto total actualizado: ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el monto total de la orden de compra: " + ex.Message);
                throw;
            }
        }
        public void GuardarDevolucion(Devolucion devolucion)
        {
            try
            {
                using (MySqlConnection con = connection.GetConnection())
                {
                    con.Open();

                    string query = "INSERT INTO devoluciones (Motivo, DetalleCompra_ID_OrdenCompra, DetalleCompra_ID_Producto) " +
                                   "VALUES (@Motivo, @ID_OrdenCompra, @ID_Producto)";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Motivo", devolucion.Motivo);
                    cmd.Parameters.AddWithValue("@ID_OrdenCompra", devolucion.ID_OrdenCompra);
                    cmd.Parameters.AddWithValue("@ID_Producto", devolucion.ID_Producto);

                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Devolucion Guardada: ");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar la devolución: " + ex.Message);
                throw;
            }
        }

    }
}
