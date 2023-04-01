using Manager.App.Concrete;
using System;

namespace ClientManagmentConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Witaj w moim programie do zarządzania przedsiębiorstwem!");
            Thread.Sleep(2000);

            MenuActionService Menu = new MenuActionService();
            Menu = Menu.Initialize(Menu);

            ClientManager clientManager = new ClientManager();
            EmployeeManager employeeManager = new EmployeeManager();

            while (true)
            {
                var mainMenu = Menu.GetMenuByType("Main");
                foreach (var item in mainMenu)
                {
                    Console.WriteLine($"{item.Id} {item.Name}");
                }

                 var option = Console.ReadKey();

                 switch (option.KeyChar)
                 {
                     case '1':
                         var keyInfoC = clientManager.ClientsView(Menu);
                         clientManager.Submenu(keyInfoC);
                         break;
                     case '2':
                         var keyInfoE = employeeManager.EmployeesView(Menu);
                         employeeManager.Submenu(keyInfoE);
                         break;
                     case '3':
                         Environment.Exit(0);
                         break;
                     default:
                         Console.WriteLine("Taka opcja nie istnieje!");
                         Thread.Sleep(2000);
                         Console.Clear();
                         break;
                 }
            }

        }
    }
}