using ClientManagmentConsoleApp;
using Manager.App.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.App.Concrete
{
    public class BaseService<T> : IService<T> where T : BasePerson
    {
       
        public List<T> Someone { get; set; }

        public BaseService()
        {
            Someone = new List<T>();
        }

        public OperationResult Add(T person)
        {
            if (person is null)
            {
                return new OperationResult() { Success = false, Message = "Nie można dodać do listy!"  };
            }
            else if (Someone.Any(x => x.Id == person.Id))
            {
                return new OperationResult() { Success = false, Message = "Osoba o tym Id jest już na liście!" };
            }
            else
            { 
                Someone.Add(person);
                return new OperationResult() { Success = true, Message = "Dodano do listy!" };
            }
        }
        public OperationResult Edit(T person)
        {
            if (person is null)
            {
                return new OperationResult() { Success = false, Message = "Nie można dodać do listy!" };
            }
            var entity = Someone.SingleOrDefault(x => x.Id == person.Id);
            if (entity != null)
            {
                if (person.GetType() == typeof(Client))
                {
                    var client = person as Client;
                    var clientEntity = entity as Client;

                    clientEntity.Id = client.Id;
                    clientEntity.Name = client.Name;
                    clientEntity.Surname = client.Surname;
                    clientEntity.BirthDate = client.BirthDate;
                    clientEntity.Telephone = client.Telephone;
                }
                else if (person.GetType() == typeof(Employee)) 
                {
                    var employee = person as Employee;
                    var employeeEntity = entity as Employee;

                    employeeEntity.Id = employee.Id;
                    employeeEntity.Name = employee.Name;
                    employeeEntity.Surname = employee.Surname;
                    employeeEntity.CompanyPosition = employee.CompanyPosition;
                    employeeEntity.Salary = employee.Salary;

                }
                return new OperationResult() { Success = true, Message = "Zaktualizowano!" }; ;

            }
            else
                return new OperationResult() { Success = false, Message = "Nie znaleziono nikogo o podanym Id!" };
        }

        public OperationResult Remove(T person)
        {
            if (person is null)
            {
                return new OperationResult() { Success = false, Message = "Nie znaleziono nikogo o podanym Id!" };
            }
            var entity = Someone.SingleOrDefault(x => x.Id == person.Id);
            if (entity != null)
            {
                Someone.Remove(person);
                return new OperationResult() { Success = true, Message = "Pomyślnie usunięto z listy!" }; ;
            }
          else
            return new OperationResult() { Success = false, Message = "Nie znaleziono nikogo o podanym Id!" }; ;
        }

    }
}
