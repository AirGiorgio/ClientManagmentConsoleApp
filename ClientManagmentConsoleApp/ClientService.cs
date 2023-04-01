using Manager.App.Concrete;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClientManagmentConsoleApp
{
    public class ClientService : BaseService<Client>
    {
        private List<Client> ClientsList = new List<Client>();

        public List<Client> ClientsFromTxt()
        {
            try
            {
                string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Clients.txt";

                foreach (var line in File.ReadAllLines(filePath))
                {
                    string[] temp = line.Split(',');
                    ClientsList.Add(new Client(int.Parse(temp[0]), temp[1], temp[2], DateOnly.Parse(temp[3]), temp[4]));
                }
                return ClientsList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Client> ClientsFromXml()
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Clients.xml";

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Client>));
            try
            {
                string xml = File.ReadAllText(filePath);
                StringReader stringReader = new StringReader(xml);
                ClientsList = (List<Client>)xmlSerializer.Deserialize(stringReader);
                return ClientsList;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Client> ClientsFromJson()
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Clients.json";
           
            try
            {
                string jsonData = File.ReadAllText(filePath);

                ClientsList = JsonConvert.DeserializeObject<List<Client>>(jsonData);

                return ClientsList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ClientService()
        {
            this.Someone = ClientsList;
        }

        public List<Client> GetClients()
        {
           return ClientsList;
        }

        public Client GetClientById(int id)
        {
            return ClientsList.SingleOrDefault(z => z.Id == id);
        }

        public Client Add(string id, string name, string surname, string birthdate, string telephone)
        {
            Client client = new Client(Convert.ToInt32(id), name, surname, DateOnly.Parse(birthdate), telephone);
            return client;
        }

    }
}

