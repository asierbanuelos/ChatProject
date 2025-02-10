namespace Cliente
{
    public partial class Mensaje : UserControl
    {
        public Mensaje(string nombre, string mensaje, bool esMio)
        {
            InitializeComponent();
            lblNombre.Text = nombre;
            lblMensaje.Text = mensaje;
            lblHora.Text = DateTime.Now.ToString("HH:mm");

            // Cambiar el color y la alineación
            if (esMio)
            {
                this.BackColor = Color.LightGreen;
                lblNombre.TextAlign = ContentAlignment.MiddleRight;
                lblMensaje.TextAlign = ContentAlignment.MiddleRight;
                lblHora.TextAlign = ContentAlignment.MiddleRight;
            }
            else
            {
                this.BackColor = Color.LightGray;
                lblNombre.TextAlign = ContentAlignment.MiddleLeft;
                lblMensaje.TextAlign = ContentAlignment.MiddleLeft;
                lblHora.TextAlign = ContentAlignment.MiddleLeft;
            }

            // Establecer un ancho fijo para el mensaje
            this.Width = 350;  // Ajuste el ancho del mensaje (puedes personalizar este valor)

            // Asegurarse de que el mensaje no se expanda horizontalmente
            lblMensaje.MaximumSize = new Size(this.Width, 0); // Limitar el tamaño del texto solo en horizontal
            lblMensaje.AutoSize = false;  // Impide que el tamaño se ajuste automáticamente en ancho, pero sí en altura

            // Ajustar la altura según el contenido del mensaje
            lblMensaje.Height = lblMensaje.PreferredHeight; // Ajustar la altura de lblMensaje según su contenido

            // Ajustar la altura total del control
            this.Height = lblNombre.Height + lblMensaje.Height + lblHora.Height + 10; // Ajuste de altura con márgenes
        }

        public void AgregarMensaje(FlowLayoutPanel panel)
        {
            // Crear un contenedor para cada mensaje
            Panel contenedor = new Panel();
            contenedor.AutoSize = true;  // El contenedor se ajusta al tamaño del mensaje
            contenedor.MaximumSize = new Size(panel.ClientSize.Width, 0);  // Evitar que el mensaje se expanda más allá del panel

            // Establecer márgenes para la alineación de los mensajes
            if (this.BackColor == Color.LightGreen) // Mensaje propio
            {
                contenedor.Margin = new Padding(panel.ClientSize.Width - this.Width, 0, 0, 0);  // Alinear a la derecha
            }
            else // Mensaje de otro usuario
            {
                contenedor.Margin = new Padding(0, 0, panel.ClientSize.Width - this.Width, 0);  // Alinear a la izquierda
            }

            // Agregar el mensaje al contenedor
            contenedor.Controls.Add(this);

            // Agregar el contenedor al FlowLayoutPanel
            panel.Controls.Add(contenedor);

            // Desplazar el panel para que el último mensaje sea visible
            panel.ScrollControlIntoView(this);

            // Desactivar el scroll horizontal en el FlowLayoutPanel
            panel.AutoScroll = true;
            panel.HorizontalScroll.Visible = false;  // Desactivar la barra de desplazamiento horizontal
        }
    }

}
