using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Entity_layer
{
    public class Cotizacion
    {
        public Cotizacion(int idCotizacion, string nombreProducto, string descripcion, double precioUnit, string tamaño, Categoria categoria, Proveedor proveedor, EstadoCotizacion estado)
        {
            IdCotizacion = idCotizacion;
            NombreProducto = nombreProducto;
            Descripcion = descripcion;
            PrecioUnit = precioUnit;
            Tamaño = tamaño;
            Categoria = categoria;
            Proveedor = proveedor;
            Estado = estado;
            Cantidad = 0; // Se inicializa en 0 y se actualizará cuando se seleccione la cotización en la orden de compra
        }
        public enum EstadoCotizacion
        {
            Pendiente,
            Aprobado
        }
        public int IdCotizacion { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public double PrecioUnit { get; set; }
        public string Tamaño { get; set; }
        public Categoria Categoria { get; set; }
        public Proveedor Proveedor { get; set; }
        public int Cantidad { get; set; }
        public EstadoCotizacion Estado { get; set; }

    }
}
