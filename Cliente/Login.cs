using System;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Cliente
{
    public partial class Login : Form
    {
        private TcpClient client;
        private NetworkStream stream;  // Corregido nombre de la variable
        private StreamReader reader;   // Corregido nombre de la variable
        private StreamWriter writer;   // Corregido nombre de la variable

        public Login()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;  // Maximizar la ventana
        }

        // Evento cuando el cliente hace clic en el botón de login
        private void b_login_Click(object sender, EventArgs e)
        {
            string clientName = t_izena.Text.Trim();  // Obtener nombre desde el TextBox

            // Comprobar si el nombre está vacío
            if (string.IsNullOrEmpty(clientName))
            {
                MessageBox.Show("Por favor, ingrese un nombre.");
                return;
            }

            try
            {
                // Intentar conectar con el servidor
                client = new TcpClient("127.0.0.1", 13000);  // Conectar al servidor en localhost y puerto 13000
                stream = client.GetStream();   // Obtener el flujo de red
                reader = new StreamReader(stream);  // Crear el lector para el flujo
                writer = new StreamWriter(stream) { AutoFlush = true };  // Crear el escritor para el flujo, habilitar AutoFlush

                // Enviar el nombre del cliente al servidor
                writer.WriteLine(clientName);

                // Leer la respuesta del servidor
                string serverResponse = reader.ReadLine();

                // Verificar si la respuesta es "Conectado"
                if (serverResponse == "Conectado")
                {
                    // Si la conexión es exitosa, cerrar el formulario de Login
                    this.Hide();

                    // Crear y mostrar el formulario de Chat
                    Chat chatForm = new Chat(client, stream, reader, writer);
                    chatForm.Show();
                }
                else
                {
                    // Si la respuesta es diferente, mostrar el error y no cerrar el formulario de Login
                    MessageBox.Show(serverResponse);
                }
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error si no se puede conectar
                MessageBox.Show($"Error al conectar al servidor: {ex.Message}");
            }
        }

        // Evento cuando el formulario de Login se cierra
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null && client.Connected)
            {
                client.Close();  // Cerrar la conexión si está abierta
            }
        }
    }
}
