using System.Collections.Generic;       
using System.IO;

namespace DirectoryExplorer.Model
{
    class Explorer
    {
        List<FileInfo> fileInfos = new List<FileInfo>(); // Информация о файлах
        Dictionary<string, int> expansions = new Dictionary<string, int>(); // Список расширений и их количество

        private string _directoryPath;
        public string DirectoryPath
        {
            get
            {
                return _directoryPath;
            }
            set
            {
                _directoryPath = value;
            }
        }

        public Explorer(string path)
        {
            DirectoryPath = path;
        }

        public void Find() 
        {
            Find(DirectoryPath);
        }
        public void Find(string path) 
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            DirectoryInfo[] dInfo = directoryInfo.GetDirectories();
            FileInfo[] fInfo = directoryInfo.GetFiles();

            foreach (DirectoryInfo directory in dInfo)
            {
                Find(directory.FullName);
            }
            foreach (FileInfo file in fInfo)
            {
                fileInfos.Add(file);

                if (!expansions.ContainsKey(file.Extension))
                {
                    expansions.Add(file.Extension, 1);
                }
                else
                {
                    expansions[file.Extension] += 1;
                }
            }
        }
        public void Open() 
        {
            FileStream stream = File.Create("view.html");
            stream.Close();
            using (StreamWriter streamwriter = new StreamWriter(@"view.html"))
            {
                streamwriter.WriteLine("<!DOCTYPE html>");
                streamwriter.WriteLine("<html>");
                streamwriter.WriteLine("<header>");
                streamwriter.WriteLine("<meta charset='utf-8'>");
                streamwriter.WriteLine("</header>");
                streamwriter.WriteLine("<body>");
                streamwriter.WriteLine("<p align='center'>Файлы</p>");
                streamwriter.WriteLine("<table border='1' align='center'>");
                streamwriter.WriteLine("<tr><td>Имя</td><td>Расширение</td><td>Объём</td><td>Путь</td></tr>");
                foreach (FileInfo file in fileInfos)
                {
                    streamwriter.WriteLine("<tr><td>" + file.Name + "<td>" + file.Extension + "<td>" + file.Length + "<td>" + file.FullName + "</tr>");
                }
                streamwriter.WriteLine("</table>");
                streamwriter.WriteLine("<p align='center'>Статистика по расширениям</p>");
                streamwriter.WriteLine("<table border='1' align='center'>");
                streamwriter.WriteLine("<tr><td>Расширение<td>Количество</td></tr>");
                foreach (KeyValuePair<string, int> expansion in expansions)
                {
                    streamwriter.WriteLine("<tr><td>" + expansion.Key + "<td>" + expansion.Value + "</tr>");
                }
                streamwriter.WriteLine("</table>");
                streamwriter.WriteLine("</body>");
                streamwriter.WriteLine("</html>");
            }
        }
    }
}
