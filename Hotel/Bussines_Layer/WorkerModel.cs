using Hotel.Data_layer;
using Hotel.Entity_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Security.Cryptography;

namespace Hotel.Bussines_Layer
{
    class WorkerModel
    {
        UserPerson userPerson = new UserPerson();
        public bool LoginUser(string user, string pass)
        {
            // Encriptar la contraseña ingresada
            string encryptedPassword = EncryptPassword(pass);
            return userPerson.Login(user, encryptedPassword);
        }
        public bool VerifityUser(string NombreUsuario)
        {
            
            if (userPerson.SearchUsuario(NombreUsuario))
            {
                string pregunta = userPerson.GetPregunta(NombreUsuario);
                UsuarioSesion.Question = pregunta;
                return true;
            }
            return false;
        }
        public bool VerifyUserAndResetPassword(string ci, string respuesta, string nuevaContraseña)
        {
            // Encriptar la contraseña ingresada
            string encryptedPassword = EncryptPassword(nuevaContraseña);
            return userPerson.VerifyUserAndResetPassword(ci, respuesta, encryptedPassword);
        }
        private string EncryptPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convertir la contraseña en bytes
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Calcular el hash de la contraseña
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                // Convertir el hash en una cadena hexadecimal
                string encryptedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                return encryptedPassword;
            }
        }
        public void RegistrarUsuario(UsuarioSesion usuario)
        {
            try
            {
                userPerson.RegistrarUsuario(usuario);
                MessageBox.Show("Usuario agregado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar el Cotizacion: " + ex.Message);
                MessageBox.Show("Usuario No agregado", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public List<UsuarioSesion> ObtenerTodosEmpleados()
        {
            return userPerson.ObtenerTodosEmpleados();
        }
    }
}