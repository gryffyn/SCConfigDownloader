using System;
using System.IO;
using System.Text;

namespace sccdownloader
{
    public class Log
    {

        public static void w(string msg)
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log");
            var path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log\\" +
                       string.Format("{0}@{1}.txt", "SCCDownloader", DateTime.Now.ToString("yyyyMd"));
            if (!File.Exists(path))
                File.Create(path).Close();
            var streamWriter =
                new StreamWriter(new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite),
                    Encoding.Default);
            streamWriter.WriteLine("[{0}]:{1}", DateTime.Now.ToLongTimeString(), msg);
            streamWriter.Close();
        }
    }
}