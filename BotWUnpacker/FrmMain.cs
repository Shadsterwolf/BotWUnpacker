using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Threading;
using System.ComponentModel;

namespace BotwUnpacker
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            //Load in previous instance
            tbxFolderRoot.Text = BotwUnpacker.Properties.Settings.Default.RootFolder;
            if (tbxFolderRoot.Text != "") btnExtractAll.Enabled = true;
            if (tbxFolderRoot.Text != "") btnOpenFolder.Enabled = true;
            lblFootnote.Text = "Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString() + "." + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build.ToString() + "\nMade by Shadsterwolf\nHeavily modified code from UWizard SARC";
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
            if (tbxFolderRoot.Text != "") oFile.InitialDirectory = tbxFolderRoot.Text;
            if (oFile.ShowDialog() == DialogResult.Cancel) goto toss;

            String oFolderName = Path.GetFileNameWithoutExtension(oFile.FileName);
            String oFolderPath = Path.GetDirectoryName(oFile.FileName) + "\\" + oFolderName;

            // if the output folder exists, prompt user if they want to extract anyway
            if (Directory.Exists(oFolderPath))
                if (MessageBox.Show(oFolderName + " already exists!" + "\n\n" + "Proceed anyway?", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.No) goto xml;

            //SARC
            loadingBar.Visible = true;
            bool boolAutoDecode = false;
            bool boolNodeDecode = false;
            if (cbxNodeDecode.Checked) //Node Yaz0 decoding
                boolNodeDecode = true;
            else //Default, without any checkboxes
            {
                if (SARC.IsYaz0File(oFile.FileName))
                {
                    if (MessageBox.Show("This file is Yaz0 encoded!" + "\n\n" + "Want to decode it and attempt to extract?", "Decode and Extact?", MessageBoxButtons.YesNo) != DialogResult.No)
                        boolAutoDecode = true; //Auto decode just this once since we prompted
                    else
                        goto toss; //User clicked no
                }
            }
            if (!SARC.Extract(oFile.FileName, oFolderPath, boolAutoDecode, boolNodeDecode)) //Extraction
            {
                MessageBox.Show("Extraction error:" + "\n\n" + SARC.lerror);
                goto toss;
            }
            else
                System.Diagnostics.Process.Start(oFolderPath);

            //XML
            xml:
            if (cbxWriteSarcXml.Checked)
            {
                if (File.Exists(oFolderPath + "_SarcDebug.xml"))
                    if (MessageBox.Show(oFolderName + ".xml already exists!" + "\n\n" + "Proceed anyway?", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.No) goto toss;
                if (!DebugWriter.WriteSarcXml(oFile.FileName, (oFolderPath + "_SarcDebug.xml")))
                    MessageBox.Show("XML file failed to write by unknown reasons");
            }

            toss:
            oFile.Dispose();
            GC.Collect();
            loadingBar.Visible = false;
        }
        #endregion

        #region Button Extract All
        private void btnExtractAll_Click(object sender, EventArgs e)
        {
            if (tbxFolderRoot.Text == "")
            {
                MessageBox.Show("Browse for a default folder to unpack from first!");
                goto toss;
            }
            if (!(Directory.Exists(tbxFolderRoot.Text)))
            {
                MessageBox.Show("Error: Invalid path" + "\n" + tbxFolderRoot.Text);
                goto toss;
            }
            loadingBar.Visible = true;
            if (cbxCompileAllInOneFolder.Checked) { if (MessageBox.Show("Extract all SARC data type files from default path?" + "\n" + tbxFolderRoot.Text + "\n" + "*This does not include subfolders" + "\n\n" + "You are choosing to compile all extracted data to ONE folder!" + "\n" + "You'll then select a folder where you want to place them all in.", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.No) goto toss; }
            else { if (MessageBox.Show("Extract all SARC data type files from default path?" + "\n" + tbxFolderRoot.Text + "\n" + "*This does not include subfolders" + "\n\n" + "This will generate SEPERATE folders of every file it unpacks!" , "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.No) goto toss; }
            DirectoryInfo dirFolder = new DirectoryInfo(tbxFolderRoot.Text);


            String oFolderName, oFolderPath;
            int sarcFileCount = 0;
            bool boolAutoDecode = false;
            bool boolNodeDecode = false;
            if (cbxNodeDecode.Checked) //Auto Yaz0 decoding
            {
                boolNodeDecode = true;
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
                    if (SARC.Extract(file.FullName, oFolderPath, boolAutoDecode, boolNodeDecode))
                        sarcFileCount++;
                }

            }
            else
            {
                foreach (FileInfo file in dirFolder.GetFiles()) //Extraction
                {
                    oFolderName = Path.GetFileNameWithoutExtension(file.FullName);
                    oFolderPath = Path.GetDirectoryName(file.FullName) + "\\" + oFolderName;
                    if (SARC.Extract(file.FullName, oFolderPath, boolAutoDecode, boolNodeDecode))
                        sarcFileCount++;
                }
            }

            MessageBox.Show("Done" + "\n\n" + sarcFileCount + " file(s) extracted!");

            toss:
            GC.Collect();
            loadingBar.Visible = false;
        }
        #endregion

        #region Button Yaz0 Decode
        private void btnYaz0Decode_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFile = new OpenFileDialog();
            if (tbxFolderRoot.Text != "") oFile.InitialDirectory = tbxFolderRoot.Text;
            if (oFile.ShowDialog() == DialogResult.Cancel) goto toss;
            string outFile = oFile.FileName;
            {
                loadingBar.Visible = true;
                if (!Yaz0.Decode(oFile.FileName, Yaz0.DecodeOutputFileRename(oFile.FileName)))
                {
                    MessageBox.Show("Decode error:" + "\n\n" + Yaz0.lerror);
                    goto toss;
                }
            }

            //XML
            if (cbxWriteYaz0Xml.Checked)
            {
                String oFolderName = Path.GetFileNameWithoutExtension(oFile.FileName);
                String oFolderPath = Path.GetDirectoryName(oFile.FileName) + "\\" + oFolderName;
                if (File.Exists(oFolderPath + "_Yaz0Debug.xml"))
                    if (MessageBox.Show(oFolderName + ".xml already exists!" + "\n\n" + "Proceed anyway?", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.No) goto toss;
                if (!DebugWriter.WriteYaz0Xml(oFile.FileName, (oFolderPath + "_Yaz0Debug.xml")))
                    MessageBox.Show("XML file failed to write by unknown reasons");
            }

            MessageBox.Show("Decode complete!" + "\n\n" + outFile);

            toss:
            oFile.Dispose();
            GC.Collect();
            loadingBar.Visible = false;
        }
        #endregion

        #region Button Yaz0 Encode
        private void btnYaz0Encode_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFile = new OpenFileDialog();
            if (tbxFolderRoot.Text != "") oFile.InitialDirectory = tbxFolderRoot.Text;
            loadingBar.Visible = true;
            if (!(oFile.ShowDialog() == DialogResult.Cancel))
            {
                if (!SARC.IsYaz0File(oFile.FileName))
                {
                    string outFile = Yaz0.EncodeOutputFileRename(oFile.FileName);
                    if (File.Exists(outFile))
                    {
                        if (MessageBox.Show(Path.GetFileName(outFile) + " already exists!" + "\n\n" + "Proceed anyway?", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            Encode(oFile.FileName, outFile);
                    }
                    else
                        Encode(oFile.FileName, outFile);
                }
                else
                    MessageBox.Show("Encode error:" + "\n\n" + "File is already Yaz0 encoded!");
            }
            oFile.Dispose();
            loadingBar.Visible = false;
        }

        private void Encode(string inFile, string outFile) //Yaz0 Encode
        {
            if (Yaz0.Encode(inFile, outFile))
                MessageBox.Show("Encode complete!" + "\n\n" + outFile);
            else
                MessageBox.Show("Encode error:" + "\n\n" + Yaz0.lerror);
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

            sFile.Filter = "All Files|*.*";
            sFile.InitialDirectory = oFolder.FileName.Remove(oFolder.FileName.LastIndexOf("\\")); //Previous folder, as selected is to build outside of it.
            sFile.FileName = System.IO.Path.GetFileName(oFolder.FileName);
            loadingBar.Visible = true;
            if (sFile.ShowDialog() == DialogResult.Cancel) goto toss;

            if (cbxSetDataOffset.Checked)
            {
                uint dataOffset = (uint)int.Parse(tbxDataOffset.Text, System.Globalization.NumberStyles.HexNumber);
                if (!SARC.Build(oFolder.FileName, sFile.FileName, dataOffset))
                {
                    MessageBox.Show("Failed to build!" + "\n\n" + SARC.lerror);
                    goto toss;
                }
            }
            else
            {
                if (!SARC.Build(oFolder.FileName, sFile.FileName))
                { 
                    MessageBox.Show("Failed to build!" + "\n\n" + SARC.lerror);
                    goto toss;
                }
            }

            MessageBox.Show("Building Complete!" + "\n\n" + sFile.FileName);

            toss:
            oFolder.Dispose();
            sFile.Dispose();
            GC.Collect();
            loadingBar.Visible = false;
        }
        #endregion

        private void cbxSetDataOffset_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxSetDataOffset.Checked)
            {
                tbxDataOffset.Enabled = true;
            }
            else
            {
                tbxDataOffset.Enabled = false;
            }
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            if (tbxFolderRoot.Text == "")
            {
                MessageBox.Show("Browse for a default folder first!");
            }
            else
            {
                try
                {
                    System.Diagnostics.Process.Start(tbxFolderRoot.Text);
                }
                catch
                {
                    MessageBox.Show("Error: Invalid path" + "\n" + tbxFolderRoot.Text);
                }
            }
        }

        private void tbxFolderRoot_TextChanged(object sender, EventArgs e)
        {
            if (tbxFolderRoot.Text == "")
            {
                BotwUnpacker.Properties.Settings.Default.RootFolder = "";
                BotwUnpacker.Properties.Settings.Default.Save();
            }
            else
            {
                BotwUnpacker.Properties.Settings.Default.RootFolder = tbxFolderRoot.Text;
                BotwUnpacker.Properties.Settings.Default.Save();
            }
        }        
        
        FrmCompareTool frmCompareBuild = new FrmCompareTool();
        private void btnCompareAndBuild_Click(object sender, EventArgs e)
        {
            if (!(frmCompareBuild.Visible))
            {
                if (frmCompareBuild.IsDisposed)
                    frmCompareBuild = new FrmCompareTool();
                frmCompareBuild.StartPosition = FormStartPosition.Manual;
                frmCompareBuild.Location = new Point (FrmMain.ActiveForm.DesktopLocation.X + 380, FrmMain.ActiveForm.DesktopLocation.Y);
                frmCompareBuild.Show();
            }
            else
            {
                frmCompareBuild.Refresh();
                if (frmCompareBuild.WindowState == FormWindowState.Minimized)
                    frmCompareBuild.WindowState = FormWindowState.Normal;
                frmCompareBuild.BringToFront();
            }

        }

        FrmPaddingTool frmPaddingEditor = new FrmPaddingTool();
        private void btnSarcEditor_Click(object sender, EventArgs e)
        {
            if (!(frmPaddingEditor.Visible))
            {
                if (frmPaddingEditor.IsDisposed)
                    frmPaddingEditor = new FrmPaddingTool();
                frmPaddingEditor.StartPosition = FormStartPosition.Manual;
                frmPaddingEditor.Location = new Point(FrmMain.ActiveForm.DesktopLocation.X + 380, FrmMain.ActiveForm.DesktopLocation.Y);
                frmPaddingEditor.Show();
            }
            else
            {
                frmPaddingEditor.Refresh();
                if (frmPaddingEditor.WindowState == FormWindowState.Minimized)
                    frmPaddingEditor.WindowState = FormWindowState.Normal;
                frmPaddingEditor.BringToFront();
            }
        }

        ToolTip t1 = new ToolTip();
        private void cbxSetDataOffset_MouseHover(object sender, EventArgs e)
        {
            t1.Show("If your build needs to set where node data begins (for special cases only)", cbxSetDataOffset);
        }

        private void cbxNodeDecode_MouseHover(object sender, EventArgs e)
        {
            t1.Show("This will decode all the unpacked node files (will also keep the encoded file)", cbxNodeDecode);
        }
    }
}
