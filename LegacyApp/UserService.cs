using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LegacyApp
{
    public class UserService
    {
        private IUserDataAccess _dataAccess;
        private IUserDataValidate _dataValidate;
        private IClientRepository _clientRepository;
        private IUserCreditService _creditService;
        public UserService()
        {
            _dataAccess = new DefaultUserDataAccess();
            _dataValidate = new DefaultUserDataValidate();
            _clientRepository = new DefaultClientRepository();
            _creditService = new DefaultUserCreditService();
        }
        public UserService(
            IUserDataAccess dataAccess,
            IUserDataValidate dataValidate,
            IClientRepository clientRepository,
            IUserCreditService creditService
            )
        {
            _dataAccess = dataAccess;
            _dataValidate = dataValidate;
            _clientRepository = clientRepository;
            _creditService = creditService;
        }
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId, DateTime timeNow)
        {
            
            Client client = _clientRepository.GetById(clientId);
            if (!_dataValidate.IsValid(firstName, lastName, email, dateOfBirth))
                throw new ValidationException("Invalid User Data.");

            int creditLimit = _creditService.GetCreditLimit(lastName);
            creditLimit = _creditService.CalcNewCreditLimit(client.Type, creditLimit);
            var user = new User(firstName, lastName, email, dateOfBirth, client, creditLimit);
            bool userCanBeAdded = CanAddUser(user, timeNow, dateOfBirth);
            if (userCanBeAdded)
            {
                _dataAccess.AddUser(user);
                return true;
            }
            return false;
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            return AddUser(firstName, lastName, email, dateOfBirth, clientId, DateTime.Now);
        }

        public bool CanAddUser(User user, DateTime timeNow, DateTime before)
        {
            int minAge = 21;
            int minCredit = 500;
            
            if (user.GetAge(before, timeNow) < minAge)
            {
                return false;
            }
            if (user.HasCreditSmallerThan(minCredit))
            {
                return false;
            }
            return true;
        }
    }
}
