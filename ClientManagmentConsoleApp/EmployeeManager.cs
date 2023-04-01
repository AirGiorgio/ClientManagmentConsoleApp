using Manager.App.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagmentConsoleApp
{
    public class EmployeeManager
    {
        EmployeeService employeeService = new EmployeeService();

        public ConsoleKeyInfo EmployeesView(MenuActionService menu)
        {
            var employees = menu.GetMenuByType("EmployeeSubmenu");
            Console.Clear(); Console.WriteLine("Wybierz operację:");
            foreach (var item in employees)
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
                    GetEmployeesFromMemory();
                       break;
                    case '2':
                        Employee e1 = GetEmployeeData(0);
                        DisplayStatus(employeeService.Add(e1));
                       break;
                    case '3':
                        Employee e2 = GetEmployeeData(1);
                        DisplayStatus(employeeService.Edit(e2));
                       break;
                    case '4':
                        Employee e3 = GetEmployeeId();
                        DisplayStatus(employeeService.Remove(e3));
                       break;
                    case '5':
                        SaveListToFile();
                        break;
                    case '6':
                        ReadEmployeesFromFile();
                        break;
                    case '7': 
                        Console.Clear(); return;
                default:
                        Console.WriteLine("Taka opcja nie istnieje!");
                        Thread.Sleep(1500);
                        Console.Clear();
                        break; 
              }; 
        }

        private void ReadEmployeesFromFile()
        {
            MenuActionService Menu = new MenuActionService();
            var keyInfo = SaveToFileService<Employee>.SaveToFileView(Menu.Initialize(Menu));
            List<Employee> ListFromFile = new List<Employee>();

            if (keyInfo.KeyChar == '1')
            {
                ListFromFile = employeeService.EmployeesFromTxt();
            }
            else if (keyInfo.KeyChar == '2')
            {
                ListFromFile = employeeService.EmployeesFromXml();
            }
            else if (keyInfo.KeyChar == '3')
            {
                ListFromFile = employeeService.EmployeesFromJson();
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
            var keyInfo = SaveToFileService<Employee>.SaveToFileView(Menu.Initialize(Menu));
            List<Employee> employees = employeeService.GetEmployees();

            bool succeed;

            if (keyInfo.KeyChar == '1')
            {
                succeed = SaveToFileService<Employee>.ToTextFile(employees, 1);
            }
            else if (keyInfo.KeyChar == '2')
            {
                succeed = SaveToFileService<Employee>.ToXmlFile(employees, 1);
            }
            else if (keyInfo.KeyChar == '3')
            {
                succeed = SaveToFileService<Employee>.ToJsonFile(employees, 1);
            }
            else
            {
                Console.Clear(); return;
            }
            Console.WriteLine(succeed == true ? "Zapisano na pulpicie pod nazwą Employees w wybranym formacie" : "Wystąpił błąd przy zapisie do pliku");
            Thread.Sleep(2000); Console.Clear();
        }

        private void GetEmployeesFromMemory()
        {
            Console.Clear();
            List<Employee> employees = employeeService.GetEmployees();
            if (employees != null && employees.Any())
            {
                foreach (var item in employees)
                {
                    Console.WriteLine($"{item.Id}. {item.Name} {item.Surname}, {item.CompanyPosition}, {item.Salary} zł");
                }
                Console.WriteLine("Naciśnij dowolny przycisk by kontynuować..."); Console.ReadKey(); Console.Clear();
            }
            else
            {
                Console.WriteLine("Lista jest pusta. Wczytaj ją z pliku lub stwórz od nowa.\r\nNaciśnij dowolny przycisk aby kontynuować...");
                Console.ReadKey(); Console.Clear();
            }
        }
        private Employee GetEmployeeId()
        {
            Console.Clear();
            Console.WriteLine("Podaj Id pracownika do usunięcia:");
            string id = Console.ReadLine();

            if (string.IsNullOrEmpty(id) || !int.TryParse(id, out int iid))
            {
                return null;
            }
            else return employeeService.GetEmployeeById(Convert.ToInt32(id));
        }
        private Employee GetEmployeeData(int x)
        {
            Console.Clear();
            Console.WriteLine(x == 1 ? "Podaj Id pracownika do edycji: " : "Podaj Id nowego pracownika: ");
            string id = Console.ReadLine();

            Console.WriteLine("Podaj imię pracownika: ");
            string name = Console.ReadLine();

            Console.WriteLine("Podaj nazwisko pracownika: ");
            string surname = Console.ReadLine();

            Console.WriteLine("Podaj stanowisko pracownika : ");
            string position = Console.ReadLine();

            Console.WriteLine("Podaj pensję pracownika: ");
            string salary = Console.ReadLine();

            bool isValidated = validate(id, name, surname, position, salary);
            if (isValidated)
            {
                return employeeService.Add(id, name, surname, position, salary);
            }
            else return null;
        }
        private bool validate(string? id, string? name, string? surname, string? position, string? salary)
        {
            if (!int.TryParse(id, out int Id))
            {
                Console.WriteLine("Id pracownika musi być liczbą całkowitą!");
                return false;
            }
            else if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(position) || string.IsNullOrEmpty(salary))
            {
                Console.WriteLine("Dane klienta nie mogą być puste!");
                return false;
            }
            else if (!decimal.TryParse(salary, out decimal sal))
            {
                Console.WriteLine("Pensja jest w nieprawidłowym formacie!");
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
