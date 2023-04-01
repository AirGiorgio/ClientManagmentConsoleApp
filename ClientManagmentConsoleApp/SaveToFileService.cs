using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace ClientManagmentConsoleApp
{
    public static class SaveToFileService<T>
    {
        public static ConsoleKeyInfo SaveToFileView(MenuActionService menu)
        {
            var files = menu.GetMenuByType("FileMenu");
            Console.Clear(); Console.WriteLine("Wybierz operację:");
            foreach (var item in files)
            {
                Console.WriteLine($"{item.Id} {item.Name}");
            }
            var operation = Console.ReadKey();
            return operation;
        }


        public static bool ToTextFile(IEnumerable<T> Someone, int x)
        {
            string ClientsOrEmployees = (x == 1) ? "Employees" : "Clients";

            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + ClientsOrEmployees + ".txt";

            StreamWriter SW = new StreamWriter(filePath);

            try
            {
                foreach (var item in Someone)
                {
                    SW.WriteLine(item.ToString());
                }
            }
            catch (IOException)
            {
                return false;
            }
            finally
            {
                SW.Close();
            }
            return true;
        }

        public static bool ToXmlFile(IEnumerable<T> Someone, int x)
        {
            string ClientsOrEmployees = (x == 1) ? "Employees" : "Clients";

            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + ClientsOrEmployees + ".xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            StreamWriter SW = new StreamWriter(filePath);
            try
            {
                serializer.Serialize(SW, Someone);
                SW.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            } 
        }
            public static bool ToJsonFile(IEnumerable<T> Someone, int x)
            {
                string ClientsOrEmployees = (x == 1) ? "Employees" : "Clients";

                string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + ClientsOrEmployees + ".json";

                try
                {
                    string jsonString = JsonConvert.SerializeObject(Someone, Formatting.Indented);
                    File.WriteAllText(filePath, jsonString);
                }
                catch (IOException)
                {
                    return false;
                }
                return true;
            }
        
    }
}
