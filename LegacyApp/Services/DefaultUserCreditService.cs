using System;
using System.Collections.Generic;
using System.Threading;

namespace LegacyApp
{
    public class DefaultUserCreditService : IDisposable, IUserCreditService
    {
        /// <summary>
        /// Simulating database
        /// </summary>
        private Dictionary<string, int> _prioritiesAndCoefficients;
        public DefaultUserCreditService(Dictionary<string, int> prioritiesAndCoefficients)
        {
            _prioritiesAndCoefficients = prioritiesAndCoefficients;
        }
        public DefaultUserCreditService()
        {
            var pC = new Dictionary<string, int>()
            {
                {"VeryImportantClient", -1},
                {"ImportantClient", 2},
            };
            _prioritiesAndCoefficients = pC;
        }
        private readonly Dictionary<string, int> _database =
            new Dictionary<string, int>()
            {
                {"Kowalski", 200},
                {"Malewski", 20000},
                {"Smith", 10000},
                {"Doe", 3000},
                {"Kwiatkowski", 1000}
            };
        
        public void Dispose()
        {
            //Simulating disposing of resources
        }

        /// <summary>
        /// This method is simulating contact with remote service which is used to get info about someone's credit limit
        /// </summary>
        /// <returns>Client's credit limit</returns>
        public int GetCreditLimit(string lastName)
        {
            int randomWaitingTime = new Random().Next(3000);
            Thread.Sleep(randomWaitingTime);

            if (_database.ContainsKey(lastName))
                return _database[lastName];

            throw new ArgumentException($"Client {lastName} does not exist");
        }
        
        public int CalcNewCreditLimit(string priority, int prevLimit)
        {
            int coeff = InferCreditCoeff(priority);
            return prevLimit * coeff;
        }
        public int InferCreditCoeff(string priority)
        {
            // To be able to specify NoCreditLimit, set Coeff to -1
            int coeff = _prioritiesAndCoefficients.GetValueOrDefault(priority, 1); // By default, we leave the same credit
            if (coeff == -1)
            {
                return coeff;
            }
            return coeff;
        }
    }
}