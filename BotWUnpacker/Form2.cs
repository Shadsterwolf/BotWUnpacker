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
            if (fileCheck[0] != 'S' && fileCheck[1] != 'A' && fileCheck[2] != 'R' && fileCheck[3] != 'C')
            {
                MessageBox.Show("Not a SARC archive! Missing SARC header at 0x00" + "\n" + "( Your file header is: " + ((char)fileCheck[0]) + ((char)fileCheck[1]) + ((char)fileCheck[2]) + ((char)fileCheck[3]) + " )");
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
                dgvOriTable.Rows[i].Cells[3].Value = nodePaths[i];
                dgvOriTable.Rows[i].Cells[4].Value = nodePaddings[i];
            }
            
            toss:
            oriFile.Dispose();
            GC.Collect();
        }
        #endregion

        #region Button Browse Custom
        private void btnCusBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog cusFolder = new FolderBrowserDialog();
            if (Properties.Settings.Default.RootFolder != "") cusFolder.SelectedPath = Properties.Settings.Default.RootFolder;
            cusFolder.Description = "Select Custom Folder";
            if (cusFolder.ShowDialog() == DialogResult.Cancel) goto toss;
            tbxCusFolder.Text = cusFolder.SelectedPath;

            dgvCusTable.Rows.Clear();
            dgvCusTable.Refresh();
            string[] cusFolderFiles = System.IO.Directory.GetFiles(cusFolder.SelectedPath == "" ? System.Environment.CurrentDirectory : cusFolder.SelectedPath, "*.*", System.IO.SearchOption.AllDirectories);
            int nodeCount = cusFolderFiles.Length;
            if (nodeCount > 1000)
            {
                MessageBox.Show("Too many files (1000+)!\n\nI doubt you ment to select this folder...\n" + tbxCusFolder.Text + "\n\nTry again");
                tbxCusFolder.Text = "";
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
                dgvCusTable.Rows[i].Cells[3].Value = nodePaths[i];
            }

            toss:
            cusFolder.Dispose();
            GC.Collect();
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
            if (tbxCusFolder.Text == "")
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
            if (FileOrDirectoryExists(tbxCusFolder.Text))
                cusFolder.SelectedPath = tbxCusFolder.Text;
            else
            {
                MessageBox.Show("Custom Folder has an invalid path:\n\n" + tbxCusFolder.Text);
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
            if (numFiles > 50)
            {
                DialogResult diagResult = MessageBox.Show("Hold up, you got " + numFiles + " files! \n\n" + "You sure you want to build? \nIt will take some time...", "Large Number of Files!", MessageBoxButtons.YesNo);
                if (diagResult == DialogResult.No)
                    goto toss;
            }

            //Save file path
            sFile.Filter = "PACK|*.pack|SARC|*.sarc|SSARC|*.ssarc|RARC|*.rarc|SGENVB|*.sgenvb|SBFARC|*.sbfarc|SBLARC|*.sblarc|SBACTORPACK|*sbactorpack|All Files|*.*";
            sFile.InitialDirectory = cusFolder.SelectedPath.Remove(cusFolder.SelectedPath.LastIndexOf("\\")); //Previous folder, as selected is to build outside of it.
            sFile.FileName = System.IO.Path.GetFileName(cusFolder.SelectedPath);
            lblProcessStatus.Visible = true;
            if (sFile.ShowDialog() == DialogResult.Cancel) goto toss;


            if (!PACK.CompareAndBuild(oriFile.FileName, cusFolder.SelectedPath, sFile.FileName))
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
                        if (dgvCusTable.Rows[i].Cells[3].Value.ToString() != dgvOriTable.Rows[i].Cells[3].Value.ToString()) //compare node path
                        {
                            ColorRowRed(dgvCusTable, i);
                            ColorRowRed(dgvOriTable, i);
                        }
                        else
                        {
                            if (dgvCusTable.Rows[i].Cells[1].Value.ToString() != dgvOriTable.Rows[i].Cells[1].Value.ToString()) //compare node types
                            {
                                dgvCusTable.Rows[i].Cells[1].Style.BackColor = Color.Yellow;
                                dgvOriTable.Rows[i].Cells[1].Style.BackColor = Color.Yellow;
                            }
                            if (dgvCusTable.Rows[i].Cells[2].Value.ToString() != dgvOriTable.Rows[i].Cells[2].Value.ToString()) //compare node sizes
                            {
                                dgvCusTable.Rows[i].Cells[2].Style.BackColor = Color.Yellow;
                                dgvOriTable.Rows[i].Cells[2].Style.BackColor = Color.Yellow;
                            }
                        }
                    }
                    else
                    {
                        ColorRowRed(dgvOriTable, i); //More original nodes than custom
                    }
                } 
                else
                {
                    ColorRowRed(dgvCusTable, i); //More custom nodes than original
                }

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
    }
}
