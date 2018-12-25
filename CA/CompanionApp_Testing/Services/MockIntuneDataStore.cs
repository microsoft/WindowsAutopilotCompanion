using CompanionApp_Testing.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanionApp_Testing.Services
{
    class MockIntuneDataStore : IIntuneDataStore<User>
    {
        List<User> users;
        public MockIntuneDataStore()
        {
            users = new List<User>();
            users.Add(new User() { DisplayName = "Manoj Jain1", GivenName = "Manoj1", Surname = "Jain1", UserPrincipalName = "manoj1@microsoft.com" });
            users.Add(new User() { DisplayName = "Manoj Jain2", GivenName = "Manoj2", Surname = "Jain2", UserPrincipalName = "manoj2@microsoft.com" });
            users.Add(new User() { DisplayName = "Manoj Jain3", GivenName = "Manoj3", Surname = "Jain3", UserPrincipalName = "manoj3@microsoft.com" });
            users.Add(new User() { DisplayName = "Manoj Jain4", GivenName = "Manoj4", Surname = "Jain4", UserPrincipalName = "manoj4@microsoft.com" });

        }
        public async Task<bool> AssignUserAsync(User user, Guid deviceId)
        {
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<User>> ListAllUsersAsync()
        {
            return await Task.FromResult(users);
        }

        public Task LogOutUser()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> SearchUserAsync(string userName)
        {
            return await Task.FromResult(users);
        }
    }
}
