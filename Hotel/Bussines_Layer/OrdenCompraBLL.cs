using Hotel.Data_layer;
using Hotel.Entity_layer;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hotel.Entity_layer.OrdenCompra;


namespace Hotel.Bussines_Layer
{
    public class OrdenCompraBLL
    {
        private OrdenCompraDAO ordenCompraDAO;

        public OrdenCompraBLL()
        {
            ordenCompraDAO = new OrdenCompraDAO();
        }

        public void InsertOrdenCompra(OrdenCompra ordenCompra)
        {
            try
            {
                ordenCompraDAO.InsertOrdenCompra(ordenCompra);
                MessageBox.Show("Orden de Compra creada exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar la Orden de Compra: " + ex.Message);
                throw;
            }
        }

        public List<OrdenCompra> GetAllCompras(string condicional)
        {
            return ordenCompraDAO.GetAllOrdenCompras(condicional);
        }

        public void EliminarOrdenCompra(int idOrdenCompra)
        {
            try
            {
                ordenCompraDAO.EliminarOrdenCompra(idOrdenCompra);
                MessageBox.Show("Orden de Compra Cancelada.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar la Orden de Compra: " + ex.Message);
                throw;
            }
        }

        public void CambiarEstadoOrdenCompra(OrdenCompra ordenCompra)
        {
            string mensajeConfirmacion = "Orden Procesada con Exito";
            string mensajeEstado = "";

            switch (ordenCompra.Estado)
            {
                case EstadoOrdenCompra.Recibido:
                    mensajeEstado = "¿Estás seguro/a de Proceguir con la Orden Compra? \n IMPORTANTE: Orden Compra debera ser enviada al proveedor";
                    break;
                case EstadoOrdenCompra.Enviado:
                    mensajeEstado = "¿Estás seguro/a de Proceguir con la Orden Compra? \n IMPORTANTE: El proveedor debe confirmar el envio de la orden";
                    break;
                case EstadoOrdenCompra.EnCamino:
                    mensajeEstado = "¿Estás seguro/a de Proceguir con la Orden Compra? \n IMPORTANTE: El pedido llego a almacenes, proceso de compra Terminado";
                    break;
                case EstadoOrdenCompra.Almacen:
                    // No se permite cambiar el estado más allá de "Almacén"
                    return;
            }

            // Mostrar mensaje de confirmación y obtener la respuesta del usuario
            MessageBoxResult confirmacion = MessageBox.Show(mensajeEstado, "Confirmar Cambio de Estado", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirmacion == MessageBoxResult.Yes)
            {
                ordenCompra.Estado = (EstadoOrdenCompra)((int)ordenCompra.Estado + 1); // Cambiar el estado al siguiente
                ordenCompra.FechaEntrega = DateTime.Now;
                // Actualizar el estado en la capa de Datos
                ordenCompraDAO.ModificarOrdenCompra(ordenCompra);

                MessageBox.Show(mensajeConfirmacion, "Cambio de Estado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public void CambiarEstadoOrdenCompraProgramada(OrdenCompra ordenCompra)
        {
            string mensajeConfirmacion = "Orden Procesada con Exito";
            string mensajeEstado = "";

            switch (ordenCompra.Estado)
            {
                case EstadoOrdenCompra.Recibido:
                    mensajeEstado = "¿Estás seguro/a de Proceguir con la Orden Compra?";
                    break;
                case EstadoOrdenCompra.Almacen:
                    // No se permite cambiar el estado más allá de "Almacén"
                    return;
            }

            // Mostrar mensaje de confirmación y obtener la respuesta del usuario
            MessageBoxResult confirmacion = MessageBox.Show(mensajeEstado, "Confirmar Cambio de Estado", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirmacion == MessageBoxResult.Yes)
            {
                // Crear una copia del objeto ordenCompra original
                OrdenCompra ordenCompraCopia = ordenCompra.Clone();

                ordenCompra.Estado = (EstadoOrdenCompra)((int)ordenCompra.Estado + 3); // Cambiar el estado enum
                ordenCompra.FechaEntrega = DateTime.Now;
                // Actualizar el estado en la capa de Datos
                ordenCompraDAO.ModificarOrdenCompra(ordenCompra);

                // Insertar una copia de la ordenCompra original con su estado anterior
                ordenCompraCopia.Estado = EstadoOrdenCompra.Recibido; // Restaurar el estado anterior
                ordenCompraCopia.Fecha = DateTime.Now;
                ordenCompraDAO.InsertOrdenCompra(ordenCompraCopia);

                MessageBox.Show(mensajeConfirmacion, "Cambio de Estado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public List<DetalleCompra> ObtenerDetallesCompra(int idOrdenCompra)
        {
            return ordenCompraDAO.ObtenerDetallesCompra(idOrdenCompra);
        }

    }
}
