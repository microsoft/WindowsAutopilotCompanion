using CompanionApp.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanionApp.Services
{
    public interface IIntuneDataStore<T>
    {
        Task<bool> AssignUserAsync(T user, Guid deviceId);
        Task<IEnumerable<T>> SearchUserAsync(string userName);
        Task<IEnumerable<T>> ListAllUsersAsync();
        Task LogOutUser();
        Task<Info> GetInfo();
    }
}
