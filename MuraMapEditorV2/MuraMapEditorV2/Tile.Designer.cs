namespace MuraMapEditorV2
{
    partial class Tile
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
            this.ImageDisplay = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ImageDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // ImageDisplay
            // 
            this.ImageDisplay.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ImageDisplay.Location = new System.Drawing.Point(0, 0);
            this.ImageDisplay.Name = "ImageDisplay";
            this.ImageDisplay.Size = new System.Drawing.Size(32, 32);
            this.ImageDisplay.TabIndex = 0;
            this.ImageDisplay.TabStop = false;
            this.ImageDisplay.Click += new System.EventHandler(this.ImageDisplay_Click);
            this.ImageDisplay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ImageDisplay_MouseClick);
            this.ImageDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImageDisplay_MouseDown);
            this.ImageDisplay.MouseEnter += new System.EventHandler(this.ImageDisplay_MouseEnter);
            this.ImageDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImageDisplay_MouseMove);
            // 
            // Tile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ImageDisplay);
            this.Name = "Tile";
            this.Size = new System.Drawing.Size(32, 32);
            ((System.ComponentModel.ISupportInitialize)(this.ImageDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ImageDisplay;
    }
}
