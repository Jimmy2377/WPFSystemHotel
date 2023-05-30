using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Entity_layer
{
    class CotizacionElegida
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public double PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public double Subtotal => PrecioUnitario * Cantidad;

        public Cotizacion Cotizacion { get; set; }

        public CotizacionElegida(int idProducto, string nombreProducto, double precioUnitario, int cantidad)
        {
            IdProducto = idProducto;
            NombreProducto = nombreProducto;
            PrecioUnitario = precioUnitario;
            Cantidad = cantidad;
            
        }
    }
}
