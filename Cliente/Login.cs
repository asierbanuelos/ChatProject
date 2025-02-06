using System;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Cliente
{
    public partial class Login : Form
    {
        public event EventHandler<LoginEventArgs> LoginSuccess;  // Evento para indicar login exitoso

        private TcpClient client;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;

        public Login()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            // Agregar el evento KeyDown al campo de texto (t_izena)
            t_izena.KeyDown += new KeyEventHandler(t_izena_KeyDown);
        }
        private void t_izena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                b_login_Click(sender, e); // Llama al código del botón al presionar Enter
            }
        }
        private void b_login_Click(object sender, EventArgs e)
        {
            string clientName = t_izena.Text.Trim();

            if (string.IsNullOrEmpty(clientName))
            {
                MessageBox.Show("Por favor, ingrese un nombre.");
                return;
            }

            try
            {
                client = new TcpClient("127.0.0.1", 13000);
                stream = client.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream) { AutoFlush = true };

                writer.WriteLine(clientName);
                string serverResponse = reader.ReadLine();

                if (serverResponse.StartsWith("Conectado-"))
                {
                    string[] parts = serverResponse.Split('-');
                    string serverName = parts[1];

                    this.Hide(); // Ocultar el formulario en lugar de cerrarlo
                    LoginSuccess?.Invoke(this, new LoginEventArgs(client, stream, reader, writer, serverName));
                }
                else
                {
                    MessageBox.Show(serverResponse);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar al servidor: {ex.Message}");
            }
        }
    }

    public class LoginEventArgs : EventArgs
    {
        public TcpClient Client { get; }
        public NetworkStream Stream { get; }
        public StreamReader Reader { get; }
        public StreamWriter Writer { get; }
        public string UserName { get; }

        public LoginEventArgs(TcpClient client, NetworkStream stream, StreamReader reader, StreamWriter writer, string userName)
        {
            Client = client;
            Stream = stream;
            Reader = reader;
            Writer = writer;
            UserName = userName;
        }
    }
}
