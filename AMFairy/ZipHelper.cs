namespace ZipHelper
{
    using ICSharpCode.SharpZipLib.Zip;
    using System;
    using System.IO;

    public class Zip
    {
        private static void CompressDirectory(string root, string path, ZipOutputStream s, byte[] buffer)
        {
            ZipEntry entry;
            root = root.TrimEnd(new char[] { '\\' }) + @"\";
            string[] files = Directory.GetFiles(path);
            string[] directories = Directory.GetDirectories(path);
            string str = path.Replace(root, "");
            if (str != "")
            {
                str = str.Replace(@"\", "/") + "/";
            }
            foreach (string str2 in files)
            {
                entry = new ZipEntry(str + Path.GetFileName(str2))
                {
                    DateTime = DateTime.Now
                };
                s.PutNextEntry(entry);
                using (FileStream stream = File.OpenRead(str2))
                {
                    int num;
                    do
                    {
                        num = stream.Read(buffer, 0, buffer.Length);
                        s.Write(buffer, 0, num);
                    }
                    while (num > 0);
                }
            }
            foreach (string str3 in directories)
            {
                entry = new ZipEntry(str3.Replace(root, "").Replace(@"\", "/") + "/");
                s.PutNextEntry(entry);
                CompressDirectory(root, str3, s, buffer);
            }
        }

        public static void CompressDirectory(string dirPath, string fileName, int level, int bufferSize)
        {
            try
            {
                byte[] buffer = new byte[bufferSize];
                using (ZipOutputStream stream = new ZipOutputStream(File.Create(fileName)))
                {
                    stream.SetLevel(level);
                    CompressDirectory(dirPath, dirPath, stream, buffer);
                    stream.Finish();
                    stream.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Extract(string zipFilePath, string extractPath, int bufferSize)
        {
            try {

                extractPath = extractPath.TrimEnd(new char[] { '\\' }) + @"\";
                byte[] buffer = new byte[bufferSize];
                using (ZipInputStream stream = new ZipInputStream(File.OpenRead(zipFilePath)))
                {
                    ZipEntry entry;
                    while ((entry = stream.GetNextEntry()) != null)
                    {
                        string directoryName = Path.GetDirectoryName(entry.Name);
                        string fileName = Path.GetFileName(entry.Name);
                        if (directoryName.Length > 0)
                        {
                            Directory.CreateDirectory(extractPath + directoryName);
                        }
                        if (fileName != string.Empty)
                        {
                            using (FileStream stream2 = File.Create(extractPath + entry.Name.Replace("/", @"\")))
                            {
                                int num;
                                bool flag;
                                goto Label_00E2;
                            Label_00B5:
                                num = stream.Read(buffer, 0, buffer.Length);
                                if (num > 0)
                                {
                                    stream2.Write(buffer, 0, num);
                                }
                                else
                                {
                                    continue;
                                }
                            Label_00E2:
                                flag = true;
                                goto Label_00B5;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

