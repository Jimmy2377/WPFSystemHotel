using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mysqlx.Crud.Order.Types;

namespace Hotel.Entity_layer
{ 
    public enum TipoUsuario
    {
        Administrador,
        Usuario
    }
    
    public class UsuarioSesion
    {
        public UsuarioSesion(int iD_Empleado, string nombreEmpleado, string apellidos, int cI, string direccion, int celular, string correo, string nombreUsuario, string claveUsuario, string pregunta, string respuesta, int iD_TipoUsuario, string departamento, int intentosFallidos, string estadoCuenta)
        {
            ID_Empleado = iD_Empleado;
            NombreEmpleado = nombreEmpleado;
            Apellidos = apellidos;
            CI = cI;
            Direccion = direccion;
            Celular = celular;
            Correo = correo;
            NombreUsuario = nombreUsuario;
            ClaveUsuario = claveUsuario;
            Pregunta = pregunta;
            Respuesta = respuesta;
            ID_TipoUsuario = iD_TipoUsuario;
            Departamento = departamento;
            IntentosFallidos = intentosFallidos;
            EstadoCuenta = estadoCuenta;
        }

        public static int IDuser { get; set; }
        public static string Nameuser { get; set; }
        public static string Lastnameuser { get; set; }
        public static string Question { get; set; }
        public static TipoUsuario TipoUsuario { get; set; }

        public int ID_Empleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string Apellidos { get; set; }
        public int CI { get; set; }
        public string Direccion { get; set; }
        public int Celular { get; set; }
        public string Correo { get; set; }
        public string NombreUsuario { get; set; }
        public string ClaveUsuario { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public int ID_TipoUsuario { get; set; }
        public string Departamento { get; set; }
        public int IntentosFallidos { get; set; }
        public string EstadoCuenta { get; set; }
    }
}
