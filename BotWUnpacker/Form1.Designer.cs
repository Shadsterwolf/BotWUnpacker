using System.Reflection;

namespace BotWUnpacker
{
    partial class Form1
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
            System.Windows.Forms.PictureBox imgIcon;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.Label lblFootnote;
            System.Windows.Forms.Label line1;
            this.btnBrowseRoot = new System.Windows.Forms.Button();
            this.btnClearRoot = new System.Windows.Forms.Button();
            this.btnExtractPack = new System.Windows.Forms.Button();
            this.btnBuildPack = new System.Windows.Forms.Button();
            this.lblFolderRoot = new System.Windows.Forms.Label();
            this.tbxFolderRoot = new System.Windows.Forms.TextBox();
            this.cbxWriteXml = new System.Windows.Forms.CheckBox();
            this.lblProcessStatus = new System.Windows.Forms.Label();
            this.cbxSetDataOffset = new System.Windows.Forms.CheckBox();
            this.tbxDataOffset = new System.Windows.Forms.TextBox();
            this.btnBuildCompare = new System.Windows.Forms.Button();
            this.btnExtractAll = new System.Windows.Forms.Button();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            imgIcon = new System.Windows.Forms.PictureBox();
            lblFootnote = new System.Windows.Forms.Label();
            line1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(imgIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // imgIcon
            // 
            imgIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            imgIcon.Image = ((System.Drawing.Image)(resources.GetObject("imgIcon.Image")));
            imgIcon.Location = new System.Drawing.Point(12, 12);
            imgIcon.Name = "imgIcon";
            imgIcon.Size = new System.Drawing.Size(100, 100);
            imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            imgIcon.TabIndex = 1;
            imgIcon.TabStop = false;
            // 
            // lblFootnote
            // 
            lblFootnote.AutoSize = true;
            lblFootnote.Location = new System.Drawing.Point(9, 313);
            lblFootnote.Name = "lblFootnote";
            lblFootnote.Size = new System.Drawing.Size(210, 39);
            lblFootnote.TabIndex = 10;
            lblFootnote.Text = "Version: 1.2 \nMade by Shadsterwolf\nHeavily modified code from UWizard SARC";
            // 
            // line1
            // 
            line1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            line1.Enabled = false;
            line1.Location = new System.Drawing.Point(13, 181);
            line1.Name = "line1";
            line1.Size = new System.Drawing.Size(356, 2);
            line1.TabIndex = 14;
            // 
            // btnBrowseRoot
            // 
            this.btnBrowseRoot.Location = new System.Drawing.Point(119, 13);
            this.btnBrowseRoot.Name = "btnBrowseRoot";
            this.btnBrowseRoot.Size = new System.Drawing.Size(97, 23);
            this.btnBrowseRoot.TabIndex = 4;
            this.btnBrowseRoot.Text = "Set Root Folder";
            this.btnBrowseRoot.UseVisualStyleBackColor = true;
            this.btnBrowseRoot.Click += new System.EventHandler(this.btnBrowseRoot_Click);
            // 
            // btnClearRoot
            // 
            this.btnClearRoot.Location = new System.Drawing.Point(222, 13);
            this.btnClearRoot.Name = "btnClearRoot";
            this.btnClearRoot.Size = new System.Drawing.Size(50, 23);
            this.btnClearRoot.TabIndex = 9;
            this.btnClearRoot.Text = "Clear";
            this.btnClearRoot.UseVisualStyleBackColor = true;
            this.btnClearRoot.Click += new System.EventHandler(this.btnClearRoot_Click);
            // 
            // btnExtractPack
            // 
            this.btnExtractPack.Location = new System.Drawing.Point(12, 122);
            this.btnExtractPack.Name = "btnExtractPack";
            this.btnExtractPack.Size = new System.Drawing.Size(100, 23);
            this.btnExtractPack.TabIndex = 2;
            this.btnExtractPack.Text = "Extract PACK";
            this.btnExtractPack.UseVisualStyleBackColor = true;
            this.btnExtractPack.Click += new System.EventHandler(this.btnExtractPack_Click);
            // 
            // btnBuildPack
            // 
            this.btnBuildPack.Location = new System.Drawing.Point(13, 195);
            this.btnBuildPack.Name = "btnBuildPack";
            this.btnBuildPack.Size = new System.Drawing.Size(99, 47);
            this.btnBuildPack.TabIndex = 3;
            this.btnBuildPack.Text = "Build PACK";
            this.btnBuildPack.UseVisualStyleBackColor = true;
            this.btnBuildPack.Click += new System.EventHandler(this.btnBuildPack_Click);
            // 
            // lblFolderRoot
            // 
            this.lblFolderRoot.AutoSize = true;
            this.lblFolderRoot.Location = new System.Drawing.Point(116, 39);
            this.lblFolderRoot.Name = "lblFolderRoot";
            this.lblFolderRoot.Size = new System.Drawing.Size(109, 13);
            this.lblFolderRoot.TabIndex = 5;
            this.lblFolderRoot.Text = "Root Folder Location:";
            // 
            // tbxFolderRoot
            // 
            this.tbxFolderRoot.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbxFolderRoot.Location = new System.Drawing.Point(119, 55);
            this.tbxFolderRoot.Name = "tbxFolderRoot";
            this.tbxFolderRoot.ReadOnly = true;
            this.tbxFolderRoot.Size = new System.Drawing.Size(253, 20);
            this.tbxFolderRoot.TabIndex = 6;
            // 
            // cbxWriteXml
            // 
            this.cbxWriteXml.AutoSize = true;
            this.cbxWriteXml.Location = new System.Drawing.Point(118, 126);
            this.cbxWriteXml.Name = "cbxWriteXml";
            this.cbxWriteXml.Size = new System.Drawing.Size(109, 17);
            this.cbxWriteXml.TabIndex = 8;
            this.cbxWriteXml.Text = "Write XML debug";
            this.cbxWriteXml.UseVisualStyleBackColor = true;
            // 
            // lblProcessStatus
            // 
            this.lblProcessStatus.AutoSize = true;
            this.lblProcessStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessStatus.Location = new System.Drawing.Point(9, 300);
            this.lblProcessStatus.Name = "lblProcessStatus";
            this.lblProcessStatus.Size = new System.Drawing.Size(81, 13);
            this.lblProcessStatus.TabIndex = 11;
            this.lblProcessStatus.Text = "Processing...";
            this.lblProcessStatus.Visible = false;
            // 
            // cbxSetDataOffset
            // 
            this.cbxSetDataOffset.AutoSize = true;
            this.cbxSetDataOffset.Location = new System.Drawing.Point(119, 195);
            this.cbxSetDataOffset.Name = "cbxSetDataOffset";
            this.cbxSetDataOffset.Size = new System.Drawing.Size(197, 17);
            this.cbxSetDataOffset.TabIndex = 12;
            this.cbxSetDataOffset.Text = "Set Fixed Data Offset (Add Padding)";
            this.cbxSetDataOffset.UseVisualStyleBackColor = true;
            this.cbxSetDataOffset.CheckedChanged += new System.EventHandler(this.cbxSetDataOffset_CheckedChanged);
            // 
            // tbxDataOffset
            // 
            this.tbxDataOffset.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbxDataOffset.Location = new System.Drawing.Point(119, 218);
            this.tbxDataOffset.MaxLength = 4;
            this.tbxDataOffset.Name = "tbxDataOffset";
            this.tbxDataOffset.ReadOnly = true;
            this.tbxDataOffset.Size = new System.Drawing.Size(32, 20);
            this.tbxDataOffset.TabIndex = 13;
            this.tbxDataOffset.Text = "2000";
            // 
            // btnBuildCompare
            // 
            this.btnBuildCompare.Enabled = false;
            this.btnBuildCompare.Location = new System.Drawing.Point(13, 249);
            this.btnBuildCompare.Name = "btnBuildCompare";
            this.btnBuildCompare.Size = new System.Drawing.Size(99, 48);
            this.btnBuildCompare.TabIndex = 15;
            this.btnBuildCompare.Text = "Compare and Build PACK";
            this.btnBuildCompare.UseVisualStyleBackColor = true;
            this.btnBuildCompare.Click += new System.EventHandler(this.btnBuildCompare_Click);
            // 
            // btnExtractAll
            // 
            this.btnExtractAll.Enabled = false;
            this.btnExtractAll.Location = new System.Drawing.Point(12, 151);
            this.btnExtractAll.Name = "btnExtractAll";
            this.btnExtractAll.Size = new System.Drawing.Size(100, 23);
            this.btnExtractAll.TabIndex = 16;
            this.btnExtractAll.Text = "Extract All";
            this.btnExtractAll.UseVisualStyleBackColor = true;
            this.btnExtractAll.Click += new System.EventHandler(this.btnExtractAll_Click);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Enabled = false;
            this.btnOpenFolder.Location = new System.Drawing.Point(119, 81);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(125, 23);
            this.btnOpenFolder.TabIndex = 17;
            this.btnOpenFolder.Text = "Open Root Folder";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.btnExtractAll);
            this.Controls.Add(this.btnBuildCompare);
            this.Controls.Add(line1);
            this.Controls.Add(this.tbxDataOffset);
            this.Controls.Add(this.cbxSetDataOffset);
            this.Controls.Add(this.lblProcessStatus);
            this.Controls.Add(lblFootnote);
            this.Controls.Add(this.btnClearRoot);
            this.Controls.Add(this.cbxWriteXml);
            this.Controls.Add(this.tbxFolderRoot);
            this.Controls.Add(this.lblFolderRoot);
            this.Controls.Add(this.btnBrowseRoot);
            this.Controls.Add(this.btnBuildPack);
            this.Controls.Add(this.btnExtractPack);
            this.Controls.Add(imgIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "BotW Unpacker ";
            ((System.ComponentModel.ISupportInitialize)(imgIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnBrowseRoot;
        private System.Windows.Forms.Button btnClearRoot;
        private System.Windows.Forms.Button btnExtractPack;
        private System.Windows.Forms.Button btnBuildPack;
        private System.Windows.Forms.Label lblFolderRoot;
        private System.Windows.Forms.TextBox tbxFolderRoot;
        private System.Windows.Forms.CheckBox cbxWriteXml;
        private System.Windows.Forms.Label lblProcessStatus;
        private System.Windows.Forms.CheckBox cbxSetDataOffset;
        private System.Windows.Forms.TextBox tbxDataOffset;
        private System.Windows.Forms.Button btnBuildCompare;
        private System.Windows.Forms.Button btnExtractAll;
        private System.Windows.Forms.Button btnOpenFolder;
    }
}

