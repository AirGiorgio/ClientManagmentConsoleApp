using Manager.App.Abstract;
using Moq;
using Xunit;
using ClientManagmentConsoleApp;
using Manager.App.Concrete;
using System;
using FluentAssertions;

namespace Manager.Test
{
    public class EmployeeServiceTest
    {
        [Fact]
        public void EmployeeListTest()
        {
            EmployeeService employeeService = new EmployeeService();

            //Assert
            employeeService.GetEmployees().Should().BeEquivalentTo(employeeService.Someone);
        }
        [Fact]
        public void GetEmployeeByIdTest()
        {
            //arrange
            Employee employee = new Employee(6, "Ktos", "Tam", "stanowisko", Decimal.Parse("5000"));
            Employee notAddedEmployee = new Employee(7, "Ktos", "Tam", "stanowisko", Decimal.Parse("5000"));
            EmployeeService employeeService = new EmployeeService();

            //act
            employeeService.Add(employee);

            // assert
            employeeService.GetEmployeeById(employee.Id).Should().Be(employee);
            employeeService.GetEmployeeById(notAddedEmployee.Id).Should().Be(null);
        }
      

        [Fact]
        public void EmployeeAddTest()
        {
            EmployeeService employeeService = new EmployeeService();

            //assert
            employeeService.Add("10", "Kolejny", "Pracownik", "stanowisko", "5000").Should().NotBe(null).And.BeOfType<Employee>();
        }
    
    }
}