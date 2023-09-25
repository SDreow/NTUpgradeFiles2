using System;
using System.IO;
using System.Windows.Forms;
using PATHS;
using Pvax.Shell;
using XML;

namespace NTUpgradeFiles2
{
    static class Program
    {
        public enum MOD
        {
            spust_FormPaths = 1,
        };

        [STAThread]
        static void Main(string[] args)
        {
            int mod = 0;
            string cmd = "";
            Paths.Set_mainPaths(Application.ExecutablePath);

            if (args.Length > 0)
            {
                cmd = args[0];
                if (cmd.Length == 1)
                {
                    if (IsNumeric(cmd))
                    {
                        mod = Convert.ToInt32(cmd);
                    }
                    switch (mod)
                    {
                        case (int)MOD.spust_FormPaths:
                            Application.EnableVisualStyles();
                            Application.SetCompatibleTextRenderingDefault(false);
                            Application.Run(new FormPaths());
                            return;
                        default:
                            break;
                    }
                }
            }

            string soubExe = Xml.GetValue("RunExe", "run", "");
            string soub = Path.GetFileNameWithoutExtension(soubExe);
            string pathSoubExe = Paths.mainFolderPath + "\\" + soubExe;
            string mainPathLink = Paths.mainFolderPath + "\\" + soub + ".lnk";
            string pathDesktopLink = Environment.GetFolderPath(Environment.SpecialFolder.Desktop).Trim() + "\\" + soub + ".lnk";

            if (!File.Exists(pathSoubExe))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormPaths());
            }
            else
            {
                if (!Paths.PrepareFiles())
                    MessageBox.Show("Upgr cesty nenalezeny, zkontrolujte cestu v NTUpgradeFiles2.xml.", "Upozornění", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (!File.Exists(mainPathLink))
                {
                    FileStream fs = File.Create(mainPathLink);
                    fs.Close();
                }
                ShellLink lnk = new ShellLink();
                lnk.Load(mainPathLink);
                lnk.Description = soub;
                lnk.Arguments = "";
                lnk.Path = Paths.mainExePath;
                lnk.IconPath = pathSoubExe;
                lnk.WorkingDirectory = Paths.mainFolderPath;
                lnk.Save(mainPathLink);

                if (Convert.ToByte(Xml.GetValue("RunExe", "shortcut", "0")) == 1)
                {
                    if (!File.Exists(pathDesktopLink))
                        File.Copy(mainPathLink, pathDesktopLink, true);
                    else
                    {
                        ShellLink lnkD = new ShellLink();
                        lnkD.Load(pathDesktopLink);
                        lnkD.Description = soub;
                        lnkD.Arguments = "";
                        lnkD.Path = Paths.mainExePath;
                        lnkD.IconPath = pathSoubExe;
                        lnkD.WorkingDirectory = Paths.mainFolderPath;
                        lnkD.Save(pathDesktopLink);
                    }
                }
                Paths.Shell(pathSoubExe, cmd);//spustim exe s atributem cmd
                Paths.FinalyUpdate();//spusti update seba sameho
            }
        }
        public static bool IsNumeric(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                double cislo;
                return double.TryParse(text, out cislo);
            }
            return false;
        }
    }
}
