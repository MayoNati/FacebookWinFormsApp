using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BasicFacebookFeatures
{
    public class AppSettings
    {
        public Point LastWindowLocation { get; set; }
        public Size LastWindowSize { get; set; }
        public bool RemmeberUser { get; set; }
        public string LastAccessToken { get; set; }

        private AppSettings()
        {
            LastWindowLocation = new Point(20, 50);
            LastWindowSize = new Size(500, 800);
            RemmeberUser = false;
            LastAccessToken = null;
        }

        public void SaveToFile()
        {
            //Stream stream = null;

            //try
            //{
            //    stream = new FileStream(@"c:\appSettings.xml", FileMode.Truncate);
            //    XmlSerializer serializer = new XmlSerializer(this.GetType());
            //    serializer.Serialize(stream, this);
            //    //stream.Flush();
            //    //stream.Close();//indlude flush
            //    //stream.Dispose();//include Close + flush
            //}
            //finally
            //{
            //    stream.Dispose();
            //}
            FileMode fileMode;

            fileMode = File.Exists(@"c:\logs\appSettings.xml") ? FileMode.Truncate : FileMode.Create;
            //if (File.Exists(@"c:\appSettings.xml"))
            //{
            //    fileMode = FileMode.Truncate;
            //}
            //else
            //{
            //    fileMode = FileMode.Create;
            //}
            using (Stream stream = new FileStream(@"c:\logs\appSettings.xml", fileMode))
            {
                XmlSerializer serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(stream, this);
            }
        }
        public static AppSettings LoadFromFile()
        {
            AppSettings obj = new AppSettings();
            if (File.Exists(@"c:\logs\appSettings.xml"))
            {
                using (Stream stream = new FileStream(@"c:\logs\appSettings.xml", FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(AppSettings));
                    obj = serializer.Deserialize(stream) as AppSettings;
                }
            }
           
            return obj;
        }

    }
}
