using System;
using ExampleSignalR.DAL.Database;

namespace ExampleSignalR.DAL
{
    public class Command
    {
        private ChatDatabaseDataContext dataContext;

        public Command()
        {
            dataContext = new ChatDatabaseDataContext();
        }

        public bool AddUser(string userName, string password, string firstName, string lastName)
        {
            try
            {
                var value = dataContext.AddUser(userName, password, firstName, lastName);
                return value == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetUser(string userName, string password)
        {
            var value = dataContext.GetUser(userName, password);
            var enumerator = value.GetEnumerator();
            enumerator.MoveNext();
            if (enumerator.Current != null)
            {
                return enumerator.Current.UserName;
            }

            return string.Empty;
        }
    }
}