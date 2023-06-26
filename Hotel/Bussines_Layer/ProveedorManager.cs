using Hotel.Data_layer;
using Hotel.Entity_layer;
using System.Collections.Generic;

namespace Hotel.Bussines_Layer
{
    public class ProveedorManager
    {
        private ProveedorDAO proveedorDAO;

        public ProveedorManager()
        {
            proveedorDAO = new ProveedorDAO();
        }

        public List<Proveedor> GetAllProveedores()
        {
            return proveedorDAO.GetAllProveedores();
        }

        public void InsertProveedor(Proveedor proveedor)
        {
            proveedorDAO.InsertProveedor(proveedor);
        }

        public void EliminarProveedor(int idProveedor)
        {
            proveedorDAO.EliminarProveedor(idProveedor);
        }

        public void ModificarProveedor(Proveedor proveedor)
        {
            proveedorDAO.ModificarProveedor(proveedor);
        }
    }
}

