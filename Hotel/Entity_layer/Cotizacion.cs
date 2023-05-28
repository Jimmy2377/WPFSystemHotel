using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Entity_layer
{
    public class Cotizacion
    {
        public Cotizacion(int idCotizacion, string nombreProducto, string descripcion, double precioUnit, string tamaño, Categoria categoria, Proveedor proveedor)
        {
            IdCotizacion = idCotizacion;
            NombreProducto = nombreProducto;
            Descripcion = descripcion;
            PrecioUnit = precioUnit;
            Tamaño = tamaño;
            Categoria = categoria;
            Proveedor = proveedor;
        }

        public int IdCotizacion { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public double PrecioUnit { get; set; }
        public string Tamaño { get; set; }
        public Categoria Categoria { get; set; }
        public Proveedor Proveedor { get; set; }
    }
}
