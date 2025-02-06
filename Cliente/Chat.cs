using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente
{
    public partial class Chat : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;
        private string clientName;
        private bool isRunning = true; // Controla si el hilo debe seguir ejecutándose

        public Chat(TcpClient client, NetworkStream stream, StreamReader reader, StreamWriter writer, string ClientName)
        {
            InitializeComponent();

            this.client = client;
            this.stream = stream;
            this.reader = reader;
            this.writer = writer;
            this.clientName = ClientName;
            this.FormClosing += Chat_FormClosing;

            // Iniciar la tarea para recibir la lista de usuarios
            Task.Run(() => RecibirListaDeUsuarios());
        }

        private void Chat_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                isRunning = false; // Detiene el bucle de lectura

                // Enviar "DISCONNECT" al servidor
                if (client != null && client.Connected)
                {
                    writer.WriteLine("DISCONNECT");
                    writer.Flush();
                }

                // Cerrar recursos en orden
                reader?.Close();
                writer?.Close();
                stream?.Close();
                client?.Close();

                Console.WriteLine("Conexión cerrada correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cerrar conexión: " + ex.Message);
            }
            finally
            {
                Application.Exit(); // Asegurar que la aplicación se cierre
            }
        }

        public void RecibirListaDeUsuarios()
        {
            try
            {
                while (isRunning && client.Connected)
                {
                    string serverResponse = reader.ReadLine();

                    if (!isRunning) break; // Salir si el chat se está cerrando

                    if (serverResponse != null && serverResponse.StartsWith("USUARIOS_ACTIVOS:"))
                    {
                        string usuariosString = serverResponse.Substring("USUARIOS_ACTIVOS:".Length);
                        string[] usuarios = usuariosString.Split(',');

                        ActualizarUsuariosActivos(usuarios);
                    }
                }
            }
            catch (Exception ex)
            {
                if (isRunning) // Solo mostrar error si el chat no se estaba cerrando
                {
                    MessageBox.Show("Error al recibir lista de usuarios: " + ex.Message);
                }
            }
        }

        public void ActualizarUsuariosActivos(string[] usuarios)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string[]>(ActualizarUsuariosActivos), new object[] { usuarios });
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    string labelName = "l_activo" + (i + 1);
                    var label = this.Controls.Find(labelName, true).FirstOrDefault() as Label;
                    if (label != null) label.Text = "";
                }

                for (int i = 0; i < usuarios.Length && i < 15; i++)
                {
                    string labelName = "l_activo" + (i + 1);
                    var label = this.Controls.Find(labelName, true).FirstOrDefault() as Label;
                    if (label != null)
                    {
                        label.Text = (usuarios[i] == clientName) ? usuarios[i] + " (Yo)" : usuarios[i];
                    }
                }
            }
        }

        private void b_desconectar_Click(object sender, EventArgs e)
        {
            try
            {
                isRunning = false; // Detiene el bucle de lectura

                // Enviar "DISCONNECT" al servidor
                if (client != null && client.Connected)
                {
                    writer.WriteLine("DISCONNECT");
                    writer.Flush();
                }

                // Cerrar recursos en orden
                reader?.Close();
                writer?.Close();
                stream?.Close();
                client?.Close();

                Console.WriteLine("Conexión cerrada correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cerrar conexión: " + ex.Message);
            }

            Application.Restart();  // Esto cerrará toda la aplicación y volverá a iniciar el formulario de Login
        }


    }
}
