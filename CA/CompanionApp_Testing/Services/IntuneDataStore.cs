using CompanionApp_Testing.Model;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CompanionApp_Testing.Services
{
    class IntuneDataStore : IIntuneDataStore<User>
    {
        HttpClient graphClient;
        Uri redirectUri = new Uri("urn:ietf:wg:oauth:2.0:oob");
        IPlatformParameters platformParameters;
        public IntuneDataStore()
        {
            platformParameters = DependencyService.Get<IADALAuthenticator>().PlatformParameters;
        }
        public async Task<bool> AssignUserAsync(User user, Guid deviceId)
        {
            var token = await this.GetTokenCache();
            graphClient = new HttpClient();

            graphClient.DefaultRequestHeaders.Add("Authorization", token);

            var data = new
            {
                userPrincipalName = user.UserPrincipalName,
                addressableUserName = user.DisplayName
            };

            var serializedItem = JsonConvert.SerializeObject(user);

            string stringUrlassignUserUrl = string.Format("https://graph.microsoft.com/beta/devicemanagement/windowsAutopilotDeviceIdentities/{0}/AssignUserTodevice", deviceId);
            var result = await graphClient.PostAsync(
                stringUrlassignUserUrl,
                new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<User>> ListAllUsersAsync()
        {
            List<User> users = new List<User>();
            var token = await this.GetTokenCache();
            graphClient = new HttpClient();
            graphClient.DefaultRequestHeaders.Add("Authorization", token);

            //var result = await graphClient.GetStringAsync($"users");
            var result = await graphClient.GetStringAsync("https://graph.microsoft.com/beta/users");

            JToken jtokenResult = JsonConvert.DeserializeObject<JToken>(result);
            JArray JsonValues = jtokenResult["value"] as JArray;

            foreach (var item in JsonValues)
            {
                User user = new User();
                user.DisplayName = item["displayName"].Value<string>();
                user.Surname = item["givenName"].Value<string>();
                user.UserPrincipalName = item["userPrincipalName"].Value<string>();
                users.Add(user);
            }

            return users;
        }

        public async Task<IEnumerable<User>> SearchUserAsync(string userName)
        {
            List<User> users = new List<User>();

            return await Task.FromResult(users);
        }

        public async Task LogOutUser()
        {
            this.platformParameters = DependencyService.Get<IADALAuthenticator>().LogoutPlatformParameters;
            AuthenticationResult result = await GetAuthorizationHeader();
        }

        async Task<string> GetTokenCache()
        {
            AuthenticationResult authenticationResult = await this.GetAuthorizationHeader();
            return authenticationResult.CreateAuthorizationHeader();
        }

        async Task<AuthenticationResult> GetAuthorizationHeader()
        {
            string applicationId = "4253c270-b756-4c48-9e28-8f202b6798ec";
            string authority = "https://login.microsoftonline.com/common/";
            //Uri redirectUri = new Uri("urn:ietf:wg:oauth:2.0:oob");
            //Uri redirectUri = new Uri("ms-app://s-1-15-2-3098116164-3842758157-1170788177-1493719480-3788797704-593311791-301083370/");

            AuthenticationContext context = new AuthenticationContext(authority);
            AuthenticationResult result = await context.AcquireTokenAsync("https://graph.microsoft.com", applicationId, redirectUri, this.platformParameters);

            return result;
        }
    }
}
