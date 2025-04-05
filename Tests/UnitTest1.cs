using LegacyApp;
using System;
using System.ComponentModel.DataAnnotations;

namespace Tests;

public class UnitTest1
{
    [Fact]
    public void AddUser_NoCredit_ShouldAddUser()
    {
        // Arrange
        UserService service = new UserService();
        
        // Act
        bool canAdd = service.AddUser("Konrad", "Malewski", "malewski@gmail.pl", DateTime.Parse("1982-03-21"), 2);
        
        // Assert
        Assert.True(canAdd);
    }

    [Fact]
    public void CalcNewCreditLimit_NoCredit_ShouldSetHasNoCreditLimit()
    {
        // Arrange
        DefaultClientRepository clientRepository = new DefaultClientRepository();
        DefaultUserDataValidate dataValidate = new DefaultUserDataValidate();
        DefaultUserCreditService creditService = new DefaultUserCreditService();
        int clientId = 2;
        string firstName = "Konrad";
        string lastName = "Malewski";
        string email = "malewski@gmail.pl";
        DateTime dateOfBirth = DateTime.Parse("1982-03-21");
        
        // Act
        Client client = clientRepository.GetById(clientId);
        if (!dataValidate.IsValid(firstName, lastName, email, dateOfBirth))
            throw new ValidationException("Invalid User Data.");

        int creditLimit = creditService.GetCreditLimit(lastName);
        creditLimit = creditService.CalcNewCreditLimit(client.Type, creditLimit);
        var user = new User(firstName, lastName, email, dateOfBirth, client, creditLimit);
        
        // Assert
        Assert.False(user.HasCreditLimit);
    }
}