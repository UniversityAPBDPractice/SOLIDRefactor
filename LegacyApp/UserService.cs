using System;
// TODO
// Apply SRP

// Apply OCP

// Apply Dependency Inversion

// Make sure you applied Extraction, Inversion and use Neat Names
namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId, DateTime timeNow)
        {
            var user = new User(firstName, lastName, email, dateOfBirth, clientId);
            bool userCanBeAdded = CanAddUser(user, timeNow, dateOfBirth);
            if (userCanBeAdded)
            {
                UserDataAccess.AddUser(user);
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
