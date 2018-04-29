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

namespace BotWUnpacker
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        #region Button Browse Original
        private void btnOriBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog oriFile = new OpenFileDialog();
            oriFile.Filter = "PACK File (*.pack *.sarc *.ssarc *.rarc *.sgenvb *.sbfarc *.sblarc *.sbactorpack)|*.pack; *.sarc; *.ssarc; *.rarc; *.sgenvb; *.sbfarc; *.sblarc; *.sbactorpack|All Files|*.*";
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
                string outFile = Path.GetDirectoryName(oriFile.FileName) + "\\" + Path.GetFileNameWithoutExtension(oriFile.FileName) + "Decoded" + Path.GetExtension(oriFile.FileName);
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

            int nodeCount = PACK.GetFileNodeCount(tbxOriFile.Text);
            string[] nodeTypes = PACK.GetFileNodeType(tbxOriFile.Text);
            uint[] nodeSizes = PACK.GetFileNodeSizes(tbxOriFile.Text);
            string[] nodePaths = PACK.GetFileNodePaths(tbxOriFile.Text);
            uint[] nodePaddings = PACK.GetFileNodePaddings(tbxOriFile.Text);

            for (int i = 0; i < nodeCount; i++)
            {
                dgvOriTable.Rows.Add();
                dgvOriTable.Rows[i].Cells[0].Value = i+1;
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
        #endregion

        #region Button Browse Custom
        private void btnCusBrowse_Click(object sender, EventArgs e)
        {
            if (rbnCustomFile.Checked)
            {
                OpenFileDialog cusFile = new OpenFileDialog();
                cusFile.Filter = "PACK File (*.pack *.sarc *.ssarc *.rarc *.sgenvb *.sbfarc *.sblarc *.sbactorpack)|*.pack; *.sarc; *.ssarc; *.rarc; *.sgenvb; *.sbfarc; *.sblarc; *.sbactorpack|All Files|*.*";
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
                    string outFile = Path.GetDirectoryName(cusFile.FileName) + "\\" + Path.GetFileNameWithoutExtension(cusFile.FileName) + "Decoded" + Path.GetExtension(cusFile.FileName);
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

                int nodeCount = PACK.GetFileNodeCount(tbxCustom.Text);
                string[] nodeTypes = PACK.GetFileNodeType(tbxCustom.Text);
                uint[] nodeSizes = PACK.GetFileNodeSizes(tbxCustom.Text);
                string[] nodePaths = PACK.GetFileNodePaths(tbxCustom.Text);
                uint[] nodePaddings = PACK.GetFileNodePaddings(tbxCustom.Text);

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
                FolderBrowserDialog cusFolder = new FolderBrowserDialog();
                if (Properties.Settings.Default.RootFolder != "") cusFolder.SelectedPath = Properties.Settings.Default.RootFolder;
                cusFolder.Description = "Select Custom Folder";
                if (cusFolder.ShowDialog() == DialogResult.Cancel) goto toss;
                tbxCustom.Text = cusFolder.SelectedPath;

                dgvCusTable.Rows.Clear();
                dgvCusTable.Refresh();
                string[] cusFolderFiles = System.IO.Directory.GetFiles(cusFolder.SelectedPath == "" ? System.Environment.CurrentDirectory : cusFolder.SelectedPath, "*.*", System.IO.SearchOption.AllDirectories);
                int nodeCount = cusFolderFiles.Length;
                if (nodeCount > 1000)
                {
                    MessageBox.Show("Too many files (1000+)!\n\nI doubt you ment to select this folder...\n" + tbxCustom.Text + "\n\nTry again");
                    tbxCustom.Text = "";
                    goto toss;
                }
                uint[] nodeSizes = PACK.GetFolderFileSizes(cusFolder.SelectedPath);
                string[] nodeTypes = PACK.GetFolderFileTypes(cusFolder.SelectedPath);
                string[] nodePaths = PACK.GetFolderFilePaths(cusFolder.SelectedPath);

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

        #region Button Build
        private void btnBuild_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog cusFolder = new FolderBrowserDialog();
            OpenFileDialog oriFile = new OpenFileDialog();
            SaveFileDialog sFile = new SaveFileDialog();

            //Verify files and directories
            if (tbxOriFile.Text == "")
            {
                MessageBox.Show("Original file path missing!");
                goto toss;
            }
            if (tbxCustom.Text == "")
            {
                MessageBox.Show("Custom folder path missing!");
                goto toss;
            }
            if (FileOrDirectoryExists(tbxOriFile.Text))
                oriFile.FileName = tbxOriFile.Text;
            else
            {
                MessageBox.Show("Original File has an invalid path:\n\n" + tbxOriFile.Text);
                goto toss;
            }
            if (FileOrDirectoryExists(tbxCustom.Text))
                cusFolder.SelectedPath = tbxCustom.Text;
            else
            {
                MessageBox.Show("Custom Folder has an invalid path:\n\n" + tbxCustom.Text);
                goto toss;
            }

            int oriNodeCount = PACK.GetFileNodeCount(oriFile.FileName);
            string[] cusFolderFiles = System.IO.Directory.GetFiles(cusFolder.SelectedPath == "" ? System.Environment.CurrentDirectory : cusFolder.SelectedPath, "*.*", System.IO.SearchOption.AllDirectories);
            if (oriNodeCount != cusFolderFiles.Length)
            {
                MessageBox.Show("Number of nodes do not match!\n\n" + "Original file nodes: " + oriNodeCount + "\nCustom Folder nodes: " + cusFolderFiles.Length);
                goto toss;
            }
            int numFiles = Directory.GetFiles(cusFolder.SelectedPath, "*", SearchOption.AllDirectories).Length;

            //Save file path
            sFile.Filter = "PACK|*.pack|SARC|*.sarc|SSARC|*.ssarc|RARC|*.rarc|SGENVB|*.sgenvb|SBFARC|*.sbfarc|SBLARC|*.sblarc|SBACTORPACK|*sbactorpack|All Files|*.*";
            sFile.InitialDirectory = cusFolder.SelectedPath.Remove(cusFolder.SelectedPath.LastIndexOf("\\")); //Previous folder, as selected is to build outside of it.
            sFile.FileName = System.IO.Path.GetFileName(cusFolder.SelectedPath);
            lblProcessStatus.Visible = true;
            if (sFile.ShowDialog() == DialogResult.Cancel) goto toss;

            if (!PACK.CompareAndBuild(oriFile.FileName, cusFolder.SelectedPath, sFile.FileName, true))
                MessageBox.Show("Failed to build!" + "\n\n" + PACK.lerror);
            else
                MessageBox.Show("Done!\n\n" + sFile.FileName);

            toss:
            cusFolder.Dispose();
            oriFile.Dispose();
            sFile.Dispose();
            GC.Collect();
            lblProcessStatus.Visible = false;
        }
        #endregion

        #region Button Compare
        private void btnCompare_Click(object sender, EventArgs e)
        {
            bool changesFound = false;
            bool errorsFound = false;

            ResetTableColor(dgvCusTable);
            ResetTableColor(dgvOriTable);
            if (dgvOriTable.Rows.Count == 0 || dgvCusTable.Rows.Count == 0)
            {
                MessageBox.Show("Both tables don't have data yet!");
                goto toss;
            }

            
            for (int i = 0; i < dgvCusTable.Rows.Count; i++)
            {
                if (((int)dgvCusTable.Rows[i].Cells[0].Value - 1) < dgvOriTable.Rows.Count) //compare number of nodes
                {
                    if (((int)dgvOriTable.Rows[i].Cells[0].Value - 1) < dgvCusTable.Rows.Count) //compare number of nodes
                    {
                        if (dgvCusTable.Rows[i].Cells[4].Value.ToString() != dgvOriTable.Rows[i].Cells[4].Value.ToString()) //compare node path
                        {
                            ColorRowRed(dgvCusTable, i);
                            ColorRowRed(dgvOriTable, i);
                            MessageBox.Show("Error found" + "\n\n" + "You have files in the wrong path!");
                        }
                        else
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
                    }
                    else
                    {
                        ColorRowRed(dgvOriTable, i); //More original nodes than custom
                        MessageBox.Show("Error found" + "\n\n" + "You have LESS files than the original!");
                    }
                } 
                else
                {
                    ColorRowRed(dgvCusTable, i); //More custom nodes than original
                    MessageBox.Show("Error found" + "\n\n" + "You have MORE files than the original!");
                }
            }

            if (errorsFound)
                btnBuild.Enabled = false;
            else
            {
                if (changesFound)
                {
                    MessageBox.Show("Done" + "\n\n" + "Check the highlighted values!");
                }
                else
                {
                    MessageBox.Show("Done" + "\n\n" + "Appears both are the same!");
                }

                if (rbnCustomFolder.Checked)
                    btnBuild.Enabled = true;
            }

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
                    table.Rows[i].Cells[j].Style.BackColor = Color.White;
                }

            }
        }

        private void ColorRowRed(DataGridView table, int row)
        {
            for (int i = 0; i < table.ColumnCount; i++)
            {
                table.Rows[row].Cells[i].Style.BackColor = Color.Red;
            }
        }

        internal static bool FileOrDirectoryExists(string name)
        {
            return (Directory.Exists(name) || File.Exists(name));
        }

        private void rbnCustomFile_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnCustomFile.Checked)
            {
                btnBuild.Enabled = false;
            }
            else
            {
                btnBuild.Enabled = true;
            }
            tbxCustom.Text = "";
            dgvCusTable.Rows.Clear();
            dgvCusTable.Refresh();
        }
    }
}
