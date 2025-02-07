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

            // Ajustar color y alineación según quién envía el mensaje
            if (esMio)
            {
                this.BackColor = Color.LightGreen;  // Fondo verde para los mensajes enviados por el usuario
                lblNombre.BackColor = Color.LightGreen;
                lblMensaje.BackColor = Color.LightGreen;
                lblHora.BackColor = Color.LightGreen;

                lblNombre.TextAlign = ContentAlignment.MiddleRight;
                lblMensaje.TextAlign = ContentAlignment.MiddleRight;
                lblHora.TextAlign = ContentAlignment.MiddleRight;

                this.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            }
            else
            {
                this.BackColor = Color.LightGray;  // Fondo gris para los mensajes recibidos
                lblNombre.BackColor = Color.LightGray;
                lblMensaje.BackColor = Color.LightGray;
                lblHora.BackColor = Color.LightGray;

                lblNombre.TextAlign = ContentAlignment.MiddleLeft;
                lblMensaje.TextAlign = ContentAlignment.MiddleLeft;
                lblHora.TextAlign = ContentAlignment.MiddleLeft;

                this.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            }

            // El evento Load ya está suscrito en el diseñador, no es necesario hacerlo aquí.
        }

        private void Mensaje_Load(object sender, EventArgs e)
        {
            AjustarTamaño();
        }

        private void AjustarTamaño()
        {
            // Verificar que el parent no sea null antes de acceder a ClientSize
            if (this.Parent != null)
            {
                // Establecer el tamaño del control para que ocupe todo el ancho del panel
                this.Width = this.Parent.ClientSize.Width; // Ocupa todo el ancho del panel

                // Ajustar la altura del control basado en el contenido
                lblMensaje.MaximumSize = new Size(this.Width - 20, 0); // Aseguramos que el mensaje no exceda el ancho del panel (con margen)
                lblMensaje.AutoSize = true;

                // Ajustar el tamaño del control de mensaje en altura
                this.Height = lblNombre.Height + lblMensaje.Height + lblHora.Height + 10; // Ajuste del alto del control
            }
        }
    }
}
