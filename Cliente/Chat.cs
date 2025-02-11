using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

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

        public Chat(TcpClient client, NetworkStream stream, StreamReader reader, StreamWriter writer, string clientName)
        {
            InitializeComponent();

            this.client = client;
            this.stream = stream;
            this.reader = reader;
            this.writer = writer;
            this.clientName = clientName;
            this.FormClosing += Chat_FormClosing;

            this.t_mensaje.KeyDown += new System.Windows.Forms.KeyEventHandler(this.t_mensaje_KeyDown);
            this.b_actualizar.Click += new System.EventHandler(this.b_actualizar_Click);


            // Iniciar la tarea para recibir mensajes del servidor
            Task.Run(() => RecibirMensajesServidor());
        }

        private void Chat_FormClosing(object sender, FormClosingEventArgs e)
        {
            EnviarDesconexion();
            CerrarConexion();
            Application.Exit(); // Cierra la aplicación completamente
        }

        private void b_desconectar_Click(object sender, EventArgs e)
        {
            EnviarDesconexion();
            CerrarConexion();
            Application.Restart(); // Reinicia la aplicación (para volver a login)
        }

        /// <summary>
        /// Envía un mensaje de desconexión al servidor.
        /// </summary>
        private void EnviarDesconexion()
        {
            if (client != null && client.Connected)
            {
                try
                {
                    writer.WriteLine($"DISC: {clientName}");
                    writer.Flush();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al enviar desconexión: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Cierra todos los recursos de la conexión.
        /// </summary>
        private void CerrarConexion()
        {
            isRunning = false;
            reader?.Close();
            writer?.Close();
            stream?.Close();
            client?.Close();
        }

        /// <summary>
        /// Escucha los mensajes del servidor y los procesa.
        /// </summary>
        private void RecibirMensajesServidor()
        {
            try
            {
                while (isRunning && client.Connected)
                {
                    string serverMessage = reader.ReadLine();
                    if (!isRunning) break;

                    if (!string.IsNullOrEmpty(serverMessage))
                    {
                        ProcesarMensaje(serverMessage); // Procesar todos los tipos de mensajes aquí
                    }
                }
            }
            catch (Exception ex)
            {
                if (isRunning)
                {
                    MessageBox.Show("Error al recibir datos del servidor: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Procesa los mensajes recibidos del servidor.
        /// </summary>
        private void ProcesarMensaje(string message)
        {
            if (message.StartsWith("USUARIOS_ACTIVOS:"))
            {
                string usuariosString = message.Substring("USUARIOS_ACTIVOS:".Length);
                string[] usuarios = usuariosString.Split(',');
                ActualizarUsuariosActivos(usuarios);
            }
            else if (message.StartsWith("MSG:"))
            {
                string[] parts = message.Split(new[] { ':' }, 3); // Separar en 3 partes máximo
                if (parts.Length == 3)
                {
                    string senderName = parts[1];
                    string chatMessage = parts[2];
                    MostrarMensaje(senderName, chatMessage);
                }
            }
            else if (message.StartsWith("API:"))
            {
                // Obtener las citas desde el mensaje (elimina "API:" al principio)
                string citasJson = message.Substring("API:".Length);
                List<dynamic> citas = JsonConvert.DeserializeObject<List<dynamic>>(citasJson);

                // Definir el formato esperado de la hora
                string formatoHora = "yyyy-MM-dd HH:mm:ss";  // Ajusta este formato según la API

                // Ordenar las citas por hora de inicio (hasieraOrdua), de forma descendente
                var citasOrdenadas = citas.OrderByDescending(cita =>
                {
                    DateTime horaInicio;
                    if (DateTime.TryParseExact(cita.hasieraOrdua.ToString(), formatoHora, null, System.Globalization.DateTimeStyles.None, out horaInicio))
                    {
                        return horaInicio;
                    }
                    else
                    {
                        // En caso de que la fecha no sea válida, devuelve un valor predeterminado (como la fecha más lejana posible)
                        return DateTime.MinValue;
                    }
                }).ToList();

                // Verificar si necesitamos ejecutar el código en el hilo principal
                if (listBoxCitas.InvokeRequired)
                {
                    // Si estamos en un hilo secundario, usamos Invoke para ejecutar el cambio en el hilo principal
                    listBoxCitas.Invoke(new Action(() =>
                    {
                        // Limpiar el ListBox antes de mostrar nuevas citas
                        listBoxCitas.Items.Clear();

                        // Añadir las citas ordenadas al ListBox
                        foreach (var cita in citasOrdenadas)
                        {
                            string citaTexto = $"{cita.izena} - {cita.hasieraOrdua} a {cita.amaieraOrdua}";
                            listBoxCitas.Items.Add(citaTexto);
                        }
                    }));
                }
                else
                {
                    // Si estamos en el hilo principal, podemos actualizar directamente el ListBox
                    listBoxCitas.Items.Clear();
                    foreach (var cita in citasOrdenadas)
                    {
                        string citaTexto = $"{cita.izena} - {cita.hasieraOrdua} a {cita.amaieraOrdua}";
                        listBoxCitas.Items.Add(citaTexto);
                    }
                }
            }


        }

        /// <summary>
        /// Actualiza la lista de usuarios conectados.
        /// </summary>
        private void ActualizarUsuariosActivos(string[] usuarios)
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

        /// <summary>
        /// Envía un mensaje de chat al servidor.
        /// </summary>
        private void EnviarMensaje(string mensaje)
        {
            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                try
                {
                    t_mensaje.Clear();
                    string mensajeFormato = $"MSG:{clientName}:{mensaje}";
                    writer.WriteLine(mensajeFormato);
                    writer.Flush();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al enviar mensaje: " + ex.Message);
                }
            }
        }


        private int posicionY = 10;  // Posición inicial para el primer mensaje

        /// <summary>
        /// Muestra el mensaje en la interfaz de usuario.
        /// </summary>
        private void MostrarMensaje(string sender, string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string, string>(MostrarMensaje), new object[] { sender, message });
            }
            else
            {
                bool esMio = sender == clientName;
                Mensaje nuevoMensaje = new Mensaje(sender, message, esMio);

                nuevoMensaje.AgregarMensaje(p_central);  // Agregar el mensaje al FlowLayoutPanel
            }
        }

        /// <summary>
        /// Permite enviar mensajes con la tecla Enter.
        /// </summary>
        private void t_mensaje_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EnviarMensaje(t_mensaje.Text); // Ahora pasa el texto del mensaje
            }
        }

        private void b_mandar_Click_1(object sender, EventArgs e)
        {
            EnviarMensaje(t_mensaje.Text);
        }

        private void b_actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                writer.WriteLine("API:"); // Envía la solicitud al servidor
                writer.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al solicitar actualización: " + ex.Message);
            }
        }
    }
}
