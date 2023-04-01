using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagmentConsoleApp
{
    public class MenuActionService
    {
        private List<MenuAction> menuActions = new List<MenuAction>();

        public void AddNewAction(int id, string name, string type)
        {
            MenuAction menu = new MenuAction() { Id = id, Name = name, Type = type};
            menuActions.Add(menu);
        }

        public List<MenuAction> GetMenuByType(string type) 
        {
            List<MenuAction> result = new List<MenuAction>();
            foreach (var item in menuActions)
            {
                if (item.Type == type)
                {
                    result.Add(item);
                }
            }
            return result;
        }
        public MenuActionService Initialize(MenuActionService actionService)
        {
            actionService.AddNewAction(1, "Klienci", "Main");
            actionService.AddNewAction(2, "Pracownicy", "Main");
            actionService.AddNewAction(3, "Wyjście", "Main");

            actionService.AddNewAction(1, "Wyświetl klienta", "ClientSubmenu");
            actionService.AddNewAction(2, "Dodaj klienta", "ClientSubmenu");
            actionService.AddNewAction(3, "Edytuj klienta", "ClientSubmenu");
            actionService.AddNewAction(4, "Usuń klienta", "ClientSubmenu");
            actionService.AddNewAction(5, "Zapisz listę do pliku", "ClientSubmenu");
            actionService.AddNewAction(6, "Odczytaj listę z pliku", "ClientSubmenu");
            actionService.AddNewAction(7, "Powrót", "ClientSubmenu");

            actionService.AddNewAction(1, "Wyświetl pracownika", "EmployeeSubmenu");
            actionService.AddNewAction(2, "Dodaj pracownika", "EmployeeSubmenu");
            actionService.AddNewAction(3, "Edytuj pracownika", "EmployeeSubmenu");
            actionService.AddNewAction(4, "Usuń pracownika", "EmployeeSubmenu");
            actionService.AddNewAction(5, "Zapisz listę do pliku", "EmployeeSubmenu");
            actionService.AddNewAction(6, "Odczytaj listę z pliku", "EmployeeSubmenu");
            actionService.AddNewAction(7, "Powrót", "EmployeeSubmenu");

            actionService.AddNewAction(1, "Plik tekstowy", "FileMenu");
            actionService.AddNewAction(2, "Plik XML", "FileMenu");
            actionService.AddNewAction(3, "Plik JSON", "FileMenu");
            actionService.AddNewAction(4, "Powrót", "FileMenu");

            return actionService;
        }
    }
}
