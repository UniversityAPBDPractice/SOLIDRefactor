namespace LegacyApp;

public class DefaultUserDataAccess : IUserDataAccess
{
    public void AddUser(User user)
    {
        UserDataAccess.AddUser(user);
    }
}