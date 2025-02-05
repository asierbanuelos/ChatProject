namespace Cliente
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            tableLayoutPanelGlobal = new TableLayoutPanel();
            tableDelMedio = new TableLayoutPanel();
            b_login = new Button();
            l_sartuIzena = new Label();
            t_izena = new TextBox();
            logo = new Panel();
            tableLayoutPanelGlobal.SuspendLayout();
            tableDelMedio.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelGlobal
            // 
            tableLayoutPanelGlobal.AutoSize = true;
            tableLayoutPanelGlobal.BackColor = Color.Transparent;
            tableLayoutPanelGlobal.BackgroundImage = (Image)resources.GetObject("tableLayoutPanelGlobal.BackgroundImage");
            tableLayoutPanelGlobal.BackgroundImageLayout = ImageLayout.Stretch;
            tableLayoutPanelGlobal.ColumnCount = 3;
            tableLayoutPanelGlobal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanelGlobal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanelGlobal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanelGlobal.Controls.Add(tableDelMedio, 1, 1);
            tableLayoutPanelGlobal.Dock = DockStyle.Fill;
            tableLayoutPanelGlobal.Location = new Point(0, 0);
            tableLayoutPanelGlobal.Name = "tableLayoutPanelGlobal";
            tableLayoutPanelGlobal.RowCount = 3;
            tableLayoutPanelGlobal.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanelGlobal.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanelGlobal.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanelGlobal.Size = new Size(892, 558);
            tableLayoutPanelGlobal.TabIndex = 0;
            // 
            // tableDelMedio
            // 
            tableDelMedio.BackColor = SystemColors.ActiveCaption;
            tableDelMedio.ColumnCount = 1;
            tableDelMedio.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableDelMedio.Controls.Add(b_login, 0, 3);
            tableDelMedio.Controls.Add(l_sartuIzena, 0, 1);
            tableDelMedio.Controls.Add(t_izena, 0, 2);
            tableDelMedio.Controls.Add(logo, 0, 0);
            tableDelMedio.Dock = DockStyle.Fill;
            tableDelMedio.Location = new Point(359, 189);
            tableDelMedio.Name = "tableDelMedio";
            tableDelMedio.RowCount = 4;
            tableDelMedio.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            tableDelMedio.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableDelMedio.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableDelMedio.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableDelMedio.Size = new Size(172, 180);
            tableDelMedio.TabIndex = 0;
            // 
            // b_login
            // 
            b_login.Anchor = AnchorStyles.None;
            b_login.Location = new Point(3, 138);
            b_login.Name = "b_login";
            b_login.Size = new Size(166, 39);
            b_login.TabIndex = 0;
            b_login.Text = "LOGIN";
            b_login.UseVisualStyleBackColor = true;
            b_login.Click += b_login_Click;
            // 
            // l_sartuIzena
            // 
            l_sartuIzena.Anchor = AnchorStyles.None;
            l_sartuIzena.AutoSize = true;
            l_sartuIzena.Location = new Point(35, 68);
            l_sartuIzena.Name = "l_sartuIzena";
            l_sartuIzena.Size = new Size(102, 25);
            l_sartuIzena.TabIndex = 2;
            l_sartuIzena.Text = "Sartu izena:";
            l_sartuIzena.TextAlign = ContentAlignment.BottomCenter;
            // 
            // t_izena
            // 
            t_izena.Anchor = AnchorStyles.None;
            t_izena.Location = new Point(3, 102);
            t_izena.Name = "t_izena";
            t_izena.Size = new Size(166, 31);
            t_izena.TabIndex = 1;
            // 
            // logo
            // 
            logo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            logo.BackgroundImage = (Image)resources.GetObject("logo.BackgroundImage");
            logo.BackgroundImageLayout = ImageLayout.Zoom;
            logo.Location = new Point(3, 3);
            logo.Name = "logo";
            logo.Size = new Size(166, 57);
            logo.TabIndex = 3;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(892, 558);
            Controls.Add(tableLayoutPanelGlobal);
            Name = "Login";
            Text = "Fp Santurtzi Hileapandegia Login";
            tableLayoutPanelGlobal.ResumeLayout(false);
            tableDelMedio.ResumeLayout(false);
            tableDelMedio.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelGlobal;
        private TableLayoutPanel tableDelMedio;
        private Button b_login;
        private Label l_sartuIzena;
        private TextBox t_izena;
        private Panel logo;
    }
}
