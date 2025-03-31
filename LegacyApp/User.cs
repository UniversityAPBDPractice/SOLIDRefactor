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
        public User(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                throw new ValidationException("Specified name is null or empty");
            }
            
            if (!email.Contains("@") && !email.Contains("."))
            {
                throw new ValidationException("Specified email is invalid");
            }

            FirstName = firstName;
            LastName = lastName;
            EmailAddress = email;
            DateOfBirth = dateOfBirth;
            
            var clientRepository = new ClientRepository();
            Client = clientRepository.GetById(clientId);

            UpdateCreditLimit();
        }

        public int GetAge(DateTime before, DateTime now)
        {
            int age = now.Year - before.Year;
            if (now.Month < before.Month || (now.Month == before.Month && now.Day < before.Day)) age--;
            return age;
        }

        public void UpdateCreditLimit()
        {
            using (var userCreditService = new UserCreditService())
            {
                CreditLimit = userCreditService.GetCreditLimit(LastName, DateOfBirth);
                var pC = new Dictionary<string, int>()
                {
                    {"VeryImportantClient", -1},
                    {"ImportantClient", 2},
                };
                int coeff = InferCreditCoeff(pC);
                if (coeff == -1) HasCreditLimit = false;
                else
                {
                    CreditLimit *= coeff;
                }
            }
        }

        public int InferCreditCoeff(Dictionary<string, int> prioritiesAndCoefficients)
        {
            // To be able to specify NoCreditLimit, set Coeff to -1
            string priority = Client.Type;
            int coeff = prioritiesAndCoefficients.GetValueOrDefault(priority, 1); // By default, we leave the same credit
            if (coeff == -1)
            {
                return coeff;
            }
            return CreditLimit * coeff;
        }

        public bool HasCreditSmallerThan(int c)
        {
            return HasCreditLimit && CreditLimit < c;
        }
    }
}