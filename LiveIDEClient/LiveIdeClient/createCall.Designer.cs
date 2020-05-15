namespace liveIde
{
    partial class createCall
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
            this.usersTextbox = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.usersList = new System.Windows.Forms.ListBox();
            this.createCallbutton = new Bunifu.Framework.UI.BunifuImageButton();
            this.addUsersLabel = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.BackButton = new Bunifu.Framework.UI.BunifuImageButton();
            this.errorLabel = new Bunifu.Framework.UI.BunifuCustomLabel();
            ((System.ComponentModel.ISupportInitialize)(this.createCallbutton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BackButton)).BeginInit();
            this.SuspendLayout();
            // 
            // usersTextbox
            // 
            this.usersTextbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.usersTextbox.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.usersTextbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.usersTextbox.HintForeColor = System.Drawing.Color.Empty;
            this.usersTextbox.HintText = "";
            this.usersTextbox.isPassword = false;
            this.usersTextbox.LineFocusedColor = System.Drawing.Color.Blue;
            this.usersTextbox.LineIdleColor = System.Drawing.Color.Gray;
            this.usersTextbox.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.usersTextbox.LineThickness = 3;
            this.usersTextbox.Location = new System.Drawing.Point(187, 90);
            this.usersTextbox.Margin = new System.Windows.Forms.Padding(4);
            this.usersTextbox.Name = "usersTextbox";
            this.usersTextbox.Size = new System.Drawing.Size(370, 44);
            this.usersTextbox.TabIndex = 1;
            this.usersTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.usersTextbox.OnValueChanged += new System.EventHandler(this.bunifuMaterialTextbox1_OnValueChanged);
            // 
            // usersList
            // 
            this.usersList.FormattingEnabled = true;
            this.usersList.Location = new System.Drawing.Point(187, 132);
            this.usersList.Name = "usersList";
            this.usersList.Size = new System.Drawing.Size(370, 95);
            this.usersList.TabIndex = 2;
            // 
            // createCallbutton
            // 
            this.createCallbutton.BackColor = System.Drawing.Color.SeaGreen;
            this.createCallbutton.Image = global::liveIde.Properties.Resources.startCall;
            this.createCallbutton.ImageActive = null;
            this.createCallbutton.Location = new System.Drawing.Point(187, 233);
            this.createCallbutton.Name = "createCallbutton";
            this.createCallbutton.Size = new System.Drawing.Size(71, 71);
            this.createCallbutton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.createCallbutton.TabIndex = 3;
            this.createCallbutton.TabStop = false;
            this.createCallbutton.Zoom = 10;
            this.createCallbutton.Click += new System.EventHandler(this.createCallbutton_Click);
            // 
            // addUsersLabel
            // 
            this.addUsersLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addUsersLabel.AutoSize = true;
            this.addUsersLabel.BackColor = System.Drawing.Color.White;
            this.addUsersLabel.Font = new System.Drawing.Font("Segoe UI Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.addUsersLabel.Location = new System.Drawing.Point(243, 46);
            this.addUsersLabel.Name = "addUsersLabel";
            this.addUsersLabel.Size = new System.Drawing.Size(218, 40);
            this.addUsersLabel.TabIndex = 4;
            this.addUsersLabel.Text = "Add users to call";
            // 
            // BackButton
            // 
            this.BackButton.BackColor = System.Drawing.Color.White;
            this.BackButton.ErrorImage = null;
            this.BackButton.Image = global::liveIde.Properties.Resources.leftArrow;
            this.BackButton.ImageActive = null;
            this.BackButton.Location = new System.Drawing.Point(0, 0);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(65, 41);
            this.BackButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BackButton.TabIndex = 8;
            this.BackButton.TabStop = false;
            this.BackButton.Zoom = 10;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Location = new System.Drawing.Point(314, 251);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(103, 13);
            this.errorLabel.TabIndex = 9;
            this.errorLabel.Text = "bunifuCustomLabel1";
            // 
            // createCall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::liveIde.Properties.Resources.theme1;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.addUsersLabel);
            this.Controls.Add(this.createCallbutton);
            this.Controls.Add(this.usersList);
            this.Controls.Add(this.usersTextbox);
            this.Name = "createCall";
            this.Text = "createCall";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.createCall_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.createCallbutton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BackButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Bunifu.Framework.UI.BunifuMaterialTextbox usersTextbox;
        private System.Windows.Forms.ListBox usersList;
        private Bunifu.Framework.UI.BunifuImageButton createCallbutton;
        private Bunifu.Framework.UI.BunifuCustomLabel addUsersLabel;
        private Bunifu.Framework.UI.BunifuImageButton BackButton;
        private Bunifu.Framework.UI.BunifuCustomLabel errorLabel;
    }
}