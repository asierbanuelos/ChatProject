namespace ChatProject
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Configurar el radio del borde
            int radius = 20;

            // Crear un rectángulo con las dimensiones del panel
            Rectangle rect = new Rectangle(0, 0, panel1.Width - 1, panel1.Height - 1);

            // Crear la forma redondeada
            using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                path.CloseFigure();

                // Dibujar el borde
                using (Pen pen = new Pen(Color.Black, 2)) // Cambia el color y grosor si deseas
                {
                    g.DrawPath(pen, path);
                }

                // Aplicar la región redondeada al panel
                panel1.Region = new Region(path);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChatGeneral chatGeneral = new ChatGeneral();
            chatGeneral.Show();



        }
    }
}

