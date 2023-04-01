using Manager.App.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagmentConsoleApp
{
    public class Client: BasePerson
    {
        public Client(int id, string name, string surname, DateOnly birthdate, string telephone)
        {
            Id = id;
            Name = name;
            Surname = surname;
            BirthDate = birthdate;
            Telephone = telephone;
        }
        public Client()
        {

        }
        public string Telephone { get; set; }

        public DateOnly BirthDate { get; set; }

        public override string ToString()
        {
            return Id + ","  + Name + "," + Surname + "," + BirthDate + "," + Telephone;
        }

    }
}
