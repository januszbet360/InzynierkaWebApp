using System.Windows.Forms;

namespace PredictiveSystemManagement
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.LogoPanel = new System.Windows.Forms.Panel();
            this.LogoPictureBox = new System.Windows.Forms.PictureBox();
            this.HistoricalDataPanel = new System.Windows.Forms.Panel();
            this.HistoricalDataGroupBox = new System.Windows.Forms.GroupBox();
            this.FilesListBox = new System.Windows.Forms.ListBox();
            this.FilesListLabel = new System.Windows.Forms.Label();
            this.ProcessFilesButton = new System.Windows.Forms.Button();
            this.RemoveFileButton = new System.Windows.Forms.Button();
            this.AddFileButton = new System.Windows.Forms.Button();
            this.ManagementSystemPanel = new System.Windows.Forms.Panel();
            this.ManagementSystemGroupBox = new System.Windows.Forms.GroupBox();
            this.LogoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
            this.HistoricalDataPanel.SuspendLayout();
            this.HistoricalDataGroupBox.SuspendLayout();
            this.ManagementSystemPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogoPanel
            // 
            this.LogoPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.LogoPanel.Controls.Add(this.LogoPictureBox);
            this.LogoPanel.Location = new System.Drawing.Point(0, 0);
            this.LogoPanel.Name = "LogoPanel";
            this.LogoPanel.Size = new System.Drawing.Size(1000, 150);
            this.LogoPanel.TabIndex = 0;
            // 
            // LogoPictureBox
            // 
            this.LogoPictureBox.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.LogoPictureBox.BackgroundImage = global::PredictiveSystemManagement.Properties.Resources.Stadium;
            this.LogoPictureBox.Image = global::PredictiveSystemManagement.Properties.Resources.Logo;
            this.LogoPictureBox.Location = new System.Drawing.Point(0, 0);
            this.LogoPictureBox.Name = "LogoPictureBox";
            this.LogoPictureBox.Size = new System.Drawing.Size(1000, 150);
            this.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LogoPictureBox.TabIndex = 0;
            this.LogoPictureBox.TabStop = false;
            // 
            // HistoricalDataPanel
            // 
            this.HistoricalDataPanel.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.HistoricalDataPanel.Controls.Add(this.HistoricalDataGroupBox);
            this.HistoricalDataPanel.Location = new System.Drawing.Point(0, 150);
            this.HistoricalDataPanel.Name = "HistoricalDataPanel";
            this.HistoricalDataPanel.Size = new System.Drawing.Size(300, 450);
            this.HistoricalDataPanel.TabIndex = 1;
            // 
            // HistoricalDataGroupBox
            // 
            this.HistoricalDataGroupBox.Controls.Add(this.FilesListBox);
            this.HistoricalDataGroupBox.Controls.Add(this.FilesListLabel);
            this.HistoricalDataGroupBox.Controls.Add(this.ProcessFilesButton);
            this.HistoricalDataGroupBox.Controls.Add(this.RemoveFileButton);
            this.HistoricalDataGroupBox.Controls.Add(this.AddFileButton);
            this.HistoricalDataGroupBox.Font = new System.Drawing.Font("Tekton Pro Ext", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.HistoricalDataGroupBox.Location = new System.Drawing.Point(12, 19);
            this.HistoricalDataGroupBox.Name = "HistoricalDataGroupBox";
            this.HistoricalDataGroupBox.Size = new System.Drawing.Size(264, 400);
            this.HistoricalDataGroupBox.TabIndex = 0;
            this.HistoricalDataGroupBox.TabStop = false;
            this.HistoricalDataGroupBox.Text = "Dane historyczne: ";
            // 
            // FilesListBox
            // 
            this.FilesListBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FilesListBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilesListBox.FormattingEnabled = true;
            this.FilesListBox.HorizontalScrollbar = true;
            this.FilesListBox.ItemHeight = 16;
            this.FilesListBox.Location = new System.Drawing.Point(9, 55);
            this.FilesListBox.Name = "FilesListBox";
            this.FilesListBox.Size = new System.Drawing.Size(208, 292);
            this.FilesListBox.TabIndex = 5;
            // 
            // FilesListLabel
            // 
            this.FilesListLabel.AutoSize = true;
            this.FilesListLabel.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FilesListLabel.Location = new System.Drawing.Point(6, 36);
            this.FilesListLabel.Name = "FilesListLabel";
            this.FilesListLabel.Size = new System.Drawing.Size(113, 20);
            this.FilesListLabel.TabIndex = 4;
            this.FilesListLabel.Text = "Lista plików CSV:";
            // 
            // ProcessFilesButton
            // 
            this.ProcessFilesButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.ProcessFilesButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ProcessFilesButton.Font = new System.Drawing.Font("Tekton Pro Ext", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessFilesButton.Location = new System.Drawing.Point(6, 353);
            this.ProcessFilesButton.Name = "ProcessFilesButton";
            this.ProcessFilesButton.Size = new System.Drawing.Size(211, 28);
            this.ProcessFilesButton.TabIndex = 3;
            this.ProcessFilesButton.Text = "PRZETWARZAJ";
            this.ProcessFilesButton.UseVisualStyleBackColor = false;
            this.ProcessFilesButton.Click += new System.EventHandler(this.ProcessFilesButton_Click);
            // 
            // RemoveFileButton
            // 
            this.RemoveFileButton.BackColor = System.Drawing.Color.Red;
            this.RemoveFileButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RemoveFileButton.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.RemoveFileButton.Location = new System.Drawing.Point(223, 97);
            this.RemoveFileButton.Name = "RemoveFileButton";
            this.RemoveFileButton.Size = new System.Drawing.Size(35, 37);
            this.RemoveFileButton.TabIndex = 2;
            this.RemoveFileButton.Text = "-";
            this.RemoveFileButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.RemoveFileButton.UseVisualStyleBackColor = false;
            this.RemoveFileButton.Click += new System.EventHandler(this.RemoveFileButton_Click);
            // 
            // AddFileButton
            // 
            this.AddFileButton.BackColor = System.Drawing.Color.Lime;
            this.AddFileButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddFileButton.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.AddFileButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.AddFileButton.Location = new System.Drawing.Point(223, 55);
            this.AddFileButton.Name = "AddFileButton";
            this.AddFileButton.Size = new System.Drawing.Size(35, 36);
            this.AddFileButton.TabIndex = 1;
            this.AddFileButton.Text = "+";
            this.AddFileButton.UseVisualStyleBackColor = false;
            this.AddFileButton.Click += new System.EventHandler(this.AddFileButton_Click);
            // 
            // ManagementSystemPanel
            // 
            this.ManagementSystemPanel.BackColor = System.Drawing.Color.YellowGreen;
            this.ManagementSystemPanel.Controls.Add(this.ManagementSystemGroupBox);
            this.ManagementSystemPanel.Location = new System.Drawing.Point(300, 150);
            this.ManagementSystemPanel.Name = "ManagementSystemPanel";
            this.ManagementSystemPanel.Size = new System.Drawing.Size(700, 450);
            this.ManagementSystemPanel.TabIndex = 2;
            // 
            // ManagementSystemGroupBox
            // 
            this.ManagementSystemGroupBox.Font = new System.Drawing.Font("Tekton Pro Ext", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ManagementSystemGroupBox.Location = new System.Drawing.Point(29, 19);
            this.ManagementSystemGroupBox.Name = "ManagementSystemGroupBox";
            this.ManagementSystemGroupBox.Size = new System.Drawing.Size(658, 400);
            this.ManagementSystemGroupBox.TabIndex = 0;
            this.ManagementSystemGroupBox.TabStop = false;
            this.ManagementSystemGroupBox.Text = "Zarządzanie systemem predykcji: ";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 600);
            this.Controls.Add(this.ManagementSystemPanel);
            this.Controls.Add(this.HistoricalDataPanel);
            this.Controls.Add(this.LogoPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Predictive System Management";
            this.LogoPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
            this.HistoricalDataPanel.ResumeLayout(false);
            this.HistoricalDataGroupBox.ResumeLayout(false);
            this.HistoricalDataGroupBox.PerformLayout();
            this.ManagementSystemPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LogoPanel;
        private System.Windows.Forms.Panel HistoricalDataPanel;
        private System.Windows.Forms.Panel ManagementSystemPanel;
        private PictureBox LogoPictureBox;
        private GroupBox HistoricalDataGroupBox;
        private GroupBox ManagementSystemGroupBox;
        private Label FilesListLabel;
        private Button ProcessFilesButton;
        private Button RemoveFileButton;
        private Button AddFileButton;
        private ListBox FilesListBox;
    }
}

