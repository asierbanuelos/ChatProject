namespace ChatProject
{
    partial class ChatGeneral
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }



        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatGeneral));
            panel1 = new Panel();
            lstUsuarios = new ListBox();
            panel2 = new Panel();
            pictureBox2 = new PictureBox();
            button1 = new Button();
            pictureBox1 = new PictureBox();
            txtMensaje = new TextBox();
            notifyIcon1 = new NotifyIcon(components);
            btnEnviar = new PictureBox();
            panel3 = new Panel();
            btnCerrar = new Button();
            label1 = new Label();
            lstMensajes = new ListBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnEnviar).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(lstUsuarios);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(0, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(395, 726);
            panel1.TabIndex = 0;
            // 
            // lstUsuarios
            // 
            lstUsuarios.FormattingEnabled = true;
            lstUsuarios.Location = new Point(23, 145);
            lstUsuarios.Name = "lstUsuarios";
            lstUsuarios.Size = new Size(335, 444);
            lstUsuarios.TabIndex = 5;
            // 
            // panel2
            // 
            panel2.Controls.Add(pictureBox2);
            panel2.Location = new Point(401, 54);
            panel2.Name = "panel2";
            panel2.Size = new Size(1279, 93);
            panel2.TabIndex = 4;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(422, 40);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(0, 0);
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // button1
            // 
            button1.BackColor = Color.Teal;
            button1.Font = new Font("Gabriola", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(1, 645);
            button1.Name = "button1";
            button1.Size = new Size(394, 80);
            button1.TabIndex = 1;
            button1.Text = "Gaurko Zitak";
            button1.UseVisualStyleBackColor = false;
            button1.UseWaitCursor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(108, 24);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(174, 84);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // txtMensaje
            // 
            txtMensaje.BackColor = Color.Gray;
            txtMensaje.BorderStyle = BorderStyle.None;
            txtMensaje.Location = new Point(430, 658);
            txtMensaje.Multiline = true;
            txtMensaje.Name = "txtMensaje";
            txtMensaje.Size = new Size(1196, 48);
            txtMensaje.TabIndex = 1;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            // 
            // btnEnviar
            // 
            btnEnviar.BackColor = SystemColors.ActiveCaptionText;
            btnEnviar.Image = (Image)resources.GetObject("btnEnviar.Image");
            btnEnviar.Location = new Point(1586, 658);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Padding = new Padding(5, 10, 5, 5);
            btnEnviar.Size = new Size(40, 48);
            btnEnviar.TabIndex = 3;
            btnEnviar.TabStop = false;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.DarkCyan;
            panel3.Controls.Add(btnCerrar);
            panel3.Controls.Add(label1);
            panel3.Location = new Point(396, 1);
            panel3.Name = "panel3";
            panel3.Size = new Size(1270, 94);
            panel3.TabIndex = 4;
            // 
            // btnCerrar
            // 
            btnCerrar.Location = new Point(1134, 24);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(108, 41);
            btnCerrar.TabIndex = 1;
            btnCerrar.Text = "btnCerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Gabriola", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(34, 24);
            label1.Name = "label1";
            label1.Size = new Size(195, 55);
            label1.TabIndex = 0;
            label1.Text = "CHAT GENERAL";
            // 
            // lstMensajes
            // 
            lstMensajes.FormattingEnabled = true;
            lstMensajes.Location = new Point(399, 95);
            lstMensajes.Name = "lstMensajes";
            lstMensajes.Size = new Size(1269, 524);
            lstMensajes.TabIndex = 5;
            // 
            // ChatGeneral
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1662, 727);
            Controls.Add(lstMensajes);
            Controls.Add(panel3);
            Controls.Add(btnEnviar);
            Controls.Add(txtMensaje);
            Controls.Add(panel1);
            Name = "ChatGeneral";
            Text = "Form1";
            Load += ChatGeneral_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnEnviar).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox1;
        private Button button1;
        private TextBox txtMensaje;
        private NotifyIcon notifyIcon1;
        private PictureBox pictureBox2;
        private PictureBox btnEnviar;
        private Panel panel2;
        private Panel panel3;
        private Label label1;
        private ListBox lstUsuarios;
        private ListBox lstMensajes;
        private Button btnCerrar;
    }
}