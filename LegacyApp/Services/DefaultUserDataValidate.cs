using System;
using System.ComponentModel.DataAnnotations;

namespace LegacyApp;

public class DefaultUserDataValidate : IUserDataValidate
{
    public bool IsValid(string firstName, string lastName, string email, DateTime dateOfBirth)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
        {
            return false;
        }
            
        if (!email.Contains("@") && !email.Contains("."))
        {
            return false;
        }

        return true;
    }
}