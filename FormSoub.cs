using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using PATHS;

namespace NTUpgradeFiles2
{
    public partial class FormSoub : Form
    {
        private string[] _upgrFiles;

        public string[] upgrFiles
        {
            get { return _upgrFiles; }
            set { _upgrFiles = value; }
        }
        public FormSoub()
        {
            InitializeComponent();
            this.Text += " (ver. " + Application.ProductVersion + ")";
        }
        private void NactiListView()
        {
            listViewSoub.Items.Clear();
            listViewSoub.Columns.Clear();

            if (_upgrFiles.Length == 0)
                return;

            //vytvorim sloupce
            listViewSoub.Columns.Add("Soubory pro upgrade", listViewSoub.Width, HorizontalAlignment.Left);

            foreach (String path in _upgrFiles)
            {
                //item
                ListViewItem item1 = new ListViewItem(path, 0);
                item1.Checked = false;
                //path
                item1.SubItems.Add(path);
                //pridam radek do kolekce ListViewItem
                listViewSoub.Items.AddRange(new ListViewItem[] { item1 });
            }
        }

        private void buttonUpgr_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            progressBar1.BringToFront();
            Paths.CopyFiles(this);
            this.Hide();
        }
        private void FormSoub_Shown(object sender, EventArgs e)
        {
            NactiListView();
            LoadGitLog();
            buttonUpgr.Select();
        }
        private void LoadGitLog()
        {
            string netVyroba = "NetVyroba.exe";
            string gitLogName = "Changelog.txt";
            string prefix = "#cz";

            try
            {
                if (!File.Exists(gitLogName) || !File.Exists(netVyroba))
                    return;

                //lokalni verze NetVyroby
                var versionInfo = FileVersionInfo.GetVersionInfo(netVyroba);
                string versionNV = versionInfo.FileVersion;
                string gitLogSource = Path.GetDirectoryName(Paths.pathsRegExe[0]) + "\\" + gitLogName;
                string gitLogLocal = Paths.mainFolderPath + "\\" + gitLogName;

                File.Copy(gitLogSource, gitLogLocal, true);
                string[] gitLog = File.ReadAllLines(gitLogLocal);

                listView2Zmeny.Items.Clear();
                listView2Zmeny.Columns.Clear();

                if (gitLog.Length == 0)
                    return;

                listView2Zmeny.Columns.Add("Co je noveho:", listView2Zmeny.Width, HorizontalAlignment.Left);
                //zacne pridavat commity, dokud nenarazi na radek s lokalni verzi
                foreach (var line in gitLog)
                {
                    if (line.StartsWith("Release ") && CompareVersions(versionNV, GetVersion(line)))
                        return;

                    if (line.StartsWith("Release "))
                    {
                        listView2Zmeny.Items.Add(new ListViewItem("", 0));
                        listView2Zmeny.Items.Add(new ListViewItem("" + line.Replace(prefix, "").Trim(), 0));
                        continue;
                    }
                    if (line.Contains(prefix))
                    {
                        listView2Zmeny.Items.Add(new ListViewItem("-" + line.Replace(prefix, "").Trim(), 0));
                        continue;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void CheckBoxVse_CheckedChanged(object sender, EventArgs e)
        {
            LoadGitLog();
        }

        private bool CompareVersions(string ver1, string ver2)
        {
            if (ver1.Equals(string.Empty) || ver2.Equals(string.Empty))
                return false;

            ver1 = ver1.Replace(".", "");
            ver2 = ver2.Replace(".", "");

            return (Int32.Parse(ver1) >= Int32.Parse(ver2)) ? true : false;
        }
        private string GetVersion(string text)
        {
            string sub = text.Substring(text.LastIndexOf("Release "));
            sub = sub.Replace("Release ", "");
            string[] split = sub.Split(' ');

            for (int i = 0; i < split.Length; i++)
            {
                int tecky = 0;

                for (int k = 0; k < split[i].Length; k++)
                {
                    var znak = split[i][k];
                    if (znak.Equals('.'))//slovo "i", znak "k"
                    {
                        tecky++;
                    }
                }

                if (tecky == 3)
                    return split[i];
            }
            return string.Empty;
        }

        internal Task InvokeAsync(Func<Task> p)
        {
            throw new NotImplementedException();
        }
    }
}
