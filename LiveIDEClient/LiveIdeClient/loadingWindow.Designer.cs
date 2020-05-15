namespace liveIde
{
    partial class loadingWindow
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
            this.myPreloader1 = new liveIde.myPreloader();
            this.SuspendLayout();
            // 
            // myPreloader1
            // 
            this.myPreloader1.Location = new System.Drawing.Point(116, 150);
            this.myPreloader1.Name = "myPreloader1";
            this.myPreloader1.Size = new System.Drawing.Size(267, 235);
            this.myPreloader1.TabIndex = 2;
            // 
            // loadingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(520, 455);
            this.Controls.Add(this.myPreloader1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "loadingWindow";
            this.Text = "loadingWindow";
            this.ResumeLayout(false);

        }

        #endregion
        private myPreloader myPreloader1;
    }
}