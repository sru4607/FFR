﻿namespace MuraMapEditorV2
{
    partial class MapEditor
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
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.OpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.TilePalette = new MuraMapEditorV2.Palette();
            this.MapView = new MuraMapEditorV2.Map();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.WarpButton = new System.Windows.Forms.Button();
            this.EnemyButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(802, 24);
            this.MenuStrip.TabIndex = 1;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(676, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tile Palette";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Map";
            // 
            // SaveDialog
            // 
            this.SaveDialog.DefaultExt = "map";
            this.SaveDialog.FileName = "Map1";
            this.SaveDialog.Filter = "Map File|*.map";
            this.SaveDialog.InitialDirectory = "/Maps";
            this.SaveDialog.Title = "Save As";
            this.SaveDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveDialog_FileOk);
            // 
            // OpenDialog
            // 
            this.OpenDialog.DefaultExt = "map";
            this.OpenDialog.Filter = "Map File|*.map";
            this.OpenDialog.InitialDirectory = "Maps";
            this.OpenDialog.Title = "Open File";
            this.OpenDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenDialog_FileOk);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(677, 228);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Event Placement:";
            // 
            // TilePalette
            // 
            this.TilePalette.AutoScroll = true;
            this.TilePalette.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.TilePalette.Location = new System.Drawing.Point(679, 55);
            this.TilePalette.Name = "TilePalette";
            this.TilePalette.Size = new System.Drawing.Size(117, 166);
            this.TilePalette.TabIndex = 5;
            this.TilePalette.Click += new System.EventHandler(this.TilePalette_Click);
            // 
            // MapView
            // 
            this.MapView.AutoScroll = true;
            this.MapView.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.MapView.Location = new System.Drawing.Point(12, 55);
            this.MapView.Name = "MapView";
            this.MapView.Size = new System.Drawing.Size(658, 498);
            this.MapView.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(680, 245);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Remove Event";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(680, 302);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Create Enemy";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(683, 365);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Create Warp";
            // 
            // WarpButton
            // 
            this.WarpButton.Image = global::MuraMapEditorV2.Properties.Resources.Warp;
            this.WarpButton.Location = new System.Drawing.Point(680, 381);
            this.WarpButton.Name = "WarpButton";
            this.WarpButton.Size = new System.Drawing.Size(40, 40);
            this.WarpButton.TabIndex = 12;
            this.WarpButton.UseVisualStyleBackColor = true;
            this.WarpButton.Click += new System.EventHandler(this.EventButton_Click);
            // 
            // EnemyButton
            // 
            this.EnemyButton.Image = global::MuraMapEditorV2.Properties.Resources.Enemy;
            this.EnemyButton.Location = new System.Drawing.Point(680, 318);
            this.EnemyButton.Name = "EnemyButton";
            this.EnemyButton.Size = new System.Drawing.Size(40, 40);
            this.EnemyButton.TabIndex = 9;
            this.EnemyButton.UseVisualStyleBackColor = true;
            this.EnemyButton.Click += new System.EventHandler(this.EventButton_Click);
            // 
            // button1
            // 
            this.button1.Image = global::MuraMapEditorV2.Properties.Resources.None;
            this.button1.Location = new System.Drawing.Point(723, 245);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(0, 0);
            this.button1.TabIndex = 8;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ClearButton
            // 
            this.ClearButton.Image = global::MuraMapEditorV2.Properties.Resources.None;
            this.ClearButton.Location = new System.Drawing.Point(680, 261);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(40, 40);
            this.ClearButton.TabIndex = 7;
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.EventButton_Click);
            // 
            // MapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(802, 581);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.WarpButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.EnemyButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TilePalette);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MapView);
            this.Controls.Add(this.MenuStrip);
            this.MainMenuStrip = this.MenuStrip;
            this.Name = "MapEditor";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Map MapView;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Palette TilePalette;
        private System.Windows.Forms.SaveFileDialog SaveDialog;
        private System.Windows.Forms.OpenFileDialog OpenDialog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button EnemyButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button WarpButton;
        private System.Windows.Forms.Label label6;
    }
}

