namespace Sigrica
{
    partial class frm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm));
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.picboxMario = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picboxMario)).BeginInit();
            this.SuspendLayout();
            // 
            // Timer
            // 
            this.Timer.Enabled = true;
            this.Timer.Interval = 10;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // picboxMario
            // 
            this.picboxMario.Image = ((System.Drawing.Image)(resources.GetObject("picboxMario.Image")));
            this.picboxMario.InitialImage = ((System.Drawing.Image)(resources.GetObject("picboxMario.InitialImage")));
            this.picboxMario.Location = new System.Drawing.Point(288, 154);
            this.picboxMario.Name = "picboxMario";
            this.picboxMario.Size = new System.Drawing.Size(50, 100);
            this.picboxMario.TabIndex = 0;
            this.picboxMario.TabStop = false;
            // 
            // frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 571);
            this.Controls.Add(this.picboxMario);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm";
            this.Text = "Sigrica";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.picboxMario)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer Timer;
        private System.Windows.Forms.PictureBox picboxMario;
    }
}

