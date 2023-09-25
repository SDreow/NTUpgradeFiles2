using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace XML
{
    static class Xml
    {
        private static Encoding _encoding = Encoding.UTF8;
        private static XmlDocument _doc;

        private static string _rootName = "profile";
        public static string rootName
        {
            get { return _rootName; }
            set { _rootName = value; }
        }

        private static string _pathFile = GetDefaultName();
        private static void GetXmlDocument()
        {
            try
            {

                _doc = new XmlDocument();
                _doc.Load(_pathFile);
            }
            catch (Exception)
            {
                _doc = null;
            }
        }
        private static string GetSectionsPath(string section)
        {
            return "section[@name=\"" + section + "\"]";
        }
        private static string GetEntryPath(string entry)
        {
            return "entry[@name=\"" + entry + "\"]";
        }
        private static object GetValue(string section, string entry)
        {
            try
            {
                GetXmlDocument();
                XmlElement root = _doc.DocumentElement;

                XmlNode entryNode = root.SelectSingleNode(GetSectionsPath(section) + "/" + GetEntryPath(entry));
                return entryNode?.InnerText;
            }
            catch
            {
                return null;
            }
        }
        private static bool HasSection(string section)
        {
            string[] sections = GetSectionNames();

            if (sections == null)
                return false;
            return Array.IndexOf(sections, section) >= 0;
        }
        public static string pathFile
        {
            get { return _pathFile; }
            set { _pathFile = value; }
        }
        //public static string GetDefaultName()
        //{
        //    try
        //    {

        //        string file = Application.ExecutablePath;
        //        string path = file.Substring(0, file.LastIndexOf('.')) + ".xml";
        //        if (!File.Exists(path))
        //        {
        //            MessageBox.Show("cesta na soubor (" + path + ") neni platna", "upozorneni", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        }
        //        return path;
        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //}

        public static string GetDefaultName()
        {
            try
            {
                string file = Application.ExecutablePath;
                string path = file.Substring(0, file.LastIndexOf('.')) + ".xml";

                if (!File.Exists(path))
                {
                    DialogResult result = MessageBox.Show("Cesta na soubor (" + path + ") neexistuje. Chcete vytvořit výchozí soubor?", "Upozornění", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Vytvořte výchozí obsah XML souboru zde
                        string defaultXmlContent = "<?xml version =\"1.0\" encoding=\"utf-8\"?>\n<profile>\n  <section name=\"RunExe\">\n    <entry name=\"run\">NetVyroba.exe</entry>\n  </section>\n  <section name=\"UpgPath\">\n    <entry name=\"NetVyroba\">\\\\DataServer\\NetVyroba\\NetVyroba.exe</entry>\n  </section>\n</profile>";

                        // Uložte výchozí obsah do souboru
                        File.WriteAllText(path, defaultXmlContent);
                    }
                    else
                    {                       
                        return "";
                    }
                }

                return path;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static int GetValue(string section, string entry, int defaultValue)
        {
            object value = GetValue(section, entry);
            if (value == null)
                return defaultValue;

            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }
        public static double GetValue(string section, string entry, double defaultValue)
        {
            object value = GetValue(section, entry);
            if (value == null)
                return defaultValue;

            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return 0;
            }
        }
        public static bool GetValue(string section, string entry, bool defaultValue)
        {
            object value = GetValue(section, entry);
            if (value == null)
                return defaultValue;

            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {
                return false;
            }
        }
        public static string GetValue(string section, string entry, string defaultValue)
        {
            object value = GetValue(section, entry);
            return (value == null ? defaultValue : value.ToString());
        }
        public static string[] GetEntryNames(string section)
        {
            // Verify the section exists
            if (!HasSection(section))
                return null;

            GetXmlDocument();
            XmlElement root = _doc.DocumentElement;

            // Get the entry nodes
            XmlNodeList entryNodes = root.SelectNodes(GetSectionsPath(section) + "/entry[@name]");
            if (entryNodes == null)
                return null;

            // Add all entry names to the string array			
            string[] entries = new string[entryNodes.Count];
            int i = 0;

            foreach (XmlNode node in entryNodes)
                entries[i++] = node.Attributes["name"].Value;

            return entries;
        }
        public static string[] GetSectionNames()
        {
            // Verify the document exists
            GetXmlDocument();
            if (_doc == null)
                return null;

            // Get the root node, if it exists
            XmlElement root = _doc.DocumentElement;
            if (root == null)
                return null;

            // Get the section nodes
            XmlNodeList sectionNodes = root.SelectNodes("section[@name]");
            if (sectionNodes == null)
                return null;

            // Add all section names to the string array			
            string[] sections = new string[sectionNodes.Count];
            int i = 0;

            foreach (XmlNode node in sectionNodes)
                sections[i++] = node.Attributes["name"].Value;

            return sections;
        }
        public static void SetValue(string section, string entry, object value)
        {
            // If the value is null, remove the entry
            if (value == null)
            {
                RemoveEntry(section, entry);
                return;
            }

            string valueString = value.ToString();

            // If the file does not exist, use the writer to quickly create it
            if (!File.Exists(_pathFile))
            {
                XmlTextWriter writer = null;

                // If there's a buffer, write to it without creating the file
                //if (m_buffer == null)
                writer = new XmlTextWriter(_pathFile, _encoding);
                //else
                //    writer = new XmlTextWriter(new MemoryStream(), Encoding);

                writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();

                writer.WriteStartElement(_rootName);
                writer.WriteStartElement("section");
                writer.WriteAttributeString("name", null, section);
                writer.WriteStartElement("entry");
                writer.WriteAttributeString("name", null, entry);
                writer.WriteString(valueString);
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.Close();
                return;
            }

            // The file exists, edit it
            GetXmlDocument();
            XmlElement root = _doc.DocumentElement;

            // Get the section element and add it if it's not there
            XmlNode sectionNode = root.SelectSingleNode(GetSectionsPath(section));
            if (sectionNode == null)
            {
                XmlElement element = _doc.CreateElement("section");
                XmlAttribute attribute = _doc.CreateAttribute("name");
                attribute.Value = section;
                element.Attributes.Append(attribute);
                sectionNode = root.AppendChild(element);
            }

            // Get the entry element and add it if it's not there
            XmlNode entryNode = sectionNode.SelectSingleNode(GetEntryPath(entry));
            if (entryNode == null)
            {
                XmlElement element = _doc.CreateElement("entry");
                XmlAttribute attribute = _doc.CreateAttribute("name");
                attribute.Value = entry;
                element.Attributes.Append(attribute);
                entryNode = sectionNode.AppendChild(element);
            }

            // Add the value and save the file
            entryNode.InnerText = valueString;
            Save();
        }
        public static void RemoveEntry(string section, string entry)
        {

            GetXmlDocument();
            if (_doc == null)
                return;

            // Get the entry's node, if it exists
            XmlElement root = _doc.DocumentElement;
            XmlNode entryNode = root.SelectSingleNode(GetSectionsPath(section) + "/" + GetEntryPath(entry));
            if (entryNode == null)
                return;

            entryNode.ParentNode.RemoveChild(entryNode);
            Save();
        }
        public static void RemoveSection(string section)
        {
            // Verify the document exists
            GetXmlDocument();
            if (_doc == null)
                return;

            // Get the root node, if it exists
            XmlElement root = _doc.DocumentElement;
            if (root == null)
                return;

            // Get the section's node, if it exists
            XmlNode sectionNode = root.SelectSingleNode(GetSectionsPath(section));
            if (sectionNode == null)
                return;

            root.RemoveChild(sectionNode);
            Save();
        }
        private static void Save()
        {
            if (_doc != null)
                _doc.Save(_pathFile);
        }
    }
}
