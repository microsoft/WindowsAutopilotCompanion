using CompanionApp.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanionApp.Services
{
    public interface IIntuneDataStore
    {
        Task<bool> AssignUserAsync(User user, Guid deviceId);
        Task<bool> UnAssignUserAsync(Guid deviceId);
        Task<IEnumerable<User>> SearchUserAsync(string userName);
        Task<IEnumerable<Device>> SearchDevicesBySerialAsync(string serial);
        Task<IEnumerable<Device>> SearchDevicesByZtdIdAsync(string ztdId);
        Task<IEnumerable<User>> ListAllUsersAsync();
        Task LogOutUser();
        Task<Info> GetInfo();
    }
}
