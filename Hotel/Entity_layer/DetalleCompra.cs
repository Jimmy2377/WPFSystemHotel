using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Entity_layer
{
    public class DetalleCompra
    {
        public DetalleCompra(int iD_Producto, int cantidad)
        {
            ID_Producto = iD_Producto;
            Cantidad = cantidad;
        }

        public int ID_OrdenCompra { get; set; }
        public int ID_Producto { get; set; }
        public int Cantidad { get; set; }
    }
}
