namespace liveIde
{
    partial class cmdRun
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
            this.output = new System.Windows.Forms.Label();
            this.Instructions = new System.Windows.Forms.Label();
            this.BackButton = new Bunifu.Framework.UI.BunifuImageButton();
            this.titeleLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BackButton)).BeginInit();
            this.SuspendLayout();
            // 
            // output
            // 
            this.output.AutoSize = true;
            this.output.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.output.Location = new System.Drawing.Point(28, 85);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(55, 20);
            this.output.TabIndex = 2;
            this.output.Text = "output";
            // 
            // Instructions
            // 
            this.Instructions.AutoSize = true;
            this.Instructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Instructions.Location = new System.Drawing.Point(28, 65);
            this.Instructions.Name = "Instructions";
            this.Instructions.Size = new System.Drawing.Size(54, 20);
            this.Instructions.TabIndex = 3;
            this.Instructions.Text = "errors:";
            // 
            // BackButton
            // 
            this.BackButton.BackColor = System.Drawing.Color.DarkSlateGray;
            this.BackButton.ErrorImage = null;
            this.BackButton.Image = global::liveIde.Properties.Resources.leftArrow;
            this.BackButton.ImageActive = null;
            this.BackButton.Location = new System.Drawing.Point(0, 5);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(55, 25);
            this.BackButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BackButton.TabIndex = 8;
            this.BackButton.TabStop = false;
            this.BackButton.Zoom = 10;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // titeleLabel
            // 
            this.titeleLabel.AutoSize = true;
            this.titeleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.titeleLabel.Location = new System.Drawing.Point(100, 9);
            this.titeleLabel.Name = "titeleLabel";
            this.titeleLabel.Size = new System.Drawing.Size(144, 29);
            this.titeleLabel.TabIndex = 9;
            this.titeleLabel.Text = "Code output";
            // 
            // cmdRun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.titeleLabel);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.Instructions);
            this.Controls.Add(this.output);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "cmdRun";
            this.Text = "cmdRun";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.cmdRun_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.BackButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label output;
        private System.Windows.Forms.Label Instructions;
        private Bunifu.Framework.UI.BunifuImageButton BackButton;
        private System.Windows.Forms.Label titeleLabel;
    }
}