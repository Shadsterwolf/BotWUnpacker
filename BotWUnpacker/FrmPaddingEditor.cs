using System;
using System.Drawing;
using System.Windows.Forms;

namespace BotwUnpacker
{
    public partial class FrmPaddingEditor : Form
    {
        public FrmPaddingEditor()
        {
            InitializeComponent();
        }

        public int fileSize;
        public int dataOffset;
        public int lastCellValue;

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog inFile = new OpenFileDialog();
            inFile.Filter = "All Files|*.*";
            if (inFile.ShowDialog() == DialogResult.Cancel) goto toss;
            tbxFile.Text = inFile.FileName;

            btnSave.Enabled = true;
            dgvTable.Rows.Clear();
            dgvTable.Refresh();
            verify:
            byte[] fileCheck = System.IO.File.ReadAllBytes(tbxFile.Text);
            if (fileCheck[0] == 'Y' && fileCheck[1] == 'a' && fileCheck[2] == 'z' && fileCheck[3] == '0') // if Yaz0 encoded, ask if they want to decode it
            {
                DialogResult diagResult = MessageBox.Show("This file is encoded!" + "\n\n" + "Do you want to decode?\nIt will create a seperate file automatically", "Yaz0 Encoded file...", MessageBoxButtons.YesNo);
                if (diagResult == DialogResult.No)
                {
                    tbxFile.Text = "";
                    btnSave.Enabled = false;
                    goto toss;
                }
                string outFile = Yaz0.DecodeOutputFileRename(inFile.FileName);
                if (!Yaz0.Decode(inFile.FileName, outFile))
                {
                    MessageBox.Show("Decode error:" + "\n\n" + Yaz0.lerror);
                    tbxFile.Text = "";
                    btnSave.Enabled = false;
                    goto toss;
                }
                tbxFile.Text = outFile;
                goto verify;
            }
            if ((""+((char)fileCheck[0]) + ((char)fileCheck[1]) + ((char)fileCheck[2]) + ((char)fileCheck[3])) != "SARC")
            {
                MessageBox.Show("Not a SARC archive! Missing SARC header at 0x00" + "\n" + "( Your file header is: " + ((char)fileCheck[0]) + ((char)fileCheck[1]) + ((char)fileCheck[2]) + ((char)fileCheck[3]) + " )");
                tbxFile.Text = "";
                btnSave.Enabled = false;
                goto toss;
            }

            int nodeCount = SARC.GetFileNodeCount(tbxFile.Text);
            string[] nodeTypes = SARC.GetFileNodeType(tbxFile.Text);
            uint[] nodeSizes = SARC.GetFileNodeSizes(tbxFile.Text);
            string[] nodePaths = SARC.GetFileNodePaths(tbxFile.Text);
            uint[] nodePaddings = SARC.GetFileNodePaddings(tbxFile.Text);

            for (int i = 0; i < nodeCount; i++)
            {
                dgvTable.Rows.Add();
                dgvTable.Rows[i].Cells[0].Value = i + 1;
                dgvTable.Rows[i].Cells[1].Value = nodePaddings[i];
                dgvTable.Rows[i].Cells[2].Value = nodeTypes[i];
                dgvTable.Rows[i].Cells[3].Value = nodeSizes[i];
                dgvTable.Rows[i].Cells[4].Value = nodeSizes[i].ToString("X");
                dgvTable.Rows[i].Cells[5].Value = nodePaths[i];
            }

            fileSize = fileCheck.Length;
            tbxFileSize.Text = fileSize.ToString();
            dataOffset = SARC.GetFileDataOffset(tbxFile.Text);
            tbxDataOffset.Text = "0x" + dataOffset.ToString("X");

            toss:
            inFile.Dispose();
            GC.Collect();
        }
        private void dgvTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(dgvTable.CurrentCell.Value) < 0)
                {
                    MessageBox.Show("Padding cannot be negative!");
                    dgvTable.CurrentCell.Value = lastCellValue;
                }
                else
                {
                    if ((Convert.ToInt32(dgvTable.CurrentCell.Value) % 2) != 0)
                        if (MessageBox.Show("You sure you want an odd number?" + "\n\n" + "Nintendo likes even numbers, expecially multiples of 4", "You sure?", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            dgvTable.CurrentCell.Value = lastCellValue;
                        }
                    fileSize += Convert.ToInt32(dgvTable.CurrentCell.Value) - Convert.ToInt32(lastCellValue);
                    tbxFileSize.Text = fileSize.ToString();
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
                dgvTable.CurrentCell.Value = lastCellValue;
            }

            if (lastCellValue != Convert.ToInt32(dgvTable.CurrentCell.Value))
                dgvTable.CurrentCell.Style.BackColor = Color.Yellow;

        }

        private void dgvTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                lastCellValue = Convert.ToInt32(dgvTable.CurrentCell.Value);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbxFile.Text == "")
                MessageBox.Show("Whoops, browse for a file first!");
            else
            {
                int nodeCount = SARC.GetFileNodeCount(tbxFile.Text);
                uint[] currentPaddings = SARC.GetFileNodePaddings(tbxFile.Text);
                int[] gridPaddings = new int[nodeCount];
                bool dataChanged = false;

                for (int i = 0; i < nodeCount; i++)
                {
                    gridPaddings[i] = Convert.ToInt32(dgvTable.Rows[i].Cells[1].Value);

                    if (gridPaddings[i] != currentPaddings[i]) //check if paddings need to be changed
                    {
                        SARC.UpdateFilePadding(tbxFile.Text, i, (gridPaddings[i] - (int)(currentPaddings[i])));
                        dataChanged = true;
                    }
                }

                if (dataChanged)
                {
                    SARC.UpdateFileSize(tbxFile.Text);
                    MessageBox.Show("Save Complete!" + "\n\n" + tbxFile.Text);
                }
                else //if no change occured in the data upon saving
                {
                    MessageBox.Show("No changes were made!" + "\n" + "Data matches current file");
                }
                for (int i = 0; i < dgvTable.Rows.Count; i++) //reset color
                    dgvTable.Rows[i].Cells[1].Style.BackColor = Color.White;
            }
        }
    }
}
