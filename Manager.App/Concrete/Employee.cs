using Manager.App.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagmentConsoleApp
{
    public class Employee:BasePerson
    {
        public Employee(int id, string name, string surname, string position, decimal salary)
        {
            Id = id;
            Name = name;
            Surname = surname;
            CompanyPosition = position;
            Salary = salary;
        }
        public Employee()
        {

        }
        public string CompanyPosition { get; set; }

        public decimal Salary { get; set; }

        public override string ToString()
        {
            return Id + "," + Name + "," + Surname + "," + CompanyPosition + "," + Salary;
        }


    }
}
