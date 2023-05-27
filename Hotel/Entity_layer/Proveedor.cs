using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Entity_layer
{
    public class Proveedor
    {
        public int ID_Proveedor { get; set; }
        public string NombreProv { get; set; }
        public string NIT { get; set; }
        public string Direccion { get; set; }
        public string Contactos { get; set; }

        public Proveedor(int idProveedor, string nombreProv, string nit, string direccion, string contactos)
        {
            ID_Proveedor = idProveedor;
            NombreProv = nombreProv;
            NIT = nit;
            Direccion = direccion;
            Contactos = contactos;
        }

        public override string ToString()
        {
            return $"ID: {ID_Proveedor}, Nombre: {NombreProv}, NIT: {NIT}, Dirección: {Direccion}, Contactos: {Contactos}";
        }
    }
}
