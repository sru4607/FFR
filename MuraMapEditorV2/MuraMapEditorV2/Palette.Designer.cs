namespace MuraMapEditorV2
{
    partial class Palette
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
            this.ScrollBar = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // ScrollBar
            // 
            this.ScrollBar.Location = new System.Drawing.Point(100, 0);
            this.ScrollBar.Name = "ScrollBar";
            this.ScrollBar.Size = new System.Drawing.Size(17, 166);
            this.ScrollBar.TabIndex = 0;
            // 
            // Palette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Controls.Add(this.ScrollBar);
            this.Name = "Palette";
            this.Size = new System.Drawing.Size(117, 166);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar ScrollBar;
    }
}
