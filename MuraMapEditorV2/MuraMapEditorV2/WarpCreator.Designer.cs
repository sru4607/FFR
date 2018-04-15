namespace MuraMapEditorV2
{
    partial class WarpCreator
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
            this.DestinationButton = new System.Windows.Forms.Button();
            this.SetWarpButton = new System.Windows.Forms.Button();
            this.DestinationLabel = new System.Windows.Forms.Label();
            this.OpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // DestinationButton
            // 
            this.DestinationButton.Location = new System.Drawing.Point(12, 12);
            this.DestinationButton.Name = "DestinationButton";
            this.DestinationButton.Size = new System.Drawing.Size(130, 23);
            this.DestinationButton.TabIndex = 0;
            this.DestinationButton.Text = "Open Destination Map";
            this.DestinationButton.UseVisualStyleBackColor = true;
            this.DestinationButton.Click += new System.EventHandler(this.DestinationButton_Click);
            // 
            // SetWarpButton
            // 
            this.SetWarpButton.Enabled = false;
            this.SetWarpButton.Location = new System.Drawing.Point(161, 11);
            this.SetWarpButton.Name = "SetWarpButton";
            this.SetWarpButton.Size = new System.Drawing.Size(93, 23);
            this.SetWarpButton.TabIndex = 1;
            this.SetWarpButton.Text = "Set Destination:";
            this.SetWarpButton.UseVisualStyleBackColor = true;
            this.SetWarpButton.Click += new System.EventHandler(this.SetWarpButton_Click);
            // 
            // DestinationLabel
            // 
            this.DestinationLabel.AutoSize = true;
            this.DestinationLabel.Location = new System.Drawing.Point(260, 16);
            this.DestinationLabel.Name = "DestinationLabel";
            this.DestinationLabel.Size = new System.Drawing.Size(47, 13);
            this.DestinationLabel.TabIndex = 2;
            this.DestinationLabel.Text = "(x,y) = (,)";
            // 
            // OpenDialog
            // 
            this.OpenDialog.DefaultExt = "map";
            this.OpenDialog.FileName = "openFileDialog1";
            this.OpenDialog.Filter = "Map Files|*.map";
            this.OpenDialog.Title = "Load Map";
            this.OpenDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenDialog_FileOk);
            // 
            // WarpCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(418, 261);
            this.Controls.Add(this.DestinationLabel);
            this.Controls.Add(this.SetWarpButton);
            this.Controls.Add(this.DestinationButton);
            this.Name = "WarpCreator";
            this.Text = "WarpCreator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DestinationButton;
        private System.Windows.Forms.Button SetWarpButton;
        private System.Windows.Forms.Label DestinationLabel;
        private System.Windows.Forms.OpenFileDialog OpenDialog;
    }
}