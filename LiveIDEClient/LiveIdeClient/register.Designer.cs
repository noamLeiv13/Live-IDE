namespace liveIde
{
    partial class register
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
            this.userNameTextBox = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.PasswordTextBox = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.signUpButton = new Bunifu.Framework.UI.BunifuFlatButton();
            this.errorLabel = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.BackButton = new Bunifu.Framework.UI.BunifuImageButton();
            this.UserNamePicture = new System.Windows.Forms.PictureBox();
            this.userNameImage = new System.Windows.Forms.PictureBox();
            this.logoPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.BackButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserNamePicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userNameImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.userNameTextBox.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.userNameTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.userNameTextBox.HintForeColor = System.Drawing.Color.White;
            this.userNameTextBox.HintText = "User Name";
            this.userNameTextBox.isPassword = false;
            this.userNameTextBox.LineFocusedColor = System.Drawing.Color.Blue;
            this.userNameTextBox.LineIdleColor = System.Drawing.Color.Gray;
            this.userNameTextBox.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.userNameTextBox.LineThickness = 3;
            this.userNameTextBox.Location = new System.Drawing.Point(142, 223);
            this.userNameTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(238, 44);
            this.userNameTextBox.TabIndex = 1;
            this.userNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PasswordTextBox.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.PasswordTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PasswordTextBox.HintForeColor = System.Drawing.Color.White;
            this.PasswordTextBox.HintText = "Password";
            this.PasswordTextBox.isPassword = false;
            this.PasswordTextBox.LineFocusedColor = System.Drawing.Color.Blue;
            this.PasswordTextBox.LineIdleColor = System.Drawing.Color.Gray;
            this.PasswordTextBox.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.PasswordTextBox.LineThickness = 3;
            this.PasswordTextBox.Location = new System.Drawing.Point(142, 302);
            this.PasswordTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(238, 44);
            this.PasswordTextBox.TabIndex = 2;
            this.PasswordTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.PasswordTextBox.OnValueChanged += new System.EventHandler(this.PasswordTextBox_OnValueChanged);
            // 
            // signUpButton
            // 
            this.signUpButton.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(25)))), ((int)(((byte)(30)))));
            this.signUpButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(25)))), ((int)(((byte)(30)))));
            this.signUpButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.signUpButton.BorderRadius = 0;
            this.signUpButton.ButtonText = "signUp";
            this.signUpButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.signUpButton.DisabledColor = System.Drawing.Color.Gray;
            this.signUpButton.Iconcolor = System.Drawing.Color.Transparent;
            this.signUpButton.Iconimage = null;
            this.signUpButton.Iconimage_right = null;
            this.signUpButton.Iconimage_right_Selected = null;
            this.signUpButton.Iconimage_Selected = null;
            this.signUpButton.IconMarginLeft = 0;
            this.signUpButton.IconMarginRight = 0;
            this.signUpButton.IconRightVisible = true;
            this.signUpButton.IconRightZoom = 0D;
            this.signUpButton.IconVisible = true;
            this.signUpButton.IconZoom = 90D;
            this.signUpButton.IsTab = false;
            this.signUpButton.Location = new System.Drawing.Point(139, 353);
            this.signUpButton.Name = "signUpButton";
            this.signUpButton.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(25)))), ((int)(((byte)(30)))));
            this.signUpButton.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.signUpButton.OnHoverTextColor = System.Drawing.Color.White;
            this.signUpButton.selected = false;
            this.signUpButton.Size = new System.Drawing.Size(241, 48);
            this.signUpButton.TabIndex = 5;
            this.signUpButton.Text = "signUp";
            this.signUpButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.signUpButton.Textcolor = System.Drawing.Color.White;
            this.signUpButton.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signUpButton.Click += new System.EventHandler(this.signUpButton_Click);
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.ForeColor = System.Drawing.Color.White;
            this.errorLabel.Location = new System.Drawing.Point(47, 417);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(54, 13);
            this.errorLabel.TabIndex = 6;
            this.errorLabel.Text = "errorLabel";
            // 
            // BackButton
            // 
            this.BackButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(25)))), ((int)(((byte)(30)))));
            this.BackButton.ErrorImage = null;
            this.BackButton.Image = global::liveIde.Properties.Resources.leftArrow;
            this.BackButton.ImageActive = null;
            this.BackButton.Location = new System.Drawing.Point(1, 0);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(74, 49);
            this.BackButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BackButton.TabIndex = 7;
            this.BackButton.TabStop = false;
            this.BackButton.Zoom = 10;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // UserNamePicture
            // 
            this.UserNamePicture.Image = global::liveIde.Properties.Resources.password;
            this.UserNamePicture.Location = new System.Drawing.Point(60, 302);
            this.UserNamePicture.Name = "UserNamePicture";
            this.UserNamePicture.Size = new System.Drawing.Size(75, 50);
            this.UserNamePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.UserNamePicture.TabIndex = 4;
            this.UserNamePicture.TabStop = false;
            // 
            // userNameImage
            // 
            this.userNameImage.Image = global::liveIde.Properties.Resources.Username;
            this.userNameImage.Location = new System.Drawing.Point(60, 223);
            this.userNameImage.Name = "userNameImage";
            this.userNameImage.Size = new System.Drawing.Size(75, 50);
            this.userNameImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.userNameImage.TabIndex = 3;
            this.userNameImage.TabStop = false;
            // 
            // logoPicture
            // 
            this.logoPicture.Image = global::liveIde.Properties.Resources.LiveIdeLogo;
            this.logoPicture.Location = new System.Drawing.Point(177, 38);
            this.logoPicture.Name = "logoPicture";
            this.logoPicture.Size = new System.Drawing.Size(131, 127);
            this.logoPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoPicture.TabIndex = 0;
            this.logoPicture.TabStop = false;
            // 
            // register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(25)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(473, 463);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.signUpButton);
            this.Controls.Add(this.UserNamePicture);
            this.Controls.Add(this.userNameImage);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.userNameTextBox);
            this.Controls.Add(this.logoPicture);
            this.Name = "register";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "register";
            this.Load += new System.EventHandler(this.register_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BackButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserNamePicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userNameImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox logoPicture;
        private Bunifu.Framework.UI.BunifuMaterialTextbox userNameTextBox;
        private Bunifu.Framework.UI.BunifuMaterialTextbox PasswordTextBox;
        private System.Windows.Forms.PictureBox userNameImage;
        private System.Windows.Forms.PictureBox UserNamePicture;
        private Bunifu.Framework.UI.BunifuFlatButton signUpButton;
        private Bunifu.Framework.UI.BunifuCustomLabel errorLabel;
        private Bunifu.Framework.UI.BunifuImageButton BackButton;
    }
}