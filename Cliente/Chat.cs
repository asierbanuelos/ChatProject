using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
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
        private String clientName;

        public Chat(TcpClient client, NetworkStream stream, StreamReader reader, StreamWriter writer,String ClientName)
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
                // Enviar "DISCONNECT" al servidor para indicar que el cliente se desconecta
                if (client != null && client.Connected)
                {
                    writer.WriteLine("DISCONNECT");
                    writer.Flush(); // Asegura que el mensaje se envíe de inmediato
                }

                // Cerrar el StreamReader, StreamWriter, y NetworkStream
                reader?.Close();
                writer?.Close();
                stream?.Close();

                // Cerrar el TcpClient
                client?.Close();

                this.FormClosing -= Chat_FormClosing;

                // Confirmar que todos los recursos fueron cerrados
                Console.WriteLine("Conexión cerrada correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cerrar conexión: " + ex.Message);
            }
            finally
            {
                // Cerrar el proceso si no se cierra correctamente
                Application.Exit();
            }
        }

        public void RecibirListaDeUsuarios()
        {
            try
            {
                while (client.Connected)
                {
                    // Leer la respuesta del servidor
                    string serverResponse = reader.ReadLine();

                    if (serverResponse != null && serverResponse.StartsWith("USUARIOS_ACTIVOS:"))
                    {
                        // Obtener la lista de usuarios
                        string usuariosString = serverResponse.Substring("USUARIOS_ACTIVOS:".Length);
                        string[] usuarios = usuariosString.Split(',');

                        // Llamar al método para actualizar los usuarios en la interfaz
                        ActualizarUsuariosActivos(usuarios);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al recibir lista de usuarios: " + ex.Message);
            }
        }

        public void ActualizarUsuariosActivos(string[] usuarios)
        {
            string nombreCliente = clientName;  // Ejemplo: Aquí deberías tener el nombre del cliente conectado

            // Asegurarse de que la actualización se haga en el hilo principal
            if (this.InvokeRequired)
            {
                // Usar Invoke para llamar al método en el hilo principal
                this.Invoke(new Action<string[]>(ActualizarUsuariosActivos), new object[] { usuarios });
            }
            else
            {
                // Limpiar los Labels antes de actualizarlos
                for (int i = 0; i < 15; i++)
                {
                    string labelName = "l_activo" + (i + 1);
                    var label = this.Controls.Find(labelName, true).FirstOrDefault() as Label;

                    if (label != null)
                    {
                        label.Text = ""; // Limpiar el texto de los labels
                    }
                }

                // Asignar los nuevos valores a los labels
                for (int i = 0; i < usuarios.Length && i < 15; i++)
                {
                    string labelName = "l_activo" + (i + 1);
                    var label = this.Controls.Find(labelName, true).FirstOrDefault() as Label;

                    if (label != null)
                    {
                        // Si el nombre del usuario es el mismo que el del cliente, agregar "(Yo)"
                        if (usuarios[i] == nombreCliente)
                        {
                            label.Text = usuarios[i] + " (Yo)";
                        }
                        else
                        {
                            label.Text = usuarios[i];
                        }
                    }
                    else
                    {
                        MessageBox.Show($"No se encontró el Label con el nombre {labelName}");
                    }
                }
            }
        }



    }
}
