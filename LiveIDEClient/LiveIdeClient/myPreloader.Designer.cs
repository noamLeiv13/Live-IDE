namespace liveIde
{
    partial class myPreloader
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(myPreloader));
            this.CircleProgressbar = new Bunifu.Framework.UI.BunifuCircleProgressbar();
            this.lineSpeed = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // CircleProgressbar
            // 
            this.CircleProgressbar.animated = true;
            this.CircleProgressbar.animationIterval = 1;
            this.CircleProgressbar.animationSpeed = 1;
            this.CircleProgressbar.BackColor = System.Drawing.SystemColors.Control;
            this.CircleProgressbar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CircleProgressbar.BackgroundImage")));
            this.CircleProgressbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CircleProgressbar.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F);
            this.CircleProgressbar.ForeColor = System.Drawing.Color.SeaGreen;
            this.CircleProgressbar.LabelVisible = false;
            this.CircleProgressbar.LineProgressThickness = 8;
            this.CircleProgressbar.LineThickness = 5;
            this.CircleProgressbar.Location = new System.Drawing.Point(0, 0);
            this.CircleProgressbar.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.CircleProgressbar.MaxValue = 100;
            this.CircleProgressbar.Name = "CircleProgressbar";
            this.CircleProgressbar.ProgressBackColor = System.Drawing.SystemColors.Control;
            this.CircleProgressbar.ProgressColor = System.Drawing.Color.LightSeaGreen;
            this.CircleProgressbar.Size = new System.Drawing.Size(235, 235);
            this.CircleProgressbar.TabIndex = 0;
            this.CircleProgressbar.Value = 30;
            // 
            // lineSpeed
            // 
            this.lineSpeed.Enabled = true;
            this.lineSpeed.Tick += new System.EventHandler(this.lineSpeed_Tick);
            // 
            // myPreloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CircleProgressbar);
            this.Name = "myPreloader";
            this.Size = new System.Drawing.Size(267, 235);
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuCircleProgressbar CircleProgressbar;
        private System.Windows.Forms.Timer lineSpeed;
    }
}
