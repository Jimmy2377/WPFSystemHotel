using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Entity_layer
{
    public class DetalleCompra
    {
        public DetalleCompra(int iD_Producto, int cantidad, string nombreProducto, int iD_OrdenCompra)
        {
            ID_Producto = iD_Producto;
            Cantidad = cantidad;
            NombreProducto = nombreProducto;
            ID_OrdenCompra = iD_OrdenCompra;
        }

        public int ID_OrdenCompra { get; set; }
        public int ID_Producto { get; set; }
        public int Cantidad { get; set; }
        public string NombreProducto { get; set; }
    }
}
