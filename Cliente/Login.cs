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
                // Verificar si la respuesta contiene "Conectado-" y extraer el nombre
                if (serverResponse.StartsWith("Conectado-"))
                {
                    string[] parts = serverResponse.Split('-');  // Dividir la respuesta
                    string serverName = parts[1];  // Obtener el nombre del cliente desde la respuesta

                    // Si la conexión es exitosa, cerrar el formulario de Login
                    this.Hide();

                    // Crear y mostrar el formulario de Chat, pasando el nombre del cliente
                    Chat chatForm = new Chat(client, stream, reader, writer, serverName);
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
            // Asegurarse de cerrar todo correctamente
            try
            {
                if (client != null && client.Connected)
                {
                    writer?.WriteLine("DISCONNECT");  // Intentar enviar mensaje de desconexión
                    writer?.Flush();  // Asegurarse de que se haya enviado
                    reader?.Close();  // Cerrar el lector
                    writer?.Close();  // Cerrar el escritor
                    stream?.Close();  // Cerrar el NetworkStream
                    client?.Close();  // Cerrar el TcpClient
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cerrar conexión: {ex.Message}");
            }
            finally
            {
                // Asegurarse de que la aplicación se cierre correctamente
                Application.Exit();
            }
        }
    }
}
