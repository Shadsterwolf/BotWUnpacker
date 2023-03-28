namespace BotwUnpacker
{
    partial class FrmCompareTool
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCompareTool));
            this.tbxOriFile = new System.Windows.Forms.TextBox();
            this.tbxCustom = new System.Windows.Forms.TextBox();
            this.btnOriBrowse = new System.Windows.Forms.Button();
            this.btnCusBrowse = new System.Windows.Forms.Button();
            this.dgvOriTable = new System.Windows.Forms.DataGridView();
            this.nodeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nodeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nodeSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nodeSizeHex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nodePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nodePadding = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCompare = new System.Windows.Forms.Button();
            this.dgvCusTable = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rbnOriFile = new System.Windows.Forms.RadioButton();
            this.rbnOriFolder = new System.Windows.Forms.RadioButton();
            this.panelOri = new System.Windows.Forms.Panel();
            this.panelCus = new System.Windows.Forms.Panel();
            this.rbnCustomFolder = new System.Windows.Forms.RadioButton();
            this.rbnCustomFile = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOriTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCusTable)).BeginInit();
            this.panelOri.SuspendLayout();
            this.panelCus.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbxOriFile
            // 
            this.tbxOriFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.tbxOriFile.ForeColor = System.Drawing.SystemColors.Window;
            this.tbxOriFile.Location = new System.Drawing.Point(12, 53);
            this.tbxOriFile.Name = "tbxOriFile";
            this.tbxOriFile.ReadOnly = true;
            this.tbxOriFile.Size = new System.Drawing.Size(297, 20);
            this.tbxOriFile.TabIndex = 7;
            // 
            // tbxCustom
            // 
            this.tbxCustom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.tbxCustom.ForeColor = System.Drawing.SystemColors.Window;
            this.tbxCustom.Location = new System.Drawing.Point(397, 53);
            this.tbxCustom.Name = "tbxCustom";
            this.tbxCustom.ReadOnly = true;
            this.tbxCustom.Size = new System.Drawing.Size(297, 20);
            this.tbxCustom.TabIndex = 8;
            // 
            // btnOriBrowse
            // 
            this.btnOriBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOriBrowse.Location = new System.Drawing.Point(128, 23);
            this.btnOriBrowse.Name = "btnOriBrowse";
            this.btnOriBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnOriBrowse.TabIndex = 0;
            this.btnOriBrowse.Text = "Browse";
            this.btnOriBrowse.UseVisualStyleBackColor = true;
            this.btnOriBrowse.Click += new System.EventHandler(this.btnOriBrowse_Click);
            // 
            // btnCusBrowse
            // 
            this.btnCusBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCusBrowse.Location = new System.Drawing.Point(513, 23);
            this.btnCusBrowse.Name = "btnCusBrowse";
            this.btnCusBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnCusBrowse.TabIndex = 1;
            this.btnCusBrowse.Text = "Browse";
            this.btnCusBrowse.UseVisualStyleBackColor = true;
            this.btnCusBrowse.Click += new System.EventHandler(this.btnCusBrowse_Click);
            // 
            // dgvOriTable
            // 
            this.dgvOriTable.AllowUserToAddRows = false;
            this.dgvOriTable.AllowUserToDeleteRows = false;
            this.dgvOriTable.AllowUserToResizeRows = false;
            this.dgvOriTable.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOriTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvOriTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOriTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nodeId,
            this.nodeType,
            this.nodeSize,
            this.nodeSizeHex,
            this.nodePath,
            this.nodePadding});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvOriTable.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvOriTable.Location = new System.Drawing.Point(12, 72);
            this.dgvOriTable.Name = "dgvOriTable";
            this.dgvOriTable.ReadOnly = true;
            this.dgvOriTable.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvOriTable.RowHeadersVisible = false;
            this.dgvOriTable.ShowCellToolTips = false;
            this.dgvOriTable.ShowEditingIcon = false;
            this.dgvOriTable.Size = new System.Drawing.Size(297, 317);
            this.dgvOriTable.TabIndex = 5;
            // 
            // nodeId
            // 
            this.nodeId.Frozen = true;
            this.nodeId.HeaderText = "Node ID";
            this.nodeId.Name = "nodeId";
            this.nodeId.ReadOnly = true;
            this.nodeId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.nodeId.Width = 40;
            // 
            // nodeType
            // 
            this.nodeType.HeaderText = "Type";
            this.nodeType.Name = "nodeType";
            this.nodeType.ReadOnly = true;
            this.nodeType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.nodeType.Width = 40;
            // 
            // nodeSize
            // 
            this.nodeSize.HeaderText = "Size (Bytes)";
            this.nodeSize.Name = "nodeSize";
            this.nodeSize.ReadOnly = true;
            this.nodeSize.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.nodeSize.Width = 50;
            // 
            // nodeSizeHex
            // 
            this.nodeSizeHex.HeaderText = "Size (Hex)";
            this.nodeSizeHex.Name = "nodeSizeHex";
            this.nodeSizeHex.ReadOnly = true;
            this.nodeSizeHex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.nodeSizeHex.Width = 50;
            // 
            // nodePath
            // 
            this.nodePath.FillWeight = 500F;
            this.nodePath.HeaderText = "Path";
            this.nodePath.Name = "nodePath";
            this.nodePath.ReadOnly = true;
            this.nodePath.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.nodePath.Width = 400;
            // 
            // nodePadding
            // 
            this.nodePadding.HeaderText = "Added Padding";
            this.nodePadding.Name = "nodePadding";
            this.nodePadding.ReadOnly = true;
            this.nodePadding.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.nodePadding.Width = 50;
            // 
            // btnCompare
            // 
            this.btnCompare.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompare.Location = new System.Drawing.Point(315, 204);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(75, 43);
            this.btnCompare.TabIndex = 3;
            this.btnCompare.Text = "Compare";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // dgvCusTable
            // 
            this.dgvCusTable.AllowUserToAddRows = false;
            this.dgvCusTable.AllowUserToDeleteRows = false;
            this.dgvCusTable.AllowUserToResizeRows = false;
            this.dgvCusTable.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCusTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCusTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCusTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCusTable.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvCusTable.Location = new System.Drawing.Point(397, 72);
            this.dgvCusTable.Name = "dgvCusTable";
            this.dgvCusTable.ReadOnly = true;
            this.dgvCusTable.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvCusTable.RowHeadersVisible = false;
            this.dgvCusTable.ShowCellToolTips = false;
            this.dgvCusTable.ShowEditingIcon = false;
            this.dgvCusTable.Size = new System.Drawing.Size(297, 317);
            this.dgvCusTable.TabIndex = 13;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "Node ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 40;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Type";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 40;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Size (Bytes)";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 50;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Size (Hex)";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 50;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.FillWeight = 500F;
            this.dataGridViewTextBoxColumn5.HeaderText = "Path";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 400;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Added Padding";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn6.Width = 50;
            // 
            // rbnOriFile
            // 
            this.rbnOriFile.AutoSize = true;
            this.rbnOriFile.Checked = true;
            this.rbnOriFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbnOriFile.Location = new System.Drawing.Point(3, 20);
            this.rbnOriFile.Name = "rbnOriFile";
            this.rbnOriFile.Size = new System.Drawing.Size(40, 17);
            this.rbnOriFile.TabIndex = 17;
            this.rbnOriFile.TabStop = true;
            this.rbnOriFile.Text = "File";
            this.rbnOriFile.UseVisualStyleBackColor = true;
            // 
            // rbnOriFolder
            // 
            this.rbnOriFolder.AutoSize = true;
            this.rbnOriFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbnOriFolder.Location = new System.Drawing.Point(3, 3);
            this.rbnOriFolder.Name = "rbnOriFolder";
            this.rbnOriFolder.Size = new System.Drawing.Size(53, 17);
            this.rbnOriFolder.TabIndex = 16;
            this.rbnOriFolder.Text = "Folder";
            this.rbnOriFolder.UseVisualStyleBackColor = true;
            // 
            // panelOri
            // 
            this.panelOri.Controls.Add(this.rbnOriFolder);
            this.panelOri.Controls.Add(this.rbnOriFile);
            this.panelOri.Location = new System.Drawing.Point(12, 9);
            this.panelOri.Name = "panelOri";
            this.panelOri.Size = new System.Drawing.Size(57, 37);
            this.panelOri.TabIndex = 18;
            // 
            // panelCus
            // 
            this.panelCus.Controls.Add(this.rbnCustomFolder);
            this.panelCus.Controls.Add(this.rbnCustomFile);
            this.panelCus.Location = new System.Drawing.Point(397, 12);
            this.panelCus.Name = "panelCus";
            this.panelCus.Size = new System.Drawing.Size(57, 37);
            this.panelCus.TabIndex = 19;
            // 
            // rbnCustomFolder
            // 
            this.rbnCustomFolder.AutoSize = true;
            this.rbnCustomFolder.Checked = true;
            this.rbnCustomFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbnCustomFolder.Location = new System.Drawing.Point(3, 3);
            this.rbnCustomFolder.Name = "rbnCustomFolder";
            this.rbnCustomFolder.Size = new System.Drawing.Size(53, 17);
            this.rbnCustomFolder.TabIndex = 16;
            this.rbnCustomFolder.TabStop = true;
            this.rbnCustomFolder.Text = "Folder";
            this.rbnCustomFolder.UseVisualStyleBackColor = true;
            // 
            // rbnCustomFile
            // 
            this.rbnCustomFile.AutoSize = true;
            this.rbnCustomFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbnCustomFile.Location = new System.Drawing.Point(3, 20);
            this.rbnCustomFile.Name = "rbnCustomFile";
            this.rbnCustomFile.Size = new System.Drawing.Size(40, 17);
            this.rbnCustomFile.TabIndex = 17;
            this.rbnCustomFile.Text = "File";
            this.rbnCustomFile.UseVisualStyleBackColor = true;
            // 
            // FrmCompareBuild
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(704, 401);
            this.Controls.Add(this.panelCus);
            this.Controls.Add(this.panelOri);
            this.Controls.Add(this.dgvCusTable);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.dgvOriTable);
            this.Controls.Add(this.btnCusBrowse);
            this.Controls.Add(this.btnOriBrowse);
            this.Controls.Add(this.tbxCustom);
            this.Controls.Add(this.tbxOriFile);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmCompareBuild";
            this.Text = "Compare Tool";
            ((System.ComponentModel.ISupportInitialize)(this.dgvOriTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCusTable)).EndInit();
            this.panelOri.ResumeLayout(false);
            this.panelOri.PerformLayout();
            this.panelCus.ResumeLayout(false);
            this.panelCus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxOriFile;
        private System.Windows.Forms.TextBox tbxCustom;
        private System.Windows.Forms.Button btnOriBrowse;
        private System.Windows.Forms.Button btnCusBrowse;
        private System.Windows.Forms.DataGridView dgvOriTable;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.DataGridView dgvCusTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodeSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodeSizeHex;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodePadding;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.RadioButton rbnOriFile;
        private System.Windows.Forms.RadioButton rbnOriFolder;
        private System.Windows.Forms.Panel panelOri;
        private System.Windows.Forms.Panel panelCus;
        private System.Windows.Forms.RadioButton rbnCustomFolder;
        private System.Windows.Forms.RadioButton rbnCustomFile;
    }
}