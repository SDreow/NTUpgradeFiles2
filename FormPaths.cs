using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using PATHS;
using XML;

namespace NTUpgradeFiles2
{
    public partial class FormPaths : Form
    {
        public FormPaths()
        {
            InitializeComponent();
        }

        private void FormPaths_Load(object sender, EventArgs e)
        {
            string path = Xml.GetValue("RunExe", "run", "");
            if (File.Exists(Paths.mainFolderPath + "\\" + path))
                labelRunExe.Text = Paths.mainFolderPath + "\\" + path;
            NactiListView();
        }

        private void buttonExeName_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "exe file (*.exe)|*.exe";
            if (!(listViewUpgrPaths.Items.Count.Equals(0) || listViewUpgrPaths.SelectedItems.Count.Equals(0) || !File.Exists(listViewUpgrPaths.SelectedItems[0].Text)))
            {
                openFileDialog1.FileName = Path.GetFileName(listViewUpgrPaths.SelectedItems[0].Text);
                openFileDialog1.InitialDirectory = Path.GetDirectoryName(listViewUpgrPaths.SelectedItems[0].Text) + "\\";
            }
            DialogResult result = openFileDialog1.ShowDialog();
            if (!result.Equals(DialogResult.OK))
                return;
            string fullNameFile = openFileDialog1.FileName;
            string folderNameFile = Path.GetDirectoryName(fullNameFile);
            string nameFile = Path.GetFileName(fullNameFile);
            string shortNameFile = Path.GetFileNameWithoutExtension(nameFile);

            if (Paths.mainFolderPath == folderNameFile)
            {
                MessageBox.Show("Pro upgrade nelze použít adresář (" + folderNameFile + ")", "Upozornění", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (System.IO.Path.GetFileName(Paths.mainExePath).ToLower() == nameFile.ToLower())
            {
                MessageBox.Show("Pro upgrade nelze použít Exe soubor s názvem (" + nameFile + ")", "Upozornění", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (Paths.ExistPathXmlFilesExe(fullNameFile).Length > 0)
            {
                MessageBox.Show("Upgr cesta exe souboru s názvem (" + nameFile + ") je již v registru uložena.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Xml.SetValue("RunExe", "run", nameFile);
            Xml.SetValue("UpgPath", shortNameFile, fullNameFile);
            NactiListView();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            int check = 0;
            foreach (ListViewItem item in listViewUpgrPaths.Items)
                if (item.Checked) check++;

            if (check == 0)
                MessageBox.Show("Není označen žádný záznam", "Upozornění", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                string message = "Chceš opravdu smazat (" + check + ") záznamu ?";
                string caption = "";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                if (result == DialogResult.No)
                    return;
            }

            foreach (ListViewItem item in listViewUpgrPaths.Items)
            {
                if (item.Checked)
                {
                    check++;
                    Xml.RemoveEntry(Paths.XmlSection, Path.GetFileNameWithoutExtension(System.IO.Path.GetFileName(item.Text)));
                }
            }
            NactiListView();
        }

        private void NactiListView()
        {
            listViewUpgrPaths.Items.Clear();
            listViewUpgrPaths.Columns.Clear();

            List<string> pathsRegExe = Paths.FindPathsXmlFilesExe();
            if (pathsRegExe.Count == 0)
                return;

            //vytvorim sloupce
            listViewUpgrPaths.Columns.Add("Upgrade cesty", listViewUpgrPaths.Width, HorizontalAlignment.Left);

            foreach (String path in pathsRegExe)
            {
                //item
                ListViewItem item1 = new ListViewItem(path, 0);
                item1.Checked = false;
                //path
                item1.SubItems.Add(path);
                //pridam radek do kolekce ListViewItem
                listViewUpgrPaths.Items.AddRange(new ListViewItem[] { item1 });
            }
        }

        private void buttonListRunExe_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "exe files (*.exe)|*.exe";
            if (File.Exists(labelRunExe.Text))
            {
                openFileDialog1.FileName = System.IO.Path.GetFileName(labelRunExe.Text);
                openFileDialog1.InitialDirectory = Path.GetDirectoryName(labelRunExe.Text) + "\\";
            }
            DialogResult result = openFileDialog1.ShowDialog();
            if (!result.Equals(DialogResult.OK))
                return;
            string fullNameFile = openFileDialog1.FileName;
            string folderNameFile = Path.GetDirectoryName(fullNameFile);
            string nameFile = System.IO.Path.GetFileName(fullNameFile);
            string shortNameFile = Path.GetFileNameWithoutExtension(nameFile);
            if (Paths.mainFolderPath != folderNameFile)
            {
                MessageBox.Show("Pro spuštení exe nelze použít jiný adresář než (" + Paths.mainFolderPath + ")", "Upozornění", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (fullNameFile.ToLower() == Paths.mainExePath.ToLower())
            {
                MessageBox.Show("Exe soubor s názvem (" + nameFile + ") nelze vybrat pro spouštení", "Upozornění", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            labelRunExe.Text = fullNameFile;
            Xml.SetValue("RunExe", "run", nameFile);
            NactiListView();
        }

        private void buttonUpgr_Click(object sender, EventArgs e)
        {
            buttonUpgr.Visible = false;
            Paths.PrepareFiles();
            Application.ExitThread();
        }
    }
}
