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
        }

        #region Browse Root Button
        private void btnBrowseRoot_Click(object sender, EventArgs e) //Browse Root button
        {
            FolderBrowserDialog oFolder = new FolderBrowserDialog();
            if (tbxFolderRoot.Text != "")
                oFolder.SelectedPath = tbxFolderRoot.Text;
            oFolder.Description = "Select a game root folder";
            if (oFolder.ShowDialog() == DialogResult.Cancel) goto toss;
            tbxFolderRoot.Text = oFolder.SelectedPath;

            //Save GameRoot property
            Properties.Settings.Default.RootFolder = oFolder.SelectedPath; 
            Properties.Settings.Default.Save();

            toss:
            oFolder.Dispose();
        }
        #endregion

        private void btnClearRoot_Click(object sender, EventArgs e)
        {
            tbxFolderRoot.Text = "";
            Properties.Settings.Default.RootFolder = "";
            Properties.Settings.Default.Save();
        }

        #region Extract Pack Button
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
            if (!PACK.Extract(oFile.FileName, oFolderPath))
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

        #region Build Pack Button
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
                if (!PACK.build(oFolder.SelectedPath, sFile.FileName, dataOffset))
                    MessageBox.Show("Failed to build!" + "\n\n" + PACK.lerror);
            }
            else
            {
                if (!PACK.build(oFolder.SelectedPath, sFile.FileName))
                    MessageBox.Show("Failed to build!" + "\n\n" + PACK.lerror);
            }

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
    }
}
