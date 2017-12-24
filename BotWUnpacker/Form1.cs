using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BotWUnpacker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Load in previous instance
            tbxFolderRoot.Text = Properties.Settings.Default.RootFolder;
            if (tbxFolderRoot.Text != "") btnExtractAll.Enabled = true;
            if (tbxFolderRoot.Text != "") btnOpenFolder.Enabled = true;
        }

        #region Button Browse Root 
        private void btnBrowseRoot_Click(object sender, EventArgs e) //Browse Root button
        {
            FolderBrowserDialog oFolder = new FolderBrowserDialog();
            if (tbxFolderRoot.Text != "")
                oFolder.SelectedPath = tbxFolderRoot.Text;
            oFolder.Description = "Select a game root folder";
            if (oFolder.ShowDialog() == DialogResult.Cancel) goto toss;
            tbxFolderRoot.Text = oFolder.SelectedPath;
            btnExtractAll.Enabled = true;
            btnOpenFolder.Enabled = true;

            //Save GameRoot property
            Properties.Settings.Default.RootFolder = oFolder.SelectedPath; 
            Properties.Settings.Default.Save();

            toss:
            oFolder.Dispose();
        }
        #endregion

        #region Button Clear 
        private void btnClearRoot_Click(object sender, EventArgs e)
        {
            tbxFolderRoot.Text = "";
            Properties.Settings.Default.RootFolder = "";
            Properties.Settings.Default.Save();
            btnExtractAll.Enabled = false;
            btnOpenFolder.Enabled = false;
        }
        #endregion

        #region Button Extract Pack
        private void btnExtractPack_Click(object sender, EventArgs e) //Extract Pack button
        {
            OpenFileDialog oFile = new OpenFileDialog();
            oFile.Filter = "PACK File (*.pack *.sarc *.ssarc *.rarc *.sgenvb *.sbfarc *.sblarc *.sbactorpack)|*.pack; *.sarc; *.ssarc; *.rarc; *.sgenvb; *.sbfarc; *.sblarc; *.sbactorpack|All Files|*.*";
            if (tbxFolderRoot.Text != "") oFile.InitialDirectory = tbxFolderRoot.Text;
            if (oFile.ShowDialog() == DialogResult.Cancel) goto toss;

            String oFolderName = Path.GetFileNameWithoutExtension(oFile.FileName);
            String oFolderPath = Path.GetDirectoryName(oFile.FileName) + "\\" + oFolderName;

            // if the output folder exists, prompt user if they want to extract anyway
            if (Directory.Exists(oFolderPath))
                if (MessageBox.Show(oFolderName + " already exists!" + "\n\n" + "Proceed anyway?", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.No) goto xml;

            //SARC
            lblProcessStatus.Visible = true;
            bool boolAutoDecode = false;
            bool boolReplaceFile = false;
            if (cbxAutoDecode.Checked) //Auto Yaz0 decoding
            {
                boolAutoDecode = true;
                if (cbxReplaceDecodedFile.Checked) //Replace File
                    boolReplaceFile = true;
            }
            if (!PACK.Extract(oFile.FileName, oFolderPath, boolAutoDecode, boolReplaceFile)) //Extraction
            {
                MessageBox.Show("Extraction error:" + "\n\n" + PACK.lerror);
                goto toss;
            }
            else
                System.Diagnostics.Process.Start(oFolderPath);

            //XML
            xml:
            if (cbxWriteXml.Checked)
            {
                if (File.Exists(oFolderPath + ".xml"))
                    if (MessageBox.Show(oFolderName + ".xml already exists!" + "\n\n" + "Proceed anyway?", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.No) goto toss;
                if (!XMLWriter.SaveXml(oFile.FileName, (oFolderPath + ".xml")))
                    MessageBox.Show("XML file failed to write by unknown reasons");
            }

            toss:
            oFile.Dispose();
            GC.Collect();
            lblProcessStatus.Visible = false;
        }
        #endregion

        #region Button Extract All
        private void btnExtractAll_Click(object sender, EventArgs e)
        {
            if (!(Directory.Exists(tbxFolderRoot.Text))) {
                MessageBox.Show("Error: Invalid path" + "\n" + tbxFolderRoot.Text);
                goto toss;
            }
            if (cbxCompileAllInOneFolder.Checked) { if (MessageBox.Show("Extract all SARC data type files from default path?" + "\n" + tbxFolderRoot.Text + "\n\n" + "You are choosing to compile all extracted data to one folder!" + "\n" + "This does not include subfolders", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.No) goto toss; }
            else { if (MessageBox.Show("Extract all SARC data type files from default path?" + "\n" + tbxFolderRoot.Text + "\n\n" + "This will generate seperate folders of every file it unpacks" + "\n" + "This does not include subfolders", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.No) goto toss; }
            lblProcessStatus.Visible = true;

            DirectoryInfo dirFolder = new DirectoryInfo(tbxFolderRoot.Text);


            String oFolderName, oFolderPath;
            int sarcFileCount = 0;
            bool boolAutoDecode = false;
            bool boolReplaceFile = false;
            if (cbxAutoDecode.Checked) //Auto Yaz0 decoding
            {
                boolAutoDecode = true;
                if (cbxReplaceDecodedFile.Checked) //Replace File
                    boolReplaceFile = true;
            }

            if (cbxCompileAllInOneFolder.Checked) //Compile All to New Folder
            {
                FolderBrowserDialog oFolder = new FolderBrowserDialog();
                if (tbxFolderRoot.Text != "") oFolder.SelectedPath = tbxFolderRoot.Text;
                if (oFolder.ShowDialog() == DialogResult.Cancel) goto toss;
                foreach (FileInfo file in dirFolder.GetFiles()) //Extraction
                {
                    oFolderName = Path.GetFileNameWithoutExtension(file.FullName);
                    oFolderPath = oFolder.SelectedPath;
                    if (PACK.Extract(file.FullName, oFolderPath, boolAutoDecode, boolReplaceFile))
                        sarcFileCount++;
                }

            }
            else
            {
                foreach (FileInfo file in dirFolder.GetFiles()) //Extraction
                {
                    oFolderName = Path.GetFileNameWithoutExtension(file.FullName);
                    oFolderPath = Path.GetDirectoryName(file.FullName) + "\\" + oFolderName;
                    if (PACK.Extract(file.FullName, oFolderPath, boolAutoDecode, boolReplaceFile))
                        sarcFileCount++;
                }
            }

            MessageBox.Show("Done" + "\n\n" + sarcFileCount + " file(s) extracted!");

            toss:
            GC.Collect();
            lblProcessStatus.Visible = false;
        }
        #endregion

        #region Button Yaz0 Decode
        private void btnYaz0Decode_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFile = new OpenFileDialog();
            oFile.Filter = "All Files|*.*";
            if (tbxFolderRoot.Text != "") oFile.InitialDirectory = tbxFolderRoot.Text;
            lblProcessStatus.Visible = true;
            if (oFile.ShowDialog() == DialogResult.Cancel) goto toss;

            //Yaz0 Decode
            if (cbxReplaceDecodedFile.Checked)
            {
                if (!Yaz0.Decode(oFile.FileName, oFile.FileName))
                {
                    MessageBox.Show("Decode error:" + "\n\n" + Yaz0.lerror);
                    goto toss;
                }
            }
            else
            {
                string outFile = Path.GetDirectoryName(oFile.FileName) + "\\" + Path.GetFileNameWithoutExtension(oFile.FileName) + "Decoded" + Path.GetExtension(oFile.FileName);
                if (!Yaz0.Decode(oFile.FileName, outFile))
                {
                    MessageBox.Show("Decode error:" + "\n\n" + Yaz0.lerror);
                    goto toss;
                }
            }

            toss:
            oFile.Dispose();
            GC.Collect();
            lblProcessStatus.Visible = false;
        }
        #endregion

        #region Button Build Pack 
        private void btnBuildPack_Click(object sender, EventArgs e) // Build Pack button
        {
            FolderBrowserDialog oFolder = new FolderBrowserDialog();
            SaveFileDialog sFile = new SaveFileDialog();

            if (cbxSetDataOffset.Checked)
            {
                try{int.Parse(tbxDataOffset.Text, System.Globalization.NumberStyles.HexNumber);} //check if it's a hex value (textbox limited to 4 characters)
                catch{MessageBox.Show("Error:" + "\n\n" + "Fixed Data Offset is not a hex value!"); goto toss;}
            }
            oFolder.Description = "Select folder to build into Pack file";
            if (tbxFolderRoot.Text != "") oFolder.SelectedPath = tbxFolderRoot.Text;
            if (oFolder.ShowDialog() == DialogResult.Cancel) goto toss;
            sFile.Filter = "PACK|*.pack|SARC|*.sarc|SSARC|*.ssarc|RARC|*.rarc|SGENVB|*.sgenvb|SBFARC|*.sbfarc|SBLARC|*.sblarc|SBACTORPACK|*sbactorpack|All Files|*.*";
            sFile.InitialDirectory = oFolder.SelectedPath.Remove(oFolder.SelectedPath.LastIndexOf("\\")); //Previous folder, as selected is to build outside of it.
            sFile.FileName = System.IO.Path.GetFileName(oFolder.SelectedPath);
            lblProcessStatus.Visible = true;
            if (sFile.ShowDialog() == DialogResult.Cancel) goto toss;

            if (cbxSetDataOffset.Checked)
            {
                uint dataOffset = (uint)int.Parse(tbxDataOffset.Text, System.Globalization.NumberStyles.HexNumber);
                if (!PACK.Build(oFolder.SelectedPath, sFile.FileName, dataOffset))
                    MessageBox.Show("Failed to build!" + "\n\n" + PACK.lerror);
            }
            else
            {
                if (!PACK.Build(oFolder.SelectedPath, sFile.FileName))
                    MessageBox.Show("Failed to build!" + "\n\n" + PACK.lerror);
            }

            toss:
            oFolder.Dispose();
            sFile.Dispose();
            GC.Collect();
            lblProcessStatus.Visible = false;
        }
        #endregion

        private void btnBuildCompare_Click(object sender, EventArgs e)
        {
            //TODO: make stuff work
        }

        private void cbxSetDataOffset_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxSetDataOffset.Checked)
            {
                tbxDataOffset.ReadOnly = false;
                tbxDataOffset.BackColor = SystemColors.Window;
            }
            else
            {
                tbxDataOffset.ReadOnly = true;
                tbxDataOffset.BackColor = SystemColors.ControlLight;
            }
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            try { System.Diagnostics.Process.Start(tbxFolderRoot.Text); }
            catch { MessageBox.Show("Error: Invalid path" + "\n" + tbxFolderRoot.Text); }
        }

        private void tbxFolderRoot_TextChanged(object sender, EventArgs e)
        {
            if (tbxFolderRoot.Text == "")
            {
                Properties.Settings.Default.RootFolder = "";
                Properties.Settings.Default.Save();
                btnExtractAll.Enabled = false;
                btnOpenFolder.Enabled = false;
            }
            else
            {
                btnExtractAll.Enabled = true;
                btnOpenFolder.Enabled = true;
                Properties.Settings.Default.RootFolder = tbxFolderRoot.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void cbxAutoDecode_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxAutoDecode.Checked)
            {
                cbxReplaceDecodedFile.Enabled = true;
            }
            else
            {
                cbxReplaceDecodedFile.Checked = false;
                cbxReplaceDecodedFile.Enabled = false;
            }
                
        }

        private void btnYaz0Encode_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFile = new OpenFileDialog();
            oFile.Filter = "All Files|*.*";
            if (tbxFolderRoot.Text != "") oFile.InitialDirectory = tbxFolderRoot.Text;
            lblProcessStatus.Visible = true;
            if (oFile.ShowDialog() == DialogResult.Cancel) goto toss;

            //Yaz0 Encode
            if (!Yaz0.Encode(oFile.FileName, oFile.FileName))
            {
                MessageBox.Show("Encode error:" + "\n\n" + Yaz0.lerror);
                goto toss;
            }

            toss:
            oFile.Dispose();
            GC.Collect();
            lblProcessStatus.Visible = false;
        }
    }
}
