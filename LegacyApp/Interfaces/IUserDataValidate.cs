using System;

namespace LegacyApp;

public interface IUserDataValidate
{
    bool IsValid(string firstName, string lastName, string email, DateTime dateOfBirth);
}