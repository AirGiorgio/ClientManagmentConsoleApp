using Manager.App.Abstract;
using Moq;
using Xunit;
using ClientManagmentConsoleApp;
using Manager.App.Concrete;
using System;
using FluentAssertions;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Manager.Test
{
    public class SaveToFileServiceTest
    {
        [Fact]
        public void ToTextFileShouldSaveToFile()
        {
            // Arrange
            var employees = new List<Employee>()
            {
                new Employee(1, "I", "D", "stan", 5000),
                new Employee(2, "E", "C", "stan", 4000)
            };

            // Act
            var result = SaveToFileService<Employee>.ToTextFile(employees, 1);

            // Assert
            Assert.True(result);
            var filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Employees.txt";
            var fileContent = File.ReadAllLines(filePath);
            Assert.Equal(2, fileContent.Length);
            Assert.Equal("1,I,D,stan,5000", fileContent[0]);
            Assert.Equal("2,E,C,stan,4000", fileContent[1]);
        }

        [Fact]
        public void ToXmlFileShouldSaveToFile()
        {
            // Arrange
            var employees = new List<Employee>()
            {
                new Employee(1, "I", "D", "stan", 5000),
                new Employee(2, "E", "C", "stan", 4000)
            };

            // Act
            var result = SaveToFileService<Employee>.ToXmlFile(employees, 1);

            // Assert
            Assert.True(result);
            var filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Employees.xml";
            var serializer = new XmlSerializer(typeof(List<Employee>));
            using (var stream = File.OpenRead(filePath))
            {
                var fileContent = (List<Employee>)serializer.Deserialize(stream);
                Assert.Equal(2, fileContent.Count);

                Assert.Equal(1, fileContent[0].Id);
                Assert.Equal("I", fileContent[0].Name);
                Assert.Equal("D", fileContent[0].Surname);
                Assert.Equal("stan", fileContent[0].CompanyPosition);
                Assert.Equal(5000, fileContent[0].Salary);

                Assert.Equal(2, fileContent[1].Id);
                Assert.Equal("E", fileContent[1].Name);
                Assert.Equal("C", fileContent[1].Surname);
                Assert.Equal("stan", fileContent[1].CompanyPosition);
                Assert.Equal(4000, fileContent[1].Salary);
            }
        }
        [Fact]
        public void ToJsonFileShouldSaveToFile()
        {
            // Arrange
            var employees = new List<Employee>()
            {
                new Employee(1, "I", "D", "stan", 5000),
                new Employee(2, "E", "C", "stan", 4000)
            };
            var filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Employees.json";

            // Act
            var result = SaveToFileService<Employee>.ToJsonFile(employees, 1);

            // Assert
            Assert.True(result);

            var fileContent = File.ReadAllText(filePath);
            var deserializedEmployees = JsonConvert.DeserializeObject<List<Employee>>(fileContent);

            Assert.Equal(employees.Count, deserializedEmployees.Count);

            for (int i = 0; i < employees.Count; i++)
            {
                Assert.Equal(employees[i].Id, deserializedEmployees[i].Id);
                Assert.Equal(employees[i].Name, deserializedEmployees[i].Name);
                Assert.Equal(employees[i].Surname, deserializedEmployees[i].Surname);
                Assert.Equal(employees[i].CompanyPosition, deserializedEmployees[i].CompanyPosition);
                Assert.Equal(employees[i].Salary, deserializedEmployees[i].Salary);
            }
        }

    }
}
