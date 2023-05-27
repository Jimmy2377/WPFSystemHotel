using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Entity_layer
{
    public class Categoria
    {
        public int ID_Categoria { get; set; }
        public string NombreCategoria { get; set; }
        public Categoria(int idCategoria, string nombreCategoria)
        {
            ID_Categoria = idCategoria;
            NombreCategoria = nombreCategoria;
        }
        public override string ToString()
        {
            return $"ID: {ID_Categoria}, Nombre: {NombreCategoria}";
        }
       
    }
}
