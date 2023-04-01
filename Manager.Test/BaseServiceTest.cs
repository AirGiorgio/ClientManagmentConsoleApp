using Manager.App.Abstract;
using Moq;
using Xunit;
using ClientManagmentConsoleApp;
using Manager.App.Concrete;
using System;
using FluentAssertions;
using System.Net.Http.Headers;

namespace Manager.Test
{
    public class BaseServiceTest
    {

        [Theory]
        [InlineData(6, "Nowy", "Pracownik", "Ksiegowy", 3000)]
        [InlineData(7, "Inny", "Pracownik", "Manager", 5000)]
        public void BaseServiceShouldAddIfPersonIsValid(int id, string name, string surname, string position, decimal salary)
        {
            //arrange
            Employee employee = new Employee(id, name, surname, position, salary);
            BaseService<Employee> employeeBaseService = new BaseService<Employee>();

            //act
            OperationResult employeeSuccedResult = employeeBaseService.Add(employee);
            
            //assert
            Assert.True(employeeSuccedResult.Success);
            employeeSuccedResult.Message.Should().Be("Dodano do listy!");
            employeeSuccedResult.Success.Should().Be(true);
            employeeBaseService.Someone.Should().Contain(employee);
            employeeBaseService.Someone.Should().HaveCount(1);
        }

        [Fact]
        public void BaseServiceShouldNotAddIfPersonIsNull()
        {
            //Arrange
            Employee nullEmployee = null;
            BaseService<Employee> employeeBaseService = new BaseService<Employee>();

            //Act
            OperationResult employeeNullResult = employeeBaseService.Add(nullEmployee);

            //Assert
            employeeNullResult.Success.Should().BeFalse();
            employeeNullResult.Message.Should().Be("Nie można dodać do listy!");
        }

        [Fact]
        public void BaseServiceShouldNotAddIfPersonExists()
        {
            //arrange
            Employee employee1 = new Employee(6, "Nowy", "Pracownik", "Ksiegowy", 3000);
            Employee employee2 = new Employee(6, "Inny", "Pracownik", "Manager", 5000);
            BaseService<Employee> employeeBaseService = new BaseService<Employee>();

            //act
            OperationResult successResult = employeeBaseService.Add(employee1);
            OperationResult falseResult = employeeBaseService.Add(employee2);

            //Assert
            Assert.False(falseResult.Success);
            falseResult.Message.Should().Be("Osoba o tym Id jest już na liście!");
        }

        [Theory]
        [InlineData(1, "HH", "KK", "1990-01-01", "123456789", true)]
        [InlineData(2, "LL", "MM", "1995-02-02", "123456789", true)]
        [InlineData(3, "VV", "WW", "1985-04-04", "123456789", true)]
        
        public void BaseServiceShouldRemoveWhenPersonIsValid(int id, string name, string surname, string BirthDate, string Telephone, bool expectedResult)
        {
            // Arrange
            Client client = new Client(id, name, surname, DateOnly.Parse(BirthDate), Telephone);
            BaseService<Client> clientBaseService = new BaseService<Client>();
            clientBaseService.Add(client);

            // Act
            OperationResult result = clientBaseService.Remove(client);

            // Assert
            Assert.Equal(expectedResult, result.Success);
            Assert.NotEqual(expectedResult, clientBaseService.Someone.Contains(client));
        }

        [Fact]
        public void BaseServiceShouldNotRemoveWhenPersonIsNull( )
        {         
            // Arrange
            Client nullClient = null;
            BaseService<Client> clientBaseService = new BaseService<Client>();
            clientBaseService.Add(nullClient);
          
            // Act      
            OperationResult nullResult = clientBaseService.Remove(nullClient);

            // Assert
            nullResult.Success.Should().BeFalse();
            nullResult.Message.Should().Be("Nie znaleziono nikogo o podanym Id!");
        }

        [Theory]
        [InlineData(6, "Nowy", "Pracownik", "Ksiegowy", 3000, 6, "pp", "oo", "stanowisko1", 5500)]
        public void BaseServiceShouldEditIfIdIsValid(int id, string name, string surname, string companyPosition, int salary,
        int editId, string editName, string editSurname, string editPosition, int editSalary)
        {
            //arrange
            Employee employee1 = new Employee(id, name, surname, companyPosition, salary);
            BaseService<Employee> employeeBaseService = new BaseService<Employee>();
            employeeBaseService.Add(employee1);

            // Act
            OperationResult editResult = employeeBaseService.Edit(new Employee(editId, editName, editSurname, editPosition, editSalary));

            // Assert
            editResult.Success.Should().BeTrue();
            editResult.Message.Should().Be("Zaktualizowano!");
           
            employee1.Name.Should().Be(editName);
            employee1.CompanyPosition.Should().Be(editPosition);
            employee1.Salary.Should().Be(editSalary);  
        }

        [Theory]
        [InlineData(7, "Nowy", "Pracownik", "Ksiegowy", 3000, 11, "dd", "ss", "stanowisko2", 4500)]
        public void BaseServiceShouldNotEditIfIdIsNotValid(int id, string name, string surname, string companyPosition, int salary,
        int editId, string editName, string editSurname, string editPosition, int editSalary)
        {
            //Arrange
            Employee employee1 = new Employee(id, name, surname, companyPosition, salary);
            BaseService<Employee> employeeBaseService = new BaseService<Employee>();
            employeeBaseService.Add(employee1);

            // Act
            OperationResult editResult = employeeBaseService.Edit(new Employee(editId, editName, editSurname, editPosition, editSalary));

            //Assert
            editResult.Success.Should().BeFalse();
            editResult.Message.Should().Be("Nie znaleziono nikogo o podanym Id!"); 
        }

    }
}
