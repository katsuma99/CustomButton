using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class XmlFileManager
    {
        /// <summary>パラメータ（Config名前,Offset名前,comポート番号）を読み込む </summary>
        public static bool ReadXml(string fileName, ref string[] data)
        {
            string filePath = GetSettingDirectoryPath() + "\\" + fileName;
            //ファイルが存在していなかったら飛ばす
            if (!System.IO.File.Exists(filePath))
                return false;

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                // XMLをTwitSettingsオブジェクトに読み込む
                XmlSerializer serializer = new XmlSerializer(typeof(string[]));

                // XMLファイルを読み込み、逆シリアル化（復元）する
                data = (string[])serializer.Deserialize(fs);
            }
            return true;
        }

        /// <summary>パラメータ（Config名前,Offset名前,comポート番号）を書き込む </summary>
        public static void WriteXml(string fileName, string[] data)
        {
            // XmlSerializerを使ってファイルに保存（オブジェクトの内容を書き込む）
            XmlSerializer serializer = new XmlSerializer(typeof(string[]));

            // カレントディレクトリに"settings.xml"というファイルで書き出す
            using (FileStream fs = new FileStream(GetSettingDirectoryPath() + "\\" + fileName, FileMode.Create))
            {
                // オブジェクトをシリアル化してXMLファイルに書き込む
                serializer.Serialize(fs, data);
            }
        }

        private static string GetSettingDirectoryPath()
    {
        string path = Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData),
            Application.CompanyName + "\\" + Application.ProductName);
        if (!System.IO.Directory.Exists(path))
            System.IO.Directory.CreateDirectory(path);
        return path;
    }
    }
}
