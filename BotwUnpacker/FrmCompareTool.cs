using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BotwUnpacker
{
    public partial class FrmCompareTool : Form
    {
        public FrmCompareTool()
        {
            InitializeComponent();
        }

        #region Button Browse Original
        private void btnOriBrowse_Click(object sender, EventArgs e)
        {
            if (rbnOriFile.Checked)
            {
                OpenFileDialog oriFile = new OpenFileDialog();
                oriFile.Filter = "All Files|*.*";
                if (Properties.Settings.Default.RootFolder != "") oriFile.InitialDirectory = Properties.Settings.Default.RootFolder;
                if (oriFile.ShowDialog() == DialogResult.Cancel) goto toss;
                tbxOriFile.Text = oriFile.FileName;

                dgvOriTable.Rows.Clear();
                dgvOriTable.Refresh();
                verify:
                byte[] fileCheck = System.IO.File.ReadAllBytes(tbxOriFile.Text);
                if (fileCheck[0] == 'Y' && fileCheck[1] == 'a' && fileCheck[2] == 'z' && fileCheck[3] == '0') // if Yaz0 encoded, ask if they want to decode it
                {
                    DialogResult diagResult = MessageBox.Show("This file is encoded!" + "\n\n" + "Do you want to decode?\nIt will create a seperate file automatically", "Yaz0 Encoded file...", MessageBoxButtons.YesNo);
                    if (diagResult == DialogResult.No)
                    {
                        tbxOriFile.Text = "";
                        goto toss;
                    }
                    string outFile = Yaz0.DecodeOutputFileRename(oriFile.FileName);
                    if (!Yaz0.Decode(oriFile.FileName, outFile))
                    {
                        MessageBox.Show("Decode error:" + "\n\n" + Yaz0.lerror);
                        tbxOriFile.Text = "";
                        goto toss;
                    }
                    tbxOriFile.Text = outFile;
                    goto verify;
                }
                if (("" + ((char)fileCheck[0]) + ((char)fileCheck[1]) + ((char)fileCheck[2]) + ((char)fileCheck[3])) != "SARC")
                {
                    MessageBox.Show("Not a SARC archive! Missing SARC header at 0x00" + "\n" + "( Your file header is: " + ((char)fileCheck[0]) + ((char)fileCheck[1]) + ((char)fileCheck[2]) + ((char)fileCheck[3]) + " )");
                    tbxOriFile.Text = "";
                    goto toss;
                }

                int nodeCount = SARC.GetFileNodeCount(tbxOriFile.Text);
                string[] nodeTypes = SARC.GetFileNodeType(tbxOriFile.Text);
                uint[] nodeSizes = SARC.GetFileNodeSizes(tbxOriFile.Text);
                string[] nodePaths = SARC.GetFileNodePaths(tbxOriFile.Text);
                uint[] nodePaddings = SARC.GetFileNodePaddings(tbxOriFile.Text);

                for (int i = 0; i < nodeCount; i++)
                {
                    dgvOriTable.Rows.Add();
                    dgvOriTable.Rows[i].Cells[0].Value = i + 1;
                    dgvOriTable.Rows[i].Cells[1].Value = nodeTypes[i];
                    dgvOriTable.Rows[i].Cells[2].Value = nodeSizes[i];
                    dgvOriTable.Rows[i].Cells[3].Value = nodeSizes[i].ToString("X");
                    dgvOriTable.Rows[i].Cells[4].Value = nodePaths[i];
                    dgvOriTable.Rows[i].Cells[5].Value = nodePaddings[i];
                }

                toss:
                oriFile.Dispose();
                GC.Collect();
            }
            else if (rbnOriFolder.Checked)
            {
                CommonOpenFileDialog oriFolder = new CommonOpenFileDialog();
                oriFolder.IsFolderPicker = true;
                if (Properties.Settings.Default.RootFolder != "") oriFolder.InitialDirectory = Properties.Settings.Default.RootFolder;
                if (oriFolder.ShowDialog() == CommonFileDialogResult.Cancel) goto toss;
                tbxOriFile.Text = oriFolder.FileName;

                dgvOriTable.Rows.Clear();
                dgvCusTable.Refresh();
                string[] oriFolderFiles = System.IO.Directory.GetFiles(oriFolder.FileName == "" ? System.Environment.CurrentDirectory : oriFolder.FileName, "*.*", System.IO.SearchOption.AllDirectories);
                int nodeCount = oriFolderFiles.Length;
                if (nodeCount > 1000)
                {
                    MessageBox.Show("Too many files (1000+)!\n\nI doubt you ment to select this folder...\n" + tbxOriFile.Text + "\n\nTry again");
                    tbxOriFile.Text = "";
                    goto toss;
                }
                uint[] nodeSizes = SARC.GetFolderFileSizes(oriFolder.FileName);
                string[] nodeTypes = SARC.GetFolderFileTypes(oriFolder.FileName);
                string[] nodePaths = SARC.GetFolderFilePaths(oriFolder.FileName);

                for (int i = 0; i < nodeCount; i++)
                {
                    dgvOriTable.Rows.Add();
                    dgvOriTable.Rows[i].Cells[0].Value = i + 1;
                    dgvOriTable.Rows[i].Cells[1].Value = nodeTypes[i];
                    dgvOriTable.Rows[i].Cells[2].Value = nodeSizes[i];
                    dgvOriTable.Rows[i].Cells[3].Value = nodeSizes[i].ToString("X");
                    dgvOriTable.Rows[i].Cells[4].Value = nodePaths[i];
                }

                toss:
                oriFolder.Dispose();
                GC.Collect();
            }
        }
        #endregion

        #region Button Browse Custom
        private void btnCusBrowse_Click(object sender, EventArgs e)
        {
            if (rbnCustomFile.Checked)
            {
                OpenFileDialog cusFile = new OpenFileDialog();
                cusFile.Filter = "All Files|*.*";
                if (Properties.Settings.Default.RootFolder != "") cusFile.InitialDirectory = Properties.Settings.Default.RootFolder;
                if (cusFile.ShowDialog() == DialogResult.Cancel) goto toss;
                tbxCustom.Text = cusFile.FileName;

                dgvCusTable.Rows.Clear();
                dgvCusTable.Refresh();
                verify:
                byte[] fileCheck = System.IO.File.ReadAllBytes(tbxCustom.Text);
                if (fileCheck[0] == 'Y' && fileCheck[1] == 'a' && fileCheck[2] == 'z' && fileCheck[3] == '0') // if Yaz0 encoded, ask if they want to decode it
                {
                    DialogResult diagResult = MessageBox.Show("This file is encoded!" + "\n\n" + "Do you want to decode?\nIt will create a seperate file automatically", "Yaz0 Encoded file...", MessageBoxButtons.YesNo);
                    if (diagResult == DialogResult.No)
                    {
                        tbxCustom.Text = "";
                        goto toss;
                    }
                    string outFile = Yaz0.DecodeOutputFileRename(cusFile.FileName);
                    if (!Yaz0.Decode(cusFile.FileName, outFile))
                    {
                        MessageBox.Show("Decode error:" + "\n\n" + Yaz0.lerror);
                        tbxCustom.Text = "";
                        goto toss;
                    }
                    tbxCustom.Text = outFile;
                    goto verify;
                }
                if (("" + ((char)fileCheck[0]) + ((char)fileCheck[1]) + ((char)fileCheck[2]) + ((char)fileCheck[3])) != "SARC")
                {
                    MessageBox.Show("Not a SARC archive! Missing SARC header at 0x00" + "\n" + "( Your file header is: " + ((char)fileCheck[0]) + ((char)fileCheck[1]) + ((char)fileCheck[2]) + ((char)fileCheck[3]) + " )");
                    tbxCustom.Text = "";
                    goto toss;
                }

                int nodeCount = SARC.GetFileNodeCount(tbxCustom.Text);
                string[] nodeTypes = SARC.GetFileNodeType(tbxCustom.Text);
                uint[] nodeSizes = SARC.GetFileNodeSizes(tbxCustom.Text);
                string[] nodePaths = SARC.GetFileNodePaths(tbxCustom.Text);
                uint[] nodePaddings = SARC.GetFileNodePaddings(tbxCustom.Text);

                for (int i = 0; i < nodeCount; i++)
                {
                    dgvCusTable.Rows.Add();
                    dgvCusTable.Rows[i].Cells[0].Value = i + 1;
                    dgvCusTable.Rows[i].Cells[1].Value = nodeTypes[i];
                    dgvCusTable.Rows[i].Cells[2].Value = nodeSizes[i];
                    dgvCusTable.Rows[i].Cells[3].Value = nodeSizes[i].ToString("X");
                    dgvCusTable.Rows[i].Cells[4].Value = nodePaths[i];
                    dgvCusTable.Rows[i].Cells[5].Value = nodePaddings[i];
                }

                toss:
                cusFile.Dispose();
                GC.Collect();
            }
            else if (rbnCustomFolder.Checked)
            {
                CommonOpenFileDialog cusFolder = new CommonOpenFileDialog();
                cusFolder.IsFolderPicker = true;
                if (Properties.Settings.Default.RootFolder != "") cusFolder.InitialDirectory = Properties.Settings.Default.RootFolder;
                if (cusFolder.ShowDialog() == CommonFileDialogResult.Cancel) goto toss;
                tbxCustom.Text = cusFolder.FileName;

                dgvCusTable.Rows.Clear();
                dgvCusTable.Refresh();
                string[] cusFolderFiles = System.IO.Directory.GetFiles(cusFolder.FileName == "" ? System.Environment.CurrentDirectory : cusFolder.FileName, "*.*", System.IO.SearchOption.AllDirectories);
                int nodeCount = cusFolderFiles.Length;
                if (nodeCount > 1000)
                {
                    MessageBox.Show("Too many files (1000+)!\n\nI doubt you ment to select this folder...\n" + tbxCustom.Text + "\n\nTry again");
                    tbxCustom.Text = "";
                    goto toss;
                }
                uint[] nodeSizes = SARC.GetFolderFileSizes(cusFolder.FileName);
                string[] nodeTypes = SARC.GetFolderFileTypes(cusFolder.FileName);
                string[] nodePaths = SARC.GetFolderFilePaths(cusFolder.FileName);

                for (int i = 0; i < nodeCount; i++)
                {
                    dgvCusTable.Rows.Add();
                    dgvCusTable.Rows[i].Cells[0].Value = i + 1;
                    dgvCusTable.Rows[i].Cells[1].Value = nodeTypes[i];
                    dgvCusTable.Rows[i].Cells[2].Value = nodeSizes[i];
                    dgvCusTable.Rows[i].Cells[3].Value = nodeSizes[i].ToString("X");
                    dgvCusTable.Rows[i].Cells[4].Value = nodePaths[i];
                }

                toss:
                cusFolder.Dispose();
                GC.Collect();
            }
        }
        #endregion

        #region Button Compare
        private void btnCompare_Click(object sender, EventArgs e)
        {
            bool changesFound = false;

            ResetTableColor(dgvCusTable);
            ResetTableColor(dgvOriTable);
            if (dgvOriTable.Rows.Count == 0 || dgvCusTable.Rows.Count == 0)
            {
                MessageBox.Show("Both tables don't have data yet!");
                goto toss;
            }
            if (dgvOriTable.Rows.Count != dgvCusTable.Rows.Count)
            {
                MessageBox.Show("Mismatching node count!" + "\n\n" + "Please check which files are missing or are extras");
                SetTableColorRed(dgvCusTable);
                SetTableColorRed(dgvOriTable);
                goto toss;
            }


            for (int i = 0; i < dgvOriTable.Rows.Count; i++)
            {
                if (dgvCusTable.Rows[i].Cells[1].Value.ToString() != dgvOriTable.Rows[i].Cells[1].Value.ToString()) //compare node types
                {
                    dgvCusTable.Rows[i].Cells[1].Style.BackColor = Color.Yellow;
                    dgvOriTable.Rows[i].Cells[1].Style.BackColor = Color.Yellow;
                    changesFound = true;
                }
                if (dgvCusTable.Rows[i].Cells[2].Value.ToString() != dgvOriTable.Rows[i].Cells[2].Value.ToString()) //compare node sizes
                {
                    dgvCusTable.Rows[i].Cells[2].Style.BackColor = Color.Yellow;
                    dgvOriTable.Rows[i].Cells[2].Style.BackColor = Color.Yellow;
                    changesFound = true;
                }
                if (rbnCustomFile.Checked)
                {
                    if ((dgvCusTable.Rows[i].Cells[5].Value.ToString() != dgvOriTable.Rows[i].Cells[5].Value.ToString())) //compare node sizes
                    {
                        dgvCusTable.Rows[i].Cells[5].Style.BackColor = Color.Yellow;
                        dgvOriTable.Rows[i].Cells[5].Style.BackColor = Color.Yellow;
                        changesFound = true;
                    }
                }
            }

            if (changesFound)
                MessageBox.Show("Check the highlighted values!");
            else
                MessageBox.Show("Both are the same!");

            toss:
            GC.Collect();
        }
        #endregion

        private void ResetTableColor(DataGridView table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int j = 0; j < table.ColumnCount; j++)
                {
                    table.Rows[i].Cells[j].Style.BackColor = dgvOriTable.DefaultCellStyle.BackColor;
                }

            }
        }

        private void SetTableColorRed(DataGridView table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int j = 0; j < table.ColumnCount; j++)
                {
                    table.Rows[i].Cells[j].Style.BackColor = Color.Red;
                }
            }
        }

        internal static bool FileOrDirectoryExists(string name)
        {
            return (Directory.Exists(name) || File.Exists(name));
        }

        private void rbnCustomFile_CheckedChanged(object sender, EventArgs e)
        {
            tbxCustom.Text = "";
            dgvCusTable.Rows.Clear();
            dgvCusTable.Refresh();
            ResetTableColor(dgvOriTable);
        }
    }
}