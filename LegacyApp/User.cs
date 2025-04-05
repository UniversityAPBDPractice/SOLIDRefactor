using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LegacyApp
{
    public class User : IAgeCalculator
    {
        public Client Client { get; internal set; }
        public DateTime DateOfBirth { get; internal set; }
        public string EmailAddress { get; internal set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public bool HasCreditLimit { get; internal set; }
        public int CreditLimit { get; internal set; }
        
        public User(string firstName, string lastName, string email, DateTime dateOfBirth, Client client, int creditLimit)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = email;
            DateOfBirth = dateOfBirth;
            CreditLimit = creditLimit;
            Client = client;
        }

        public int GetAge(DateTime before, DateTime now)
        {
            int age = now.Year - before.Year;
            if (now.Month < before.Month || (now.Month == before.Month && now.Day < before.Day)) age--;
            return age;
        }

        public bool HasCreditSmallerThan(int c)
        {
            return HasCreditLimit && CreditLimit < c;
        }
    }
}