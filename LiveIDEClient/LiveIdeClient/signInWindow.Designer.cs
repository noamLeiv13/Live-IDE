namespace liveIde
{
    partial class signInWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(signInWindow));
            this.topPanel = new System.Windows.Forms.Panel();
            this.signInPanel = new Bunifu.Framework.UI.BunifuGradientPanel();
            this.errorMsgLabel = new System.Windows.Forms.Label();
            this.SignUpLabel = new System.Windows.Forms.Label();
            this.loginButton = new Bunifu.Framework.UI.BunifuFlatButton();
            this.password = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.userName = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.loginLabel = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.personPIctureBox = new System.Windows.Forms.PictureBox();
            this.closeButton = new Bunifu.Framework.UI.BunifuImageButton();
            this.topPanel.SuspendLayout();
            this.signInPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.personPIctureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(121)))), ((int)(((byte)(225)))));
            this.topPanel.Controls.Add(this.signInPanel);
            this.topPanel.Controls.Add(this.closeButton);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(483, 437);
            this.topPanel.TabIndex = 0;
            // 
            // signInPanel
            // 
            this.signInPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("signInPanel.BackgroundImage")));
            this.signInPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.signInPanel.Controls.Add(this.errorMsgLabel);
            this.signInPanel.Controls.Add(this.SignUpLabel);
            this.signInPanel.Controls.Add(this.loginButton);
            this.signInPanel.Controls.Add(this.password);
            this.signInPanel.Controls.Add(this.userName);
            this.signInPanel.Controls.Add(this.loginLabel);
            this.signInPanel.Controls.Add(this.personPIctureBox);
            this.signInPanel.GradientBottomLeft = System.Drawing.Color.White;
            this.signInPanel.GradientBottomRight = System.Drawing.Color.White;
            this.signInPanel.GradientTopLeft = System.Drawing.Color.White;
            this.signInPanel.GradientTopRight = System.Drawing.Color.White;
            this.signInPanel.Location = new System.Drawing.Point(52, 12);
            this.signInPanel.Name = "signInPanel";
            this.signInPanel.Quality = 10;
            this.signInPanel.Size = new System.Drawing.Size(388, 413);
            this.signInPanel.TabIndex = 8;
            // 
            // errorMsgLabel
            // 
            this.errorMsgLabel.AutoSize = true;
            this.errorMsgLabel.BackColor = System.Drawing.Color.White;
            this.errorMsgLabel.Location = new System.Drawing.Point(15, 280);
            this.errorMsgLabel.Name = "errorMsgLabel";
            this.errorMsgLabel.Size = new System.Drawing.Size(35, 13);
            this.errorMsgLabel.TabIndex = 8;
            this.errorMsgLabel.Text = "label1";
            // 
            // SignUpLabel
            // 
            this.SignUpLabel.AutoSize = true;
            this.SignUpLabel.BackColor = System.Drawing.Color.White;
            this.SignUpLabel.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SignUpLabel.Location = new System.Drawing.Point(160, 246);
            this.SignUpLabel.Name = "SignUpLabel";
            this.SignUpLabel.Size = new System.Drawing.Size(72, 22);
            this.SignUpLabel.TabIndex = 7;
            this.SignUpLabel.Text = "Signup";
            this.SignUpLabel.Click += new System.EventHandler(this.SignUpLabel_Click);
            // 
            // loginButton
            // 
            this.loginButton.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.loginButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.loginButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.loginButton.BorderRadius = 0;
            this.loginButton.ButtonText = "Login";
            this.loginButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginButton.DisabledColor = System.Drawing.Color.Gray;
            this.loginButton.Iconcolor = System.Drawing.Color.Transparent;
            this.loginButton.Iconimage = null;
            this.loginButton.Iconimage_right = null;
            this.loginButton.Iconimage_right_Selected = null;
            this.loginButton.Iconimage_Selected = null;
            this.loginButton.IconMarginLeft = 0;
            this.loginButton.IconMarginRight = 0;
            this.loginButton.IconRightVisible = false;
            this.loginButton.IconRightZoom = 0D;
            this.loginButton.IconVisible = true;
            this.loginButton.IconZoom = 90D;
            this.loginButton.IsTab = false;
            this.loginButton.Location = new System.Drawing.Point(122, 327);
            this.loginButton.Name = "loginButton";
            this.loginButton.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.loginButton.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.loginButton.OnHoverTextColor = System.Drawing.Color.White;
            this.loginButton.selected = false;
            this.loginButton.Size = new System.Drawing.Size(142, 38);
            this.loginButton.TabIndex = 4;
            this.loginButton.Text = "Login";
            this.loginButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.loginButton.Textcolor = System.Drawing.Color.White;
            this.loginButton.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // password
            // 
            this.password.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.password.BackColor = System.Drawing.Color.White;
            this.password.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.password.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.password.HintForeColor = System.Drawing.Color.Empty;
            this.password.HintText = "Password";
            this.password.isPassword = true;
            this.password.LineFocusedColor = System.Drawing.Color.Blue;
            this.password.LineIdleColor = System.Drawing.Color.Gray;
            this.password.LineMouseHoverColor = System.Drawing.Color.Gray;
            this.password.LineThickness = 4;
            this.password.Location = new System.Drawing.Point(18, 176);
            this.password.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(337, 54);
            this.password.TabIndex = 3;
            this.password.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.password.OnValueChanged += new System.EventHandler(this.password_OnValueChanged);
            // 
            // userName
            // 
            this.userName.BackColor = System.Drawing.Color.White;
            this.userName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.userName.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.userName.HintForeColor = System.Drawing.Color.Empty;
            this.userName.HintText = "User Name";
            this.userName.isPassword = false;
            this.userName.LineFocusedColor = System.Drawing.Color.Blue;
            this.userName.LineIdleColor = System.Drawing.Color.Gray;
            this.userName.LineMouseHoverColor = System.Drawing.Color.Gray;
            this.userName.LineThickness = 4;
            this.userName.Location = new System.Drawing.Point(18, 112);
            this.userName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(337, 54);
            this.userName.TabIndex = 2;
            this.userName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // loginLabel
            // 
            this.loginLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loginLabel.AutoSize = true;
            this.loginLabel.BackColor = System.Drawing.Color.White;
            this.loginLabel.Font = new System.Drawing.Font("Segoe UI Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.loginLabel.Location = new System.Drawing.Point(148, 67);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(84, 40);
            this.loginLabel.TabIndex = 1;
            this.loginLabel.Text = "Login";
            // 
            // personPIctureBox
            // 
            this.personPIctureBox.BackColor = System.Drawing.Color.White;
            this.personPIctureBox.Image = global::liveIde.Properties.Resources.person;
            this.personPIctureBox.Location = new System.Drawing.Point(132, 14);
            this.personPIctureBox.Name = "personPIctureBox";
            this.personPIctureBox.Size = new System.Drawing.Size(100, 50);
            this.personPIctureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.personPIctureBox.TabIndex = 0;
            this.personPIctureBox.TabStop = false;
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(121)))), ((int)(((byte)(225)))));
            this.closeButton.Image = global::liveIde.Properties.Resources.closeBlack;
            this.closeButton.ImageActive = null;
            this.closeButton.Location = new System.Drawing.Point(0, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(46, 47);
            this.closeButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.closeButton.TabIndex = 7;
            this.closeButton.TabStop = false;
            this.closeButton.Zoom = 10;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // signInWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(483, 437);
            this.ControlBox = false;
            this.Controls.Add(this.topPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "signInWindow";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "signInWindows";
            this.Load += new System.EventHandler(this.signInWindow_Load);
            this.topPanel.ResumeLayout(false);
            this.signInPanel.ResumeLayout(false);
            this.signInPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.personPIctureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private Bunifu.Framework.UI.BunifuImageButton closeButton;
        private Bunifu.Framework.UI.BunifuGradientPanel signInPanel;
        private System.Windows.Forms.Label SignUpLabel;
        private Bunifu.Framework.UI.BunifuFlatButton loginButton;
        private Bunifu.Framework.UI.BunifuMaterialTextbox password;
        private Bunifu.Framework.UI.BunifuMaterialTextbox userName;
        private Bunifu.Framework.UI.BunifuCustomLabel loginLabel;
        private System.Windows.Forms.PictureBox personPIctureBox;
        private System.Windows.Forms.Label errorMsgLabel;
    }
}