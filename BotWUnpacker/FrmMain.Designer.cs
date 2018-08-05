using System.Reflection;

namespace BotwUnpacker
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            System.Windows.Forms.Label line1;
            this.btnBrowseRoot = new System.Windows.Forms.Button();
            this.btnExtractPack = new System.Windows.Forms.Button();
            this.btnBuildPack = new System.Windows.Forms.Button();
            this.lblFolderRoot = new System.Windows.Forms.Label();
            this.tbxFolderRoot = new System.Windows.Forms.TextBox();
            this.cbxWriteXml = new System.Windows.Forms.CheckBox();
            this.cbxSetDataOffset = new System.Windows.Forms.CheckBox();
            this.tbxDataOffset = new System.Windows.Forms.TextBox();
            this.btnExtractAll = new System.Windows.Forms.Button();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.btnYaz0Decode = new System.Windows.Forms.Button();
            this.cbxNodeDecode = new System.Windows.Forms.CheckBox();
            this.cbxCompileAllInOneFolder = new System.Windows.Forms.CheckBox();
            this.btnYaz0Encode = new System.Windows.Forms.Button();
            this.btnCompareAndBuild = new System.Windows.Forms.Button();
            this.btnPaddingEditor = new System.Windows.Forms.Button();
            this.lblFootnote = new System.Windows.Forms.Label();
            this.loadingBar = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            imgIcon = new System.Windows.Forms.PictureBox();
            line1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(imgIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingBar)).BeginInit();
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
            // line1
            // 
            line1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            line1.Enabled = false;
            line1.Location = new System.Drawing.Point(12, 225);
            line1.Name = "line1";
            line1.Size = new System.Drawing.Size(356, 2);
            line1.TabIndex = 23;
            // 
            // btnBrowseRoot
            // 
            this.btnBrowseRoot.Location = new System.Drawing.Point(116, 54);
            this.btnBrowseRoot.Name = "btnBrowseRoot";
            this.btnBrowseRoot.Size = new System.Drawing.Size(71, 23);
            this.btnBrowseRoot.TabIndex = 4;
            this.btnBrowseRoot.Text = "Browse";
            this.btnBrowseRoot.UseVisualStyleBackColor = true;
            this.btnBrowseRoot.Click += new System.EventHandler(this.btnBrowseRoot_Click);
            // 
            // btnExtractPack
            // 
            this.btnExtractPack.Location = new System.Drawing.Point(12, 118);
            this.btnExtractPack.Name = "btnExtractPack";
            this.btnExtractPack.Size = new System.Drawing.Size(100, 23);
            this.btnExtractPack.TabIndex = 2;
            this.btnExtractPack.Text = "Unpack SARC";
            this.btnExtractPack.UseVisualStyleBackColor = true;
            this.btnExtractPack.Click += new System.EventHandler(this.btnExtractPack_Click);
            // 
            // btnBuildPack
            // 
            this.btnBuildPack.Location = new System.Drawing.Point(13, 238);
            this.btnBuildPack.Name = "btnBuildPack";
            this.btnBuildPack.Size = new System.Drawing.Size(100, 48);
            this.btnBuildPack.TabIndex = 3;
            this.btnBuildPack.Text = " Build SARC";
            this.btnBuildPack.UseVisualStyleBackColor = true;
            this.btnBuildPack.Click += new System.EventHandler(this.btnBuildPack_Click);
            // 
            // lblFolderRoot
            // 
            this.lblFolderRoot.AutoSize = true;
            this.lblFolderRoot.Location = new System.Drawing.Point(115, 12);
            this.lblFolderRoot.Name = "lblFolderRoot";
            this.lblFolderRoot.Size = new System.Drawing.Size(120, 13);
            this.lblFolderRoot.TabIndex = 5;
            this.lblFolderRoot.Text = "Default Folder Location:";
            // 
            // tbxFolderRoot
            // 
            this.tbxFolderRoot.BackColor = System.Drawing.SystemColors.Control;
            this.tbxFolderRoot.Location = new System.Drawing.Point(116, 28);
            this.tbxFolderRoot.Name = "tbxFolderRoot";
            this.tbxFolderRoot.Size = new System.Drawing.Size(253, 20);
            this.tbxFolderRoot.TabIndex = 6;
            this.tbxFolderRoot.TextChanged += new System.EventHandler(this.tbxFolderRoot_TextChanged);
            // 
            // cbxWriteXml
            // 
            this.cbxWriteXml.AutoSize = true;
            this.cbxWriteXml.Location = new System.Drawing.Point(118, 122);
            this.cbxWriteXml.Name = "cbxWriteXml";
            this.cbxWriteXml.Size = new System.Drawing.Size(109, 17);
            this.cbxWriteXml.TabIndex = 8;
            this.cbxWriteXml.Text = "Write XML debug";
            this.cbxWriteXml.UseVisualStyleBackColor = true;
            // 
            // cbxSetDataOffset
            // 
            this.cbxSetDataOffset.AutoSize = true;
            this.cbxSetDataOffset.Location = new System.Drawing.Point(118, 238);
            this.cbxSetDataOffset.Name = "cbxSetDataOffset";
            this.cbxSetDataOffset.Size = new System.Drawing.Size(99, 17);
            this.cbxSetDataOffset.TabIndex = 12;
            this.cbxSetDataOffset.Text = "Set Data Offset";
            this.cbxSetDataOffset.UseVisualStyleBackColor = true;
            this.cbxSetDataOffset.CheckedChanged += new System.EventHandler(this.cbxSetDataOffset_CheckedChanged);
            this.cbxSetDataOffset.MouseHover += new System.EventHandler(this.cbxSetDataOffset_MouseHover);
            // 
            // tbxDataOffset
            // 
            this.tbxDataOffset.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbxDataOffset.Location = new System.Drawing.Point(213, 238);
            this.tbxDataOffset.MaxLength = 4;
            this.tbxDataOffset.Name = "tbxDataOffset";
            this.tbxDataOffset.ReadOnly = true;
            this.tbxDataOffset.Size = new System.Drawing.Size(32, 20);
            this.tbxDataOffset.TabIndex = 13;
            this.tbxDataOffset.Text = "2000";
            // 
            // btnExtractAll
            // 
            this.btnExtractAll.Enabled = false;
            this.btnExtractAll.Location = new System.Drawing.Point(12, 147);
            this.btnExtractAll.Name = "btnExtractAll";
            this.btnExtractAll.Size = new System.Drawing.Size(100, 23);
            this.btnExtractAll.TabIndex = 16;
            this.btnExtractAll.Text = "Unpack All";
            this.btnExtractAll.UseVisualStyleBackColor = true;
            this.btnExtractAll.Click += new System.EventHandler(this.btnExtractAll_Click);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Enabled = false;
            this.btnOpenFolder.Location = new System.Drawing.Point(193, 54);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(84, 23);
            this.btnOpenFolder.TabIndex = 17;
            this.btnOpenFolder.Text = "Open Folder";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // btnYaz0Decode
            // 
            this.btnYaz0Decode.Location = new System.Drawing.Point(12, 176);
            this.btnYaz0Decode.Name = "btnYaz0Decode";
            this.btnYaz0Decode.Size = new System.Drawing.Size(100, 23);
            this.btnYaz0Decode.TabIndex = 18;
            this.btnYaz0Decode.Text = "Yaz0 Decode";
            this.btnYaz0Decode.UseVisualStyleBackColor = true;
            this.btnYaz0Decode.Click += new System.EventHandler(this.btnYaz0Decode_Click);
            // 
            // cbxNodeDecode
            // 
            this.cbxNodeDecode.AutoSize = true;
            this.cbxNodeDecode.Location = new System.Drawing.Point(12, 205);
            this.cbxNodeDecode.Name = "cbxNodeDecode";
            this.cbxNodeDecode.Size = new System.Drawing.Size(166, 17);
            this.cbxNodeDecode.TabIndex = 20;
            this.cbxNodeDecode.Text = "Auto Decode Unpacked Files";
            this.cbxNodeDecode.UseVisualStyleBackColor = true;
            this.cbxNodeDecode.MouseHover += new System.EventHandler(this.cbxNodeDecode_MouseHover);
            // 
            // cbxCompileAllInOneFolder
            // 
            this.cbxCompileAllInOneFolder.AutoSize = true;
            this.cbxCompileAllInOneFolder.Location = new System.Drawing.Point(118, 151);
            this.cbxCompileAllInOneFolder.Name = "cbxCompileAllInOneFolder";
            this.cbxCompileAllInOneFolder.Size = new System.Drawing.Size(144, 17);
            this.cbxCompileAllInOneFolder.TabIndex = 21;
            this.cbxCompileAllInOneFolder.Text = "Compile All In One Folder";
            this.cbxCompileAllInOneFolder.UseVisualStyleBackColor = true;
            // 
            // btnYaz0Encode
            // 
            this.btnYaz0Encode.Location = new System.Drawing.Point(12, 292);
            this.btnYaz0Encode.Name = "btnYaz0Encode";
            this.btnYaz0Encode.Size = new System.Drawing.Size(100, 23);
            this.btnYaz0Encode.TabIndex = 22;
            this.btnYaz0Encode.Text = "Yaz0 Encode";
            this.btnYaz0Encode.UseVisualStyleBackColor = true;
            this.btnYaz0Encode.Click += new System.EventHandler(this.btnYaz0Encode_Click);
            // 
            // btnCompareAndBuild
            // 
            this.btnCompareAndBuild.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCompareAndBuild.Location = new System.Drawing.Point(272, 238);
            this.btnCompareAndBuild.Name = "btnCompareAndBuild";
            this.btnCompareAndBuild.Size = new System.Drawing.Size(100, 48);
            this.btnCompareAndBuild.TabIndex = 24;
            this.btnCompareAndBuild.Text = "Compare and Build";
            this.btnCompareAndBuild.UseVisualStyleBackColor = false;
            this.btnCompareAndBuild.Click += new System.EventHandler(this.btnCompareAndBuild_Click);
            // 
            // btnPaddingEditor
            // 
            this.btnPaddingEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPaddingEditor.Location = new System.Drawing.Point(272, 292);
            this.btnPaddingEditor.Name = "btnPaddingEditor";
            this.btnPaddingEditor.Size = new System.Drawing.Size(100, 48);
            this.btnPaddingEditor.TabIndex = 25;
            this.btnPaddingEditor.Text = "Padding Editor";
            this.btnPaddingEditor.UseVisualStyleBackColor = false;
            this.btnPaddingEditor.Click += new System.EventHandler(this.btnSarcEditor_Click);
            // 
            // lblFootnote
            // 
            this.lblFootnote.AutoSize = true;
            this.lblFootnote.Location = new System.Drawing.Point(12, 346);
            this.lblFootnote.Name = "lblFootnote";
            this.lblFootnote.Size = new System.Drawing.Size(49, 13);
            this.lblFootnote.TabIndex = 27;
            this.lblFootnote.Text = "Footnote";
            // 
            // loadingBar
            // 
            this.loadingBar.Image = ((System.Drawing.Image)(resources.GetObject("loadingBar.Image")));
            this.loadingBar.Location = new System.Drawing.Point(12, 321);
            this.loadingBar.Name = "loadingBar";
            this.loadingBar.Size = new System.Drawing.Size(179, 22);
            this.loadingBar.TabIndex = 29;
            this.loadingBar.TabStop = false;
            this.loadingBar.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(85, 346);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "BETA";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(384, 401);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loadingBar);
            this.Controls.Add(this.lblFootnote);
            this.Controls.Add(this.btnPaddingEditor);
            this.Controls.Add(this.btnCompareAndBuild);
            this.Controls.Add(line1);
            this.Controls.Add(this.btnYaz0Encode);
            this.Controls.Add(this.cbxCompileAllInOneFolder);
            this.Controls.Add(this.cbxNodeDecode);
            this.Controls.Add(this.btnYaz0Decode);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.btnExtractAll);
            this.Controls.Add(this.tbxDataOffset);
            this.Controls.Add(this.cbxSetDataOffset);
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
            this.Name = "FrmMain";
            this.ShowIcon = false;
            this.Text = "BotW Unpacker ";
            ((System.ComponentModel.ISupportInitialize)(imgIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnBrowseRoot;
        private System.Windows.Forms.Button btnExtractPack;
        private System.Windows.Forms.Button btnBuildPack;
        private System.Windows.Forms.Label lblFolderRoot;
        private System.Windows.Forms.TextBox tbxFolderRoot;
        private System.Windows.Forms.CheckBox cbxWriteXml;
        private System.Windows.Forms.CheckBox cbxSetDataOffset;
        private System.Windows.Forms.TextBox tbxDataOffset;
        private System.Windows.Forms.Button btnExtractAll;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Button btnYaz0Decode;
        private System.Windows.Forms.CheckBox cbxNodeDecode;
        private System.Windows.Forms.CheckBox cbxCompileAllInOneFolder;
        private System.Windows.Forms.Button btnYaz0Encode;
        private System.Windows.Forms.Button btnCompareAndBuild;
        private System.Windows.Forms.Button btnPaddingEditor;
        private System.Windows.Forms.Label lblFootnote;
        private System.Windows.Forms.PictureBox loadingBar;
        private System.Windows.Forms.Label label1;
    }
}

