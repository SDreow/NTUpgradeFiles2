using NTUpgradeFiles2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using XML;
using System.Linq;

namespace PATHS
{
    public class PathUpade
    {
        public string srcPath { get; set; }
        public string dstPath { get; set; }
    }
    static class Paths
    {
        public static string XmlSection = "UpgPath";
        public static String mainExePath;
        public static String mainFolderPath;
        public static String mainNameExe;
        private static PathUpade NtUpdates = null;
        public static List<string> pathsRegExe;
        private static List<PathUpade> pathsUpgrFiles;

        public static void Set_mainPaths(string Path)
        {
            if (File.Exists(Path))
            {
                mainExePath = Path;
                mainFolderPath = System.IO.Path.GetDirectoryName(mainExePath);
                mainNameExe = System.IO.Path.GetFileNameWithoutExtension(mainExePath);
            }
            else
            {
                MessageBox.Show("Cesta (" + mainExePath + ") neexistuje", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mainFolderPath = mainNameExe = "";
            }
        }

        public static List<string> FindPathsMainFiles()
        {
            List<string> cesty = new List<string>();
            DirectoryInfo dirf = new DirectoryInfo(mainFolderPath);

            foreach (FileInfo nfo in dirf.GetFiles())
            {
                cesty.Add(nfo.FullName);
            }
            return cesty;
        }

        public static List<string> FindPathsXmlFilesExe()
        {
            List<string> cesty = new List<string>();
            string[] xml_section = Xml.GetEntryNames("UpgPath");

            if (xml_section != null)
            {
                foreach (string section in xml_section)
                {
                    string cesta = Xml.GetValue("UpgPath", section, "");
                    if (Directory.Exists(Path.GetDirectoryName(cesta)))
                        cesty.Add(cesta);
                }
            }
            return cesty;
        }

        public static List<string> GetAllPathsFolders(string pathFolder)
        {
            List<string> cesty = new List<string>();
            DirectoryInfo[] foldersAll = new DirectoryInfo(pathFolder).GetDirectories();
            foreach (DirectoryInfo folder in foldersAll)
            {
                cesty.Add(folder.FullName);
                cesty.AddRange(GetAllPathsFolders(folder.FullName));
            }
            return cesty;
        }
        public static List<string> GetAllPathsFolders_FirstLevel(string pathFolder)
        {
            List<string> cesty = new List<string>();
            List<Task> tasks = new List<Task>();
            DirectoryInfo[] foldersAll = new DirectoryInfo(pathFolder).GetDirectories();
            foreach (DirectoryInfo folder in foldersAll)
            {
                Task t = Task.Run(() =>
                {
                    cesty.Add(folder.FullName);
                    cesty.AddRange(GetAllPathsFolders(folder.FullName));
                });
                tasks.Add(t);
            }
            Task.WaitAll(tasks.ToArray());
            return cesty;
        }

        //public static List<FileSystemInfo> GetPathsFiles(string pathFolder)
        //{
        //    DirectoryInfo directoryInfo = new DirectoryInfo(pathFolder);
        //    return directoryInfo.EnumerateFileSystemInfos().ToList();
        //}

        public static List<FileSystemInfo> GetAllPathsFiles(string pathFolder)
        {
            List<FileSystemInfo> subory = new List<FileSystemInfo>();
            subory.AddRange(GetPathsFiles(pathFolder));
            foreach (string path in GetAllPathsFolders_FirstLevel(pathFolder))
            {
                if (path != null)
                    subory.AddRange(GetPathsFiles(path));
            }
            return subory;

        }

        public static List<FileSystemInfo> GetPathsFiles(string pathFolder)
        {
            List<FileSystemInfo> subory = new List<FileSystemInfo>();
            foreach (FileSystemInfo file in new DirectoryInfo(pathFolder).GetFiles())
            {
                subory.Add(file);
            }
            return subory;
        }

        public static string ExistPathXmlFilesExe(string Path)
        {
            string name = System.IO.Path.GetFileNameWithoutExtension(Path);
            string pathReg = Xml.GetValue(XmlSection, name, "");
            if (File.Exists(pathReg))
            {
                return pathReg;
            }
            return "";
        }

        public static bool ExistPathmainFilesExe(string cesta)
        {
            foreach (string cesty in FindPathsMainFiles())
            {
                if (System.IO.Path.GetFileName(cesty) == System.IO.Path.GetFileName(cesta))
                {
                    return true;
                }
            }
            return false;
        }

        public static void CreateFolders(List<string> srcPaths, string dstRelPath)
        {
            foreach (string srcFile in srcPaths)
            {
                string srcPath = Path.GetDirectoryName(srcFile);
                foreach (string cesta in GetAllPathsFolders(srcPath))
                {
                    string srcRozdil = cesta.Substring(srcPath.Length, cesta.Length - srcPath.Length);
                    string dst = dstRelPath + srcRozdil;

                    if (!Directory.Exists(dst))
                        Directory.CreateDirectory(dst);
                }
            }
        }

        public static List<PathUpade> InfoUpgrAllFiles(List<string> pathsUpgrFilesExe)
        {
            List<PathUpade> cesty = new List<PathUpade>();
            List<FileSystemInfo> dstPathFiles = new List<FileSystemInfo>();
            List<FileSystemInfo> srcPathFiles = new List<FileSystemInfo>();
            foreach (string pathFilesExe in pathsUpgrFilesExe)
            {
                string srcRelPath = Path.GetDirectoryName(pathFilesExe);
                string dstRelPath = mainFolderPath;
                Task t1 = Task.Run(() =>
                {
                    dstPathFiles = GetAllPathsFiles(dstRelPath);
                });
                Task t2 = Task.Run(() =>
                {
                    srcPathFiles = GetAllPathsFiles(srcRelPath);
                });
                Task.WaitAll(t1, t2);
                cesty.AddRange(UpgrFiles(dstPathFiles, dstRelPath, srcPathFiles, srcRelPath));
            }
            return cesty;
        }

        //public static async Task CopyAllFilesAsync(List<PathUpade> pathsUpgrFiles, FormSoub okno)
        //{
        //    okno.progressBar1.Maximum = pathsUpgrFiles.Count;
        //    List<Task> copyTasks = new List<Task>();

        //    // Zde přidáme proměnnou pro kontrolu zavření okna
        //    bool closeWindow = false;

        //    for (int i = 0; i < pathsUpgrFiles.Count; i++)
        //    {
        //        PathUpade cesta = pathsUpgrFiles[i];

        //        if (cesta.dstPath.ToLower() == mainExePath.ToLower())
        //        {
        //            NtUpdates = cesta;
        //        }
        //        else
        //        {
                    // Asynchronně kopírujeme soubory a přidáme úkol do seznamu úkolů
                    //Task copyTask = Task.Run(() => File.Copy(cesta.srcPath, cesta.dstPath, true));
                    //copyTasks.Add(copyTask);

                    //// Aktualizace progress baru na hlavním vlákně
                    //Func<Task> updateProgressBar = async () =>
                    //{
                    //    await okno.InvokeAsync(() =>
                    //    {
                    //        okno.progressBar1.Value = i + 1; // Aktualizujeme progress bar
                    //    });
                    //};

                    //await Task.Run(updateProgressBar);



                    //// Pokud kopírování stále probíhá, uzamkneme okno
                    //if (i < pathsUpgrFiles.Count - 1)
                    //{
                    //    await okno.InvokeAsync(() =>
                    //    {
                    //        okno.Enabled = false;
                    //    });
                    //}
                    //else
                    //{
                    //    // Pokud kopírování skončilo, povolíme okno a nastavíme closeWindow na true
                    //    await okno.InvokeAsync(() =>
                    //    {
                    //        okno.Enabled = true;
                    //        closeWindow = true;
                    //    });
                    //}
            //    }
            //}

            // Počkejme na dokončení všech kopírovacích úkolů
            //await Task.WhenAll(copyTasks);

            // Zde zavřeme okno, pokud closeWindow je true
            //if (closeWindow)
            //{
            //    await okno.InvokeAsync(() =>
            //    {
            //        okno.Close();
            //    });
            //}

            // Po dokončení všech úkolů můžete provést další operace
            // ...
        //}






        public static async Task CopyAllFilesAsync(List<PathUpade> pathsUpgrFiles, FormSoub okno)
        {
            okno.progressBar1.Maximum = pathsUpgrFiles.Count;
            List<Task> copyTasks = new List<Task>();
            try
            {
                foreach (PathUpade cesta in pathsUpgrFiles)
                {
                    if (cesta.dstPath.ToLower() == mainExePath.ToLower())
                    {
                        NtUpdates = cesta;
                    }
                    else
                    {
                        // Asynchronně kopírujeme soubory
                        Task copyTask = Task.Run(() => File.Copy(cesta.srcPath, cesta.dstPath, true));
                        copyTasks.Add(copyTask);
                    }

                    //Aktualizace progress baru na hlavním vlákně
                    okno.Invoke((MethodInvoker)delegate
                    {
                        okno.progressBar1.Increment(1);
                    });

                    //Asynchronně odstraňujeme položku z listViewSoub
                    //await Task.Run(() =>
                    //{
                    //    okno.Invoke((MethodInvoker)delegate
                    //    {
                    //        okno.listViewSoub.Items.RemoveAt(0);
                    //    });
                    //});
                }

                // Počkejme na dokončení všech kopírovacích úkolů
                await Task.WhenAll(copyTasks);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        //public static async Task CopyAllFilesAsync(List<PathUpade> pathsUpgrFiles, FormSoub okno)
        //{
        //    okno.progressBar1.Maximum = pathsUpgrFiles.Count;

        //    foreach (PathUpade cesta in pathsUpgrFiles)
        //    {
        //        if (cesta.dstPath.ToLower() == mainExePath.ToLower())
        //        {
        //            NtUpdates = cesta;
        //        }
        //        else
        //        {
        //            await Task.Run(() => File.Copy(cesta.srcPath, cesta.dstPath, true));
        //        }

        //        okno.progressBar1.Increment(1);
        //        okno.listViewSoub.Items.RemoveAt(0);
        //        okno.Refresh();
        //    }
        //}

        //public static void CopyAllFiles(List<PathUpade> pathsUpgrFiles, FormSoub okno)
        //{
        //    okno.progressBar1.Maximum = pathsUpgrFiles.Count;
        //    foreach (PathUpade cesta in pathsUpgrFiles)
        //    {
        //        if (cesta.dstPath.ToLower() == mainExePath.ToLower())
        //            NtUpdates = cesta;
        //        else
        //            File.Copy(cesta.srcPath, cesta.dstPath, true);

        //        okno.progressBar1.Increment(1);
        //        okno.listViewSoub.Items.RemoveAt(0);
        //        okno.Refresh();
        //    }
        //}

        public static List<PathUpade> UpgrFiles(List<FileSystemInfo> dstPathsFiles, string dstRelPathFiles, List<FileSystemInfo> srcPathsFiles, string srcRelPathFiles)
        {
            List<PathUpade> cesty = new List<PathUpade>();

            foreach (FileSystemInfo srcPathFiles in srcPathsFiles)
            {
                DateTime dt = DateTime.Now;
                string srcRozdil = srcPathFiles.FullName.Substring(srcRelPathFiles.Length, srcPathFiles.FullName.Length - srcRelPathFiles.Length);
                string dst = dstRelPathFiles + srcRozdil;

                FileSystemInfo dstFile = dstPathsFiles.Find(item => item.FullName == dst);
                if (dstFile == null)
                {
                    string dst_find = dst.ToLower();
                    dstFile = dstPathsFiles.Find(item => item.FullName.ToLower() == dst_find);
                }

                dstPathsFiles.Remove(dstFile);

                if (dstFile != null)
                    if (srcPathFiles.LastWriteTime <= dstFile.LastWriteTime.AddSeconds(2))
                        continue;

                cesty.Add(new PathUpade { srcPath = srcPathFiles.FullName, dstPath = dst });
            }
            return cesty;
        }

        public static List<string> GetSrcFiles(List<PathUpade> pathsUpgrFiles)
        {
            List<string> subory = new List<string>();
            foreach (PathUpade subor in pathsUpgrFiles)
            {
                subory.Add(subor.srcPath);
            }
            subory.Sort();
            return subory;
        }

        ///<summary>
        ///Provede upgrade souboru z prislusneho uloziste
        ///</summary>
        public static bool PrepareFiles()
        {
            pathsRegExe = FindPathsXmlFilesExe();
            if (pathsRegExe.Count == 0)
                return false;

            FormSoub form = new FormSoub();
            form.Text = "Hledám update programu";
            form.upgrFiles = new string[] { };
            form.Show();
            form.Refresh();

            pathsUpgrFiles = InfoUpgrAllFiles(pathsRegExe);
            form.Close();
            if (pathsUpgrFiles.Count == 0)
                return true;

            form = new FormSoub();
            form.upgrFiles = GetSrcFiles(pathsUpgrFiles).ToArray();
            form.Refresh();
            form.ShowDialog();
            return true;
        }

        public static void CopyFiles(FormSoub okno)
        {
            Paths.CreateFolders(pathsRegExe, Paths.mainFolderPath);
            _ = Paths.CopyAllFilesAsync(pathsUpgrFiles, okno);
        }

        public static void Shell(string path, string Arguments)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("Soubor exe (" + path + ") nenalezen", "Upozornění", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Process compiler = new Process();
            compiler.StartInfo.FileName = path;
            compiler.StartInfo.Arguments = Arguments;
            compiler.StartInfo.UseShellExecute = true;
            compiler.Start();
        }

        ///<summary>
        ///Ak je update seba sameho potom spustim shell
        ///</summary>
        public static void FinalyUpdate()
        {
            if (NtUpdates != null)
            {
                string[] lines = { ":LoopDel"
                                 , "del %2"
                                 , "if exist %2 goto LoopDel"
                                 , ":LoopCopy"
                                 , "copy %1 %2"
                                 , "if not exist %2 goto LoopCopy"
                                 , "del %0" };

                File.WriteAllLines(
                    mainFolderPath + "\\NTUpgradeFiles.bat",
                    lines);

                Paths.Shell(mainFolderPath + "\\NTUpgradeFiles.bat", "\"" + NtUpdates.srcPath + "\" \"" + NtUpdates.dstPath + "\"");
            }
        }
    }
}
