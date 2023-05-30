using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Entity_layer
{
    public class DetalleCompra
    {
        public DetalleCompra(int iD_OrdenCompra, int iD_Cotizacion, int cantidad)
        {
            ID_OrdenCompra = iD_OrdenCompra;
            ID_Cotizacion = iD_Cotizacion;
            Cantidad = cantidad;
        }

        public int ID_OrdenCompra { get; set; }
        public int ID_Cotizacion { get; set; }
        public int Cantidad { get; set; }
    }
}
