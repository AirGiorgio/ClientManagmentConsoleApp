using Manager.App.Abstract;
using Moq;
using Manager.App.Abstract;
using Moq;
using Xunit;
using ClientManagmentConsoleApp;
using Manager.App.Concrete;
using System;
using FluentAssertions;

namespace Manager.Test
{
    public class ClientServiceTest
    {
        [Fact]
        public void ClientListTest()
        {
            //arrange
            ClientService clientService = new ClientService();
           
            //Assert
            clientService.GetClients().Should().BeEquivalentTo(clientService.Someone);
        }

        [Fact]
        public void GetClientByIdTest()
        { 
            //arrange
            Client client = new Client(6, "Ktos", "Tam", DateOnly.Parse("2000-02-02"), "999888777");
            Client notAddedClient = new Client(7, "ja", "ds", DateOnly.Parse("2000-02-02"), "123456789");

            ClientService clientService = new ClientService();

            //act
            clientService.Add(client);
            
            // assert
            clientService.GetClientById(client.Id).Should().Be(client);
            clientService.GetClientById(notAddedClient.Id).Should().Be(null);
           
        }

        [Fact]
        public void ClientAddTest()
        {
            //arrange
            ClientService clientService = new ClientService();

            //assert
            clientService.Add("10", "Kolejny", "Klient", "1999-09-09", "000111222").Should().NotBe(null).And.BeOfType<Client>();
        }
    }
}
