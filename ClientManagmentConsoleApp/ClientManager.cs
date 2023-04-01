using Manager.App.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagmentConsoleApp
{
    public class ClientManager
    {
        ClientService clientService = new ClientService();
        public ConsoleKeyInfo ClientsView(MenuActionService menu)
        {
            var clients = menu.GetMenuByType("ClientSubmenu");
            Console.Clear(); Console.WriteLine("Wybierz operację:");
            foreach (var item in clients)
            {
                Console.WriteLine($"{item.Id} {item.Name}");
            }
            var operation = Console.ReadKey();
            return operation;
        }

        public void Submenu(ConsoleKeyInfo c)
        {    
             switch (c.KeyChar)
             {
                 case '1':
                     GetClientsFromMemory();
                     break;
                 case '2':
                     Client c1 = GetClientData(0);
                     DisplayStatus(clientService.Add(c1));
                     break;
                 case '3':
                     Client c2 = GetClientData(1);
                     DisplayStatus(clientService.Edit(c2));
                     break;
                 case '4':
                     Client c3 = GetClientId();
                     DisplayStatus(clientService.Remove(c3));
                     break;
                case '5':
                    SaveListToFile();
                    break;
                case '6':
                    ReadClientsFromFile();
                    break;
                case '7':
                    Console.Clear(); return;
                default:
                     Console.WriteLine("Taka opcja nie istnieje!");
                     Thread.Sleep(2000);
                     Console.Clear();
                     break;
             }        
        }

        private void ReadClientsFromFile()
        {
            MenuActionService Menu = new MenuActionService();
            var keyInfo = SaveToFileService<Client>.SaveToFileView(Menu.Initialize(Menu));
            List<Client> ListFromFile = new List<Client>();

            if (keyInfo.KeyChar == '1')
            {
                ListFromFile = clientService.ClientsFromTxt();
            }
            else if (keyInfo.KeyChar == '2')
            {
                ListFromFile = clientService.ClientsFromXml();
            }
            else if (keyInfo.KeyChar == '3')
            {
                ListFromFile = clientService.ClientsFromJson();
            }
            else
            {
                Console.Clear(); return;
            }
            if (ListFromFile != null && ListFromFile.Any())
            {
                Console.WriteLine("Pomyślnie wczytano listę z pliku"); Thread.Sleep(1500); Console.Clear();
            }
            else
            {
                Console.WriteLine("Nie udalo sie wczytac listy. Upewnij się, że zostala wygenerowana przez ten " +
                    "program oraz czy widnieje na pulpicie.\r\nNaciśnij dowolny przycisk by kontynuować...");
                Console.ReadKey(); Console.Clear();
            }
        }
        private void SaveListToFile()
        {
            MenuActionService Menu = new MenuActionService();
            var keyInfo = SaveToFileService<Client>.SaveToFileView(Menu.Initialize(Menu));
            List<Client> clients = clientService.GetClients();

            bool succeed;

            if (keyInfo.KeyChar == '1')
            {
                succeed = SaveToFileService<Client>.ToTextFile(clients, 0);
            }
            else if (keyInfo.KeyChar == '2')
            {
                succeed = SaveToFileService<Client>.ToXmlFile(clients, 0);
            }
            else if (keyInfo.KeyChar == '3')
            {
                succeed = SaveToFileService<Client>.ToJsonFile(clients, 0);
            }
            else
            {
                Console.Clear(); return;
            }
            Console.WriteLine(succeed == true ? "Zapisano na pulpicie pod nazwą Clients w wybranym formacie" : "Wystąpił błąd przy zapisie do pliku");
            Thread.Sleep(2000); Console.Clear();
        }


        private void GetClientsFromMemory()
        {
            Console.Clear();
            List<Client> clients = clientService.GetClients();
          
            if (clients != null && clients.Any())
            {
                foreach (var item in clients)
                {
                    Console.WriteLine($"{item.Id}. {item.Name} {item.Surname}, {item.BirthDate}, {item.Telephone}");
                }
                Console.WriteLine("Naciśnij dowolny przycisk by kontynuować..."); Console.ReadKey(); Console.Clear();
            }
            else
            {
                Console.WriteLine("Lista jest pusta. Wczytaj ją z pliku lub stwórz od nowa.\r\nNaciśnij dowolny przycisk aby kontynuować..."); 
                Console.ReadKey(); Console.Clear();
            }
        }

        private Client GetClientId()
        {
            Console.Clear();
            Console.WriteLine("Podaj Id klienta do usunięcia: ");
            string id = Console.ReadLine();

            if (string.IsNullOrEmpty(id) || !int.TryParse(id, out int iid))
            {
                return null;
            }
            else
            return clientService.GetClientById(Convert.ToInt32(id));
        }

        private Client GetClientData(int x)
        {
            Console.Clear();
            Console.WriteLine(x == 1 ? "Podaj Id klienta do edycji: " : "Podaj Id nowego klienta: ");
            string id = Console.ReadLine();

            Console.WriteLine("Podaj imię klienta: ");
            string name = Console.ReadLine();

            Console.WriteLine("Podaj nazwisko klienta: ");
            string surname = Console.ReadLine();

            Console.WriteLine("Podaj datę urodzenia klienta : ");
            string birthdate = Console.ReadLine();

            Console.WriteLine("Podaj telefon klienta: ");
            string telephone = Console.ReadLine();

            bool isValidated = validate(id, name, surname, birthdate, telephone);
            if (isValidated)
            {
                return clientService.Add(id, name, surname, birthdate, telephone);  
            }
            else return null;
        }

        private bool validate(string? id, string? name, string? surname, string? birthdate, string? telephone)
        {

            if (!int.TryParse(id, out int Id))
            {
                Console.WriteLine("Id klienta musi być liczbą całkowitą!");
                return false;
            }
            else if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(birthdate) || string.IsNullOrEmpty(telephone))
            {
                Console.WriteLine("Dane klienta nie mogą być puste!");
                return false;
            }
            else if (!DateOnly.TryParse(birthdate, out DateOnly bdate))
            {
                Console.WriteLine("Data urodzenia jest w nieprawidłowym formacie!");
                return false;
            }
            else if (telephone.Length != 9 || !int.TryParse(telephone, out int tphone))
            {
                Console.WriteLine("Nieprawidłowy numer telefonu!");
                return false;
            }
            else
                return true;
        }
        private void DisplayStatus(OperationResult o)
        {
            Console.WriteLine(o.Message + "\r\nNaciśnij dowolny przycisk by kontynuować...");
            Console.ReadKey();
            Console.Clear();
            return;
        }
    }
}
