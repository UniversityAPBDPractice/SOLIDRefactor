using System;
namespace LegacyApp
{
    public class UserService
    {
        private IUserDataAccess _dataAccess;
        public UserService()
        {
            _dataAccess = new DefaultUserDataAccess();
        }

        public UserService(IUserDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId, DateTime timeNow)
        {
            var user = new User(firstName, lastName, email, dateOfBirth, clientId);
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
