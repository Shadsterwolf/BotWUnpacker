using System.Reflection;

namespace BotwUnpacker5
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.PictureBox imgIcon;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            System.Windows.Forms.Label line1;
            this.btnBrowseRoot = new System.Windows.Forms.Button();
            this.btnExtractPack = new System.Windows.Forms.Button();
            this.btnBuildPack = new System.Windows.Forms.Button();
            this.lblFolderRoot = new System.Windows.Forms.Label();
            this.tbxFolderRoot = new System.Windows.Forms.TextBox();
            this.cbxWriteSarcXml = new System.Windows.Forms.CheckBox();
            this.cbxSetDataOffset = new System.Windows.Forms.CheckBox();
            this.tbxDataOffset = new System.Windows.Forms.TextBox();
            this.btnExtractAll = new System.Windows.Forms.Button();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.btnYaz0Decode = new System.Windows.Forms.Button();
            this.cbxNodeDecode = new System.Windows.Forms.CheckBox();
            this.cbxCompileAllInOneFolder = new System.Windows.Forms.CheckBox();
            this.btnYaz0Encode = new System.Windows.Forms.Button();
            this.btnCompareTool = new System.Windows.Forms.Button();
            this.btnPaddingTool = new System.Windows.Forms.Button();
            this.lblFootnote = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.loadingBar = new System.Windows.Forms.PictureBox();
            this.cbxWriteYaz0Xml = new System.Windows.Forms.CheckBox();
            this.rbnWiiU = new System.Windows.Forms.RadioButton();
            this.rbnSwitch = new System.Windows.Forms.RadioButton();
            this.doneBar = new System.Windows.Forms.PictureBox();
            imgIcon = new System.Windows.Forms.PictureBox();
            line1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(imgIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doneBar)).BeginInit();
            this.SuspendLayout();
            // 
            // imgIcon
            // 
            imgIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            imgIcon.Image = ((System.Drawing.Image)(resources.GetObject("imgIcon.Image")));
            imgIcon.Location = new System.Drawing.Point(14, 14);
            imgIcon.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            imgIcon.Name = "imgIcon";
            imgIcon.Size = new System.Drawing.Size(117, 115);
            imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            imgIcon.TabIndex = 99;
            imgIcon.TabStop = false;
            // 
            // line1
            // 
            line1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            line1.Enabled = false;
            line1.Location = new System.Drawing.Point(14, 260);
            line1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            line1.Name = "line1";
            line1.Size = new System.Drawing.Size(415, 2);
            line1.TabIndex = 23;
            // 
            // btnBrowseRoot
            // 
            this.btnBrowseRoot.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.btnBrowseRoot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseRoot.Location = new System.Drawing.Point(135, 62);
            this.btnBrowseRoot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnBrowseRoot.Name = "btnBrowseRoot";
            this.btnBrowseRoot.Size = new System.Drawing.Size(83, 27);
            this.btnBrowseRoot.TabIndex = 15;
            this.btnBrowseRoot.Text = "Browse";
            this.btnBrowseRoot.UseVisualStyleBackColor = true;
            this.btnBrowseRoot.Click += new System.EventHandler(this.btnBrowseRoot_Click);
            // 
            // btnExtractPack
            // 
            this.btnExtractPack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExtractPack.Location = new System.Drawing.Point(14, 136);
            this.btnExtractPack.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnExtractPack.Name = "btnExtractPack";
            this.btnExtractPack.Size = new System.Drawing.Size(117, 27);
            this.btnExtractPack.TabIndex = 0;
            this.btnExtractPack.Text = "Unpack SARC";
            this.btnExtractPack.UseVisualStyleBackColor = true;
            this.btnExtractPack.Click += new System.EventHandler(this.btnExtractPack_Click);
            // 
            // btnBuildPack
            // 
            this.btnBuildPack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuildPack.Location = new System.Drawing.Point(14, 275);
            this.btnBuildPack.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnBuildPack.Name = "btnBuildPack";
            this.btnBuildPack.Size = new System.Drawing.Size(118, 55);
            this.btnBuildPack.TabIndex = 5;
            this.btnBuildPack.Text = " Build SARC";
            this.btnBuildPack.UseVisualStyleBackColor = true;
            this.btnBuildPack.Click += new System.EventHandler(this.btnBuildPack_Click);
            // 
            // lblFolderRoot
            // 
            this.lblFolderRoot.AutoSize = true;
            this.lblFolderRoot.Location = new System.Drawing.Point(134, 14);
            this.lblFolderRoot.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFolderRoot.Name = "lblFolderRoot";
            this.lblFolderRoot.Size = new System.Drawing.Size(133, 15);
            this.lblFolderRoot.TabIndex = 5;
            this.lblFolderRoot.Text = "Default Folder Location:";
            // 
            // tbxFolderRoot
            // 
            this.tbxFolderRoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.tbxFolderRoot.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.tbxFolderRoot.Location = new System.Drawing.Point(135, 32);
            this.tbxFolderRoot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbxFolderRoot.Name = "tbxFolderRoot";
            this.tbxFolderRoot.Size = new System.Drawing.Size(294, 23);
            this.tbxFolderRoot.TabIndex = 14;
            this.tbxFolderRoot.TextChanged += new System.EventHandler(this.tbxFolderRoot_TextChanged);
            // 
            // cbxWriteSarcXml
            // 
            this.cbxWriteSarcXml.AutoSize = true;
            this.cbxWriteSarcXml.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.cbxWriteSarcXml.Location = new System.Drawing.Point(138, 141);
            this.cbxWriteSarcXml.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbxWriteSarcXml.Name = "cbxWriteSarcXml";
            this.cbxWriteSarcXml.Size = new System.Drawing.Size(116, 19);
            this.cbxWriteSarcXml.TabIndex = 100;
            this.cbxWriteSarcXml.Text = "Write Xml Debug";
            this.cbxWriteSarcXml.UseVisualStyleBackColor = true;
            // 
            // cbxSetDataOffset
            // 
            this.cbxSetDataOffset.AutoSize = true;
            this.cbxSetDataOffset.Location = new System.Drawing.Point(138, 275);
            this.cbxSetDataOffset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbxSetDataOffset.Name = "cbxSetDataOffset";
            this.cbxSetDataOffset.Size = new System.Drawing.Size(119, 19);
            this.cbxSetDataOffset.TabIndex = 6;
            this.cbxSetDataOffset.Text = "Set Data Offset 0x";
            this.cbxSetDataOffset.UseVisualStyleBackColor = true;
            this.cbxSetDataOffset.CheckedChanged += new System.EventHandler(this.cbxSetDataOffset_CheckedChanged);
            this.cbxSetDataOffset.MouseHover += new System.EventHandler(this.cbxSetDataOffset_MouseHover);
            // 
            // tbxDataOffset
            // 
            this.tbxDataOffset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.tbxDataOffset.Enabled = false;
            this.tbxDataOffset.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.tbxDataOffset.Location = new System.Drawing.Point(261, 272);
            this.tbxDataOffset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbxDataOffset.MaxLength = 4;
            this.tbxDataOffset.Name = "tbxDataOffset";
            this.tbxDataOffset.ReadOnly = true;
            this.tbxDataOffset.Size = new System.Drawing.Size(37, 23);
            this.tbxDataOffset.TabIndex = 7;
            this.tbxDataOffset.Text = "2000";
            // 
            // btnExtractAll
            // 
            this.btnExtractAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExtractAll.Location = new System.Drawing.Point(14, 170);
            this.btnExtractAll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnExtractAll.Name = "btnExtractAll";
            this.btnExtractAll.Size = new System.Drawing.Size(117, 27);
            this.btnExtractAll.TabIndex = 1;
            this.btnExtractAll.Text = "Unpack All";
            this.btnExtractAll.UseVisualStyleBackColor = true;
            this.btnExtractAll.Click += new System.EventHandler(this.btnExtractAll_Click);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFolder.Location = new System.Drawing.Point(225, 62);
            this.btnOpenFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(98, 27);
            this.btnOpenFolder.TabIndex = 16;
            this.btnOpenFolder.Text = "Open Folder";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // btnYaz0Decode
            // 
            this.btnYaz0Decode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYaz0Decode.Location = new System.Drawing.Point(14, 203);
            this.btnYaz0Decode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnYaz0Decode.Name = "btnYaz0Decode";
            this.btnYaz0Decode.Size = new System.Drawing.Size(117, 27);
            this.btnYaz0Decode.TabIndex = 3;
            this.btnYaz0Decode.Text = "Yaz0 Decode";
            this.btnYaz0Decode.UseVisualStyleBackColor = true;
            this.btnYaz0Decode.Click += new System.EventHandler(this.btnYaz0Decode_Click);
            // 
            // cbxNodeDecode
            // 
            this.cbxNodeDecode.AutoSize = true;
            this.cbxNodeDecode.Location = new System.Drawing.Point(14, 237);
            this.cbxNodeDecode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbxNodeDecode.Name = "cbxNodeDecode";
            this.cbxNodeDecode.Size = new System.Drawing.Size(177, 19);
            this.cbxNodeDecode.TabIndex = 4;
            this.cbxNodeDecode.Text = "Auto Decode Unpacked Files";
            this.cbxNodeDecode.UseVisualStyleBackColor = true;
            this.cbxNodeDecode.MouseHover += new System.EventHandler(this.cbxNodeDecode_MouseHover);
            // 
            // cbxCompileAllInOneFolder
            // 
            this.cbxCompileAllInOneFolder.AutoSize = true;
            this.cbxCompileAllInOneFolder.Location = new System.Drawing.Point(138, 174);
            this.cbxCompileAllInOneFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbxCompileAllInOneFolder.Name = "cbxCompileAllInOneFolder";
            this.cbxCompileAllInOneFolder.Size = new System.Drawing.Size(164, 19);
            this.cbxCompileAllInOneFolder.TabIndex = 2;
            this.cbxCompileAllInOneFolder.Text = "Compile All To One Folder";
            this.cbxCompileAllInOneFolder.UseVisualStyleBackColor = true;
            // 
            // btnYaz0Encode
            // 
            this.btnYaz0Encode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYaz0Encode.Location = new System.Drawing.Point(14, 337);
            this.btnYaz0Encode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnYaz0Encode.Name = "btnYaz0Encode";
            this.btnYaz0Encode.Size = new System.Drawing.Size(117, 27);
            this.btnYaz0Encode.TabIndex = 10;
            this.btnYaz0Encode.Text = "Yaz0 Encode";
            this.btnYaz0Encode.UseVisualStyleBackColor = true;
            this.btnYaz0Encode.Click += new System.EventHandler(this.btnYaz0Encode_Click);
            // 
            // btnCompareTool
            // 
            this.btnCompareTool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompareTool.Location = new System.Drawing.Point(317, 275);
            this.btnCompareTool.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCompareTool.Name = "btnCompareTool";
            this.btnCompareTool.Size = new System.Drawing.Size(117, 55);
            this.btnCompareTool.TabIndex = 11;
            this.btnCompareTool.Text = "Compare Tool";
            this.btnCompareTool.UseVisualStyleBackColor = false;
            this.btnCompareTool.Click += new System.EventHandler(this.btnCompareAndBuild_Click);
            // 
            // btnPaddingTool
            // 
            this.btnPaddingTool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPaddingTool.Location = new System.Drawing.Point(317, 337);
            this.btnPaddingTool.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPaddingTool.Name = "btnPaddingTool";
            this.btnPaddingTool.Size = new System.Drawing.Size(117, 55);
            this.btnPaddingTool.TabIndex = 12;
            this.btnPaddingTool.Text = "Padding Tool";
            this.btnPaddingTool.UseVisualStyleBackColor = false;
            this.btnPaddingTool.Click += new System.EventHandler(this.btnSarcEditor_Click);
            // 
            // lblFootnote
            // 
            this.lblFootnote.AutoSize = true;
            this.lblFootnote.Location = new System.Drawing.Point(14, 399);
            this.lblFootnote.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFootnote.Name = "lblFootnote";
            this.lblFootnote.Size = new System.Drawing.Size(55, 15);
            this.lblFootnote.TabIndex = 27;
            this.lblFootnote.Text = "Footnote";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // loadingBar
            // 
            this.loadingBar.Image = ((System.Drawing.Image)(resources.GetObject("loadingBar.Image")));
            this.loadingBar.Location = new System.Drawing.Point(14, 370);
            this.loadingBar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.loadingBar.Name = "loadingBar";
            this.loadingBar.Size = new System.Drawing.Size(209, 25);
            this.loadingBar.TabIndex = 100;
            this.loadingBar.TabStop = false;
            this.loadingBar.Visible = false;
            // 
            // cbxWriteYaz0Xml
            // 
            this.cbxWriteYaz0Xml.AutoSize = true;
            this.cbxWriteYaz0Xml.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.cbxWriteYaz0Xml.Location = new System.Drawing.Point(138, 208);
            this.cbxWriteYaz0Xml.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbxWriteYaz0Xml.Name = "cbxWriteYaz0Xml";
            this.cbxWriteYaz0Xml.Size = new System.Drawing.Size(116, 19);
            this.cbxWriteYaz0Xml.TabIndex = 101;
            this.cbxWriteYaz0Xml.Text = "Write Xml Debug";
            this.cbxWriteYaz0Xml.UseVisualStyleBackColor = true;
            // 
            // rbnWiiU
            // 
            this.rbnWiiU.AutoSize = true;
            this.rbnWiiU.Checked = true;
            this.rbnWiiU.Location = new System.Drawing.Point(138, 301);
            this.rbnWiiU.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rbnWiiU.Name = "rbnWiiU";
            this.rbnWiiU.Size = new System.Drawing.Size(50, 19);
            this.rbnWiiU.TabIndex = 8;
            this.rbnWiiU.TabStop = true;
            this.rbnWiiU.Text = "WiiU";
            this.rbnWiiU.UseVisualStyleBackColor = true;
            // 
            // rbnSwitch
            // 
            this.rbnSwitch.AutoSize = true;
            this.rbnSwitch.Location = new System.Drawing.Point(194, 301);
            this.rbnSwitch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rbnSwitch.Name = "rbnSwitch";
            this.rbnSwitch.Size = new System.Drawing.Size(60, 19);
            this.rbnSwitch.TabIndex = 9;
            this.rbnSwitch.Text = "Switch";
            this.rbnSwitch.UseVisualStyleBackColor = true;
            this.rbnSwitch.CheckedChanged += new System.EventHandler(this.rbnSwitch_CheckedChanged);
            // 
            // doneBar
            // 
            this.doneBar.Image = ((System.Drawing.Image)(resources.GetObject("doneBar.Image")));
            this.doneBar.Location = new System.Drawing.Point(14, 370);
            this.doneBar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.doneBar.Name = "doneBar";
            this.doneBar.Size = new System.Drawing.Size(209, 25);
            this.doneBar.TabIndex = 102;
            this.doneBar.TabStop = false;
            this.doneBar.Visible = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.ClientSize = new System.Drawing.Size(448, 463);
            this.Controls.Add(this.doneBar);
            this.Controls.Add(this.rbnSwitch);
            this.Controls.Add(this.rbnWiiU);
            this.Controls.Add(this.cbxWriteYaz0Xml);
            this.Controls.Add(this.loadingBar);
            this.Controls.Add(this.lblFootnote);
            this.Controls.Add(this.btnPaddingTool);
            this.Controls.Add(this.btnCompareTool);
            this.Controls.Add(line1);
            this.Controls.Add(this.btnYaz0Encode);
            this.Controls.Add(this.cbxCompileAllInOneFolder);
            this.Controls.Add(this.cbxNodeDecode);
            this.Controls.Add(this.btnYaz0Decode);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.btnExtractAll);
            this.Controls.Add(this.tbxDataOffset);
            this.Controls.Add(this.cbxSetDataOffset);
            this.Controls.Add(this.cbxWriteSarcXml);
            this.Controls.Add(this.tbxFolderRoot);
            this.Controls.Add(this.lblFolderRoot);
            this.Controls.Add(this.btnBrowseRoot);
            this.Controls.Add(this.btnBuildPack);
            this.Controls.Add(this.btnExtractPack);
            this.Controls.Add(imgIcon);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.ShowIcon = false;
            this.Text = "BotW Unpacker";
            ((System.ComponentModel.ISupportInitialize)(imgIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doneBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnBrowseRoot;
        private System.Windows.Forms.Button btnExtractPack;
        private System.Windows.Forms.Button btnBuildPack;
        private System.Windows.Forms.Label lblFolderRoot;
        private System.Windows.Forms.TextBox tbxFolderRoot;
        private System.Windows.Forms.CheckBox cbxWriteSarcXml;
        private System.Windows.Forms.CheckBox cbxSetDataOffset;
        private System.Windows.Forms.TextBox tbxDataOffset;
        private System.Windows.Forms.Button btnExtractAll;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Button btnYaz0Decode;
        private System.Windows.Forms.CheckBox cbxNodeDecode;
        private System.Windows.Forms.CheckBox cbxCompileAllInOneFolder;
        private System.Windows.Forms.Button btnYaz0Encode;
        private System.Windows.Forms.Button btnCompareTool;
        private System.Windows.Forms.Button btnPaddingTool;
        private System.Windows.Forms.Label lblFootnote;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox loadingBar;
        private System.Windows.Forms.CheckBox cbxWriteYaz0Xml;
        private System.Windows.Forms.RadioButton rbnWiiU;
        private System.Windows.Forms.RadioButton rbnSwitch;
        private System.Windows.Forms.PictureBox doneBar;
    }
}

