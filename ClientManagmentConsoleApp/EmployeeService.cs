using Manager.App.Concrete;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ClientManagmentConsoleApp
{
    public class EmployeeService: BaseService<Employee>
    {

        private List<Employee> EmployeeList = new List<Employee>();

         public List<Employee> EmployeesFromTxt()
        {
            try
            {
                string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Employees.txt";

                foreach (var line in File.ReadAllLines(filePath))
                {
                    string[] temp = line.Split(',');
                    EmployeeList.Add(new Employee(int.Parse(temp[0]), temp[1], temp[2], temp[3], Decimal.Parse(temp[4])));
                }
                return EmployeeList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Employee> EmployeesFromXml()
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Employees.xml";
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Employee>));

                string xml = File.ReadAllText(filePath);
                StringReader stringReader = new StringReader(xml);
                EmployeeList = (List<Employee>)xmlSerializer.Deserialize(stringReader);

                return EmployeeList;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Employee> EmployeesFromJson()
        {
            try
            {
                string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Employees.json";

                string jsonData = File.ReadAllText(filePath);

                EmployeeList = JsonConvert.DeserializeObject<List<Employee>>(jsonData);

                return EmployeeList;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public EmployeeService()
        {
            this.Someone = EmployeeList;
        }

        public List<Employee> GetEmployees() 
        {
            return EmployeeList;
        }

         public Employee GetEmployeeById(int id)
         {
             return EmployeeList.SingleOrDefault(z => z.Id == Convert.ToInt32(id));
        }  

        public Employee Add(string id, string name, string surname, string position, string salary)
        {
            Employee employee = new Employee(Convert.ToInt32(id), name, surname, position, Math.Round(Decimal.Parse(salary), 2)); 
            return employee;
        }
        
    }
}
