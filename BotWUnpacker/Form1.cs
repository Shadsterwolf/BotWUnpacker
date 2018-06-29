using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace BotWUnpacker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Load in previous instance
            tbxFolderRoot.Text = BotwUnpacker.Properties.Settings.Default.RootFolder;
            if (tbxFolderRoot.Text != "") btnExtractAll.Enabled = true;
            if (tbxFolderRoot.Text != "") btnOpenFolder.Enabled = true;
            lblFootnote.Text = "Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString() + "\nMade by Shadsterwolf\nHeavily modified code from UWizard SARC";
        }


        #region Button Browse Root 
        private void btnBrowseRoot_Click(object sender, EventArgs e) //Browse Root button
        {
            CommonOpenFileDialog oFolder = new CommonOpenFileDialog();
            oFolder.IsFolderPicker = true;
            if (tbxFolderRoot.Text != "")
                oFolder.InitialDirectory = tbxFolderRoot.Text;
            if (oFolder.ShowDialog() == CommonFileDialogResult.Cancel) goto toss;
            tbxFolderRoot.Text = oFolder.FileName;
            btnExtractAll.Enabled = true;
            btnOpenFolder.Enabled = true;

            //Save GameRoot property
            BotwUnpacker.Properties.Settings.Default.RootFolder = oFolder.FileName;
            BotwUnpacker.Properties.Settings.Default.Save();

            toss:
            oFolder.Dispose();
        }
        #endregion

        #region Button Extract Pack
        private void btnExtractPack_Click(object sender, EventArgs e) //Extract Pack button
        {
            OpenFileDialog oFile = new OpenFileDialog();
            oFile.Filter = "All Files|*.*";
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
            if (cbxAutoDecode.Checked) //Auto Yaz0 decoding
                boolAutoDecode = true;
            else //Default, without any checkboxes
            {
                if (PACK.IsYaz0File(oFile.FileName))
                {
                    if (MessageBox.Show("This file is Yaz0 encoded!" + "\n\n" + "Want to decode it and attempt to extract?", "Decode and Extact?", MessageBoxButtons.YesNo) != DialogResult.No)
                        boolAutoDecode = true; //Auto decode just this once since we prompted
                    else
                        goto toss; //User clicked no
                }
            }
            if (!PACK.Extract(oFile.FileName, oFolderPath, boolAutoDecode)) //Extraction
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
            if (cbxAutoDecode.Checked) //Auto Yaz0 decoding
            {
                boolAutoDecode = true;
            }

            if (cbxCompileAllInOneFolder.Checked) //Compile All to New Folder
            {
                CommonOpenFileDialog oFolder = new CommonOpenFileDialog();
                oFolder.IsFolderPicker = true;
                if (tbxFolderRoot.Text != "") oFolder.InitialDirectory = tbxFolderRoot.Text;
                if (oFolder.ShowDialog() == CommonFileDialogResult.Cancel) goto toss;
                foreach (FileInfo file in dirFolder.GetFiles()) //Extraction
                {
                    oFolderName = Path.GetFileNameWithoutExtension(file.FullName);
                    oFolderPath = oFolder.FileName;
                    if (PACK.Extract(file.FullName, oFolderPath, boolAutoDecode))
                        sarcFileCount++;
                }

            }
            else
            {
                foreach (FileInfo file in dirFolder.GetFiles()) //Extraction
                {
                    oFolderName = Path.GetFileNameWithoutExtension(file.FullName);
                    oFolderPath = Path.GetDirectoryName(file.FullName) + "\\" + oFolderName;
                    if (PACK.Extract(file.FullName, oFolderPath, boolAutoDecode))
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
            string outFile = oFile.FileName;
            {
                if (!Yaz0.Decode(oFile.FileName, Yaz0.DecodeOutputFileRename(oFile.FileName)))
                {
                    MessageBox.Show("Decode error:" + "\n\n" + Yaz0.lerror);
                    goto toss;
                }
            }

            MessageBox.Show("Decode complete!" + "\n\n" + outFile);

            toss:
            oFile.Dispose();
            GC.Collect();
            lblProcessStatus.Visible = false;
        }
        #endregion

        #region Button Yaz0 Encode
        private void btnYaz0Encode_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFile = new OpenFileDialog();
            oFile.Filter = "All Files|*.*";
            if (tbxFolderRoot.Text != "") oFile.InitialDirectory = tbxFolderRoot.Text;
            lblProcessStatus.Visible = true;
            if (oFile.ShowDialog() == DialogResult.Cancel) goto toss;

            String outFile = Path.GetDirectoryName(oFile.FileName) + "\\" + Path.GetFileNameWithoutExtension(oFile.FileName) + "Encoded" + Path.GetExtension(oFile.FileName);
            outFile = outFile.Replace("Decoded", "");

            //Yaz0 Encode
            if (!Yaz0.Encode(oFile.FileName, outFile))
            {
                MessageBox.Show("Encode error:" + "\n\n" + Yaz0.lerror);
                goto toss;
            }

            MessageBox.Show("Encode complete!" + "\n\n" + oFile.FileName);

            toss:
            oFile.Dispose();
            GC.Collect();
            lblProcessStatus.Visible = false;
        }
        #endregion

        #region Button Build Pack 
        private void btnBuildPack_Click(object sender, EventArgs e) // Build Pack button
        {
            CommonOpenFileDialog oFolder = new CommonOpenFileDialog();
            oFolder.IsFolderPicker = true;
            SaveFileDialog sFile = new SaveFileDialog();

            if (cbxSetDataOffset.Checked)
            {
                try{int.Parse(tbxDataOffset.Text, System.Globalization.NumberStyles.HexNumber);} //check if it's a hex value (textbox limited to 4 characters)
                catch{MessageBox.Show("Error:" + "\n\n" + "Fixed Data Offset is not a hex value!"); goto toss;}
            }
            if (tbxFolderRoot.Text != "") oFolder.InitialDirectory = tbxFolderRoot.Text;
            if (oFolder.ShowDialog() == CommonFileDialogResult.Cancel) goto toss;
            int numFiles = Directory.GetFiles(oFolder.FileName, "*", SearchOption.AllDirectories).Length;            

            sFile.Filter = "PACK|*.pack|SARC|*.sarc|SSARC|*.ssarc|RARC|*.rarc|SGENVB|*.sgenvb|SBFARC|*.sbfarc|SBLARC|*.sblarc|SBACTORPACK|*sbactorpack|All Files|*.*";
            sFile.InitialDirectory = oFolder.FileName.Remove(oFolder.FileName.LastIndexOf("\\")); //Previous folder, as selected is to build outside of it.
            sFile.FileName = System.IO.Path.GetFileName(oFolder.FileName);
            lblProcessStatus.Visible = true;
            if (sFile.ShowDialog() == DialogResult.Cancel) goto toss;

            if (cbxSetDataOffset.Checked)
            {
                uint dataOffset = (uint)int.Parse(tbxDataOffset.Text, System.Globalization.NumberStyles.HexNumber);
                if (!PACK.Build(oFolder.FileName, sFile.FileName, dataOffset))
                {
                    MessageBox.Show("Failed to build!" + "\n\n" + PACK.lerror);
                    goto toss;
                }
            }
            else
            {
                if (!PACK.Build(oFolder.FileName, sFile.FileName))
                { 
                    MessageBox.Show("Failed to build!" + "\n\n" + PACK.lerror);
                    goto toss;
                }
            }

            MessageBox.Show("Building Complete!" + "\n\n" + sFile.FileName);

            toss:
            oFolder.Dispose();
            sFile.Dispose();
            GC.Collect();
            lblProcessStatus.Visible = false;
        }
        #endregion

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
                BotwUnpacker.Properties.Settings.Default.RootFolder = "";
                BotwUnpacker.Properties.Settings.Default.Save();
                btnExtractAll.Enabled = false;
                btnOpenFolder.Enabled = false;
            }
            else
            {
                btnExtractAll.Enabled = true;
                btnOpenFolder.Enabled = true;
                BotwUnpacker.Properties.Settings.Default.RootFolder = tbxFolderRoot.Text;
                BotwUnpacker.Properties.Settings.Default.Save();
            }
        }        
        
        Form2 form2 = new Form2();
        private void btnCompareAndBuild_Click(object sender, EventArgs e)
        {
            if (!(form2.Visible))
            {
                if (form2.IsDisposed)
                    form2 = new Form2();
                form2.StartPosition = FormStartPosition.Manual;
                form2.Location = new Point (Form1.ActiveForm.DesktopLocation.X + 380, Form1.ActiveForm.DesktopLocation.Y);
                form2.Show();
            }
            else
            {
                form2.Refresh();
                if (form2.WindowState == FormWindowState.Minimized)
                    form2.WindowState = FormWindowState.Normal;
                form2.BringToFront();
            }

        }

        Form3 form3 = new Form3();
        private void btnSarcEditor_Click(object sender, EventArgs e)
        {
            if (!(form3.Visible))
            {
                if (form3.IsDisposed)
                    form3 = new Form3();
                form3.StartPosition = FormStartPosition.Manual;
                form3.Location = new Point(Form1.ActiveForm.DesktopLocation.X + 380, Form1.ActiveForm.DesktopLocation.Y);
                form3.Show();
            }
            else
            {
                form3.Refresh();
                if (form3.WindowState == FormWindowState.Minimized)
                    form3.WindowState = FormWindowState.Normal;
                form3.BringToFront();
            }
        }

        ToolTip t1 = new ToolTip();




        private void cbxSetDataOffset_MouseHover(object sender, EventArgs e)
        {
            t1.Show("If you need to set where node data is", cbxSetDataOffset);
        }
    }
}
