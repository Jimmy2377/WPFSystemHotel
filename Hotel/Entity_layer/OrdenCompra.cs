using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Entity_layer
{
    public class OrdenCompra
    {
        public OrdenCompra(int idOrdenCompra, DateTime fecha, int tiempoEntrega, double montoTotal, EstadoOrdenCompra estado, string departamento, string tipoCompra, int idEmpleado)
        {
            ID_OrdenCompra = idOrdenCompra;
            Fecha = fecha;
            TiempoEntrega = tiempoEntrega;
            MontoTotal = montoTotal;
            Estado = estado;
            Departamento = departamento;
            TipoCompra = tipoCompra;
            ID_Empleado = idEmpleado;
        }
        public enum EstadoOrdenCompra
        {
            Recibido,
            Enviado,
            EnCamino,
            Almacen
        }
        
        public int ID_OrdenCompra { get; set; }
        public DateTime Fecha { get; set; }
        public int TiempoEntrega { get; set; }
        public double MontoTotal { get; set; }
        public EstadoOrdenCompra Estado { get; set; }
        public string Departamento { get; set; }
        public string TipoCompra { get; set; }
        public int ID_Empleado { get; set; }
        public DateTime FechaEntrega { get; set; }
        public List<DetalleCompra> DetallesCompra { get; set; }

        public OrdenCompra Clone()
        {
            // Crear una nueva instancia de OrdenCompra
            OrdenCompra copia = new OrdenCompra(ID_OrdenCompra, Fecha, TiempoEntrega, MontoTotal, Estado, Departamento, TipoCompra, ID_Empleado);

            // Copiar la lista de detalles de compra
            if (DetallesCompra != null)
            {
                copia.DetallesCompra = new List<DetalleCompra>();
                foreach (var detalle in DetallesCompra)
                {
                    copia.DetallesCompra.Add(new DetalleCompra(detalle.ID_Producto, detalle.Cantidad, detalle.NombreProducto));
                }
            }

            // Devolver la copia
            return copia;
        }
    }
}
