namespace MuraMapEditorV2
{
    partial class Map
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
            this.HScrollBar = new System.Windows.Forms.HScrollBar();
            this.VScrollBar = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // HScrollBar
            // 
            this.HScrollBar.Location = new System.Drawing.Point(0, 481);
            this.HScrollBar.Name = "HScrollBar";
            this.HScrollBar.Size = new System.Drawing.Size(641, 17);
            this.HScrollBar.TabIndex = 0;
            // 
            // VScrollBar
            // 
            this.VScrollBar.Location = new System.Drawing.Point(641, 0);
            this.VScrollBar.Name = "VScrollBar";
            this.VScrollBar.Size = new System.Drawing.Size(17, 481);
            this.VScrollBar.TabIndex = 1;
            // 
            // Map
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Controls.Add(this.VScrollBar);
            this.Controls.Add(this.HScrollBar);
            this.Name = "Map";
            this.Size = new System.Drawing.Size(658, 498);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HScrollBar HScrollBar;
        private System.Windows.Forms.VScrollBar VScrollBar;
    }
}
