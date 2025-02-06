using System;
using System.Windows.Forms;

namespace Cliente
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new AppContext());
        }
    }

    // Clase para gestionar los formularios de la aplicación
    public class AppContext : ApplicationContext
    {
        public AppContext()
        {
            // Iniciar la aplicación con el formulario de Login
            Login loginForm = new Login();
            loginForm.LoginSuccess += OnLoginSuccess;  // Evento cuando el Login es exitoso
            loginForm.Show();
        }

        private void OnLoginSuccess(object sender, LoginEventArgs e)
        {
            // Cerrar el formulario de Login cuando el usuario se autentica
            ((Login)sender).Close();

            // Abrir el formulario de Chat con los datos recibidos
            Chat chatForm = new Chat(e.Client, e.Stream, e.Reader, e.Writer, e.UserName);
            chatForm.FormClosed += (s, args) => ExitThread();  // Cerrar la aplicación cuando se cierre Chat
            chatForm.Show();
        }
    }
}
