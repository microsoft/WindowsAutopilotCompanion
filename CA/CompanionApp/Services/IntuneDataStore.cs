using CompanionApp.Model;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CompanionApp.Services
{
    class IntuneDataStore : IIntuneDataStore
    {
        HttpClient graphClient;

        public IntuneDataStore()
        {

        }

        public async Task<bool> AssignUserAsync(User user, Guid deviceId)
        {
            var token = ADALAuthentication.Instance.AuthResult.AccessToken;
            graphClient = new HttpClient();
            graphClient.DefaultRequestHeaders.Add("Authorization", token);

            var data = new
            {
                userPrincipalName = user.UserPrincipalName,
                addressableUserName = user.DisplayName
            };

            var serializedItem = JsonConvert.SerializeObject(data);

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

        public async Task<bool> UnAssignUserAsync(Guid deviceId)
        {
            var token = ADALAuthentication.Instance.AuthResult.AccessToken;
            graphClient = new HttpClient();
            graphClient.DefaultRequestHeaders.Add("Authorization", token);

            string stringUrlassignUserUrl = string.Format("https://graph.microsoft.com/beta/devicemanagement/windowsAutopilotDeviceIdentities/{0}/unassignUserFromDevice", deviceId);
            var result = await graphClient.PostAsync(
                stringUrlassignUserUrl,
                new StringContent(String.Empty, Encoding.UTF8, "application/json"));

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<User>> ListAllUsersAsync()
        {
            List<User> users = new List<User>();
            var token = ADALAuthentication.Instance.AuthResult.AccessToken;
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
            // DependencyService.Get<IADALAuthenticator>().PlatformParameters = null;
        }

        public async Task<Info> GetInfo()
        {
            Info i = new Info();

            var token = ADALAuthentication.Instance.AuthResult.AccessToken;
            graphClient = new HttpClient();
            graphClient.DefaultRequestHeaders.Add("Authorization", token);

            var result = await graphClient.GetStringAsync("https://graph.microsoft.com/v1.0/organization");

            JToken jtokenResult = JsonConvert.DeserializeObject<JToken>(result);
            JArray JsonValues = jtokenResult["value"] as JArray;

            foreach (var item in JsonValues)
            {
                i.TenantID = item["id"].Value<string>();
                i.TenantDisplayName = item["displayName"].Value<string>();
                JArray domains = item["verifiedDomains"] as JArray;
                foreach (var domain in domains)
                {
                    if (domain["isInitial"].Value<bool>())
                    {
                        i.TenantName = domain["name"].Value<string>();
                    }
                }
            }

            return await Task.FromResult(i);
        }

        public async Task<IEnumerable<Model.Device>> SearchDevicesBySerialAsync(string serial)
        {
            IEnumerable<DeviceCategory> categories = await ListAllCategoriesAsync();

            List<Model.Device> devices = new List<Model.Device>();
            var token = ADALAuthentication.Instance.AuthResult.AccessToken;
            graphClient = new HttpClient();
            graphClient.DefaultRequestHeaders.Add("Authorization", token);

            var result = await graphClient.GetStringAsync("https://graph.microsoft.com/beta/deviceManagement/windowsAutopilotDeviceIdentities?$filter=contains(serialNumber,'" + serial + "')");

            JToken jtokenResult = JsonConvert.DeserializeObject<JToken>(result);
            JArray JsonValues = jtokenResult["value"] as JArray;

            foreach (var item in JsonValues)
            {
                devices.Add(await ProcessDevice(item, categories));
            }
            return devices;
        }

        public async Task<IEnumerable<Model.Device>> SearchDevicesByZtdIdAsync(string ztdId)
        {
            IEnumerable<DeviceCategory> categories = await ListAllCategoriesAsync();

            List<Model.Device> devices = new List<Model.Device>();
            var token = ADALAuthentication.Instance.AuthResult.AccessToken;
            graphClient = new HttpClient();
            graphClient.DefaultRequestHeaders.Add("Authorization", token);

            var result = await graphClient.GetStringAsync("https://graph.microsoft.com/beta/deviceManagement/windowsAutopilotDeviceIdentities/" + ztdId + "?$expand=deploymentProfile,intendedDeploymentProfile");

            JToken item = JsonConvert.DeserializeObject<JToken>(result);

            Model.Device device = await ProcessDevice(item, categories);
            devices.Add(device);

            return devices;
        }

        private async Task<Model.Device> ProcessDevice(JToken item, IEnumerable<DeviceCategory> categories)
        {
            Model.Device device = new Model.Device();
            device.SerialNumber = item["serialNumber"].Value<string>();
            device.Manufacturer = item["manufacturer"].Value<string>();
            device.Model = item["model"].Value<string>();
            device.GroupTag = item["groupTag"].Value<string>();
            device.PurchaseOrderNumber = item["purchaseOrderIdentifier"].Value<string>();
            device.AddressibleUserName = item["addressableUserName"].Value<string>();
            device.UserPrincipalName = item["userPrincipalName"].Value<string>();
            device.AzureActiveDirectoryDeviceId = item["azureActiveDirectoryDeviceId"].Value<string>();
            device.ManagedDeviceId = item["managedDeviceId"].Value<string>();
            device.ZtdId = item["id"].Value<string>();
            device.CategoryList = categories;

            // Get details from Autopilot device
            var autopilotDetails = await graphClient.GetStringAsync("https://graph.microsoft.com/beta/deviceManagement/windowsAutopilotDeviceIdentities/" + device.ZtdId + "?$expand=deploymentProfile,intendedDeploymentProfile");
            JToken autopilotToken = JsonConvert.DeserializeObject<JToken>(autopilotDetails);
            if (autopilotToken["deploymentProfile"].HasValues)
            {
                device.DeploymentProfile = autopilotToken["deploymentProfile"]["displayName"].Value<string>();
            }

            // Get details from Intune device
            var intuneDetails = await graphClient.GetStringAsync("https://graph.microsoft.com/v1.0/deviceManagement/managedDevices/" + device.ManagedDeviceId + "/deviceCategory");
            JToken intuneToken = JsonConvert.DeserializeObject<JToken>(intuneDetails);

            device.ManagedDeviceCategoryId = intuneToken["id"].Value<string>();
            // Flaky, look up the name instead of this: device.ManagedDeviceCategory = intuneToken["displayName"].Value<string>();
            foreach (DeviceCategory cat in categories)
            {
                if (cat.Id == device.ManagedDeviceCategoryId)
                    device.ManagedDeviceCategory = cat.DisplayName;
            }
            return device;
        }

        public async Task<IEnumerable<DeviceCategory>> ListAllCategoriesAsync()
        {
            List<DeviceCategory> categories = new List<DeviceCategory>();
            var token = ADALAuthentication.Instance.AuthResult.AccessToken;
            graphClient = new HttpClient();
            graphClient.DefaultRequestHeaders.Add("Authorization", token);

            var result = await graphClient.GetStringAsync("https://graph.microsoft.com/v1.0/deviceManagement/deviceCategories");

            JToken jtokenResult = JsonConvert.DeserializeObject<JToken>(result);
            JArray JsonValues = jtokenResult["value"] as JArray;

            // Add the "Unassigned" category
            categories.Add(new DeviceCategory() { Id = Guid.Empty.ToString(), DisplayName = "Unassigned" });
            foreach (var item in JsonValues)
            {
                DeviceCategory cat = new DeviceCategory();
                cat.DisplayName = item["displayName"].Value<string>();
                cat.Id = item["id"].Value<string>();
                categories.Add(cat);
            }

            return categories;
        }

        public async Task<bool> AssignCategory(Model.Device device)
        {
            // For whatever reason, only the beta namespace works for this, so don't change 
            // it unless you know the v1.0 namespace now works.

            var token = ADALAuthentication.Instance.AuthResult.AccessToken;
            graphClient = new HttpClient();
            graphClient.DefaultRequestHeaders.Add("Authorization", token);

            string stringDeviceUrl = string.Format("https://graph.microsoft.com/beta/deviceManagement/managedDevices('{0}')/deviceCategory/$ref", device.ManagedDeviceId);
            string categoryId = String.Empty;
            foreach (var category in device.CategoryList)
            {
                if (category.Id == device.ManagedDeviceCategoryId)
                {
                    categoryId = category.Id;
                }
            }

            string payload = string.Format("{{ \"@odata.id\": \"https://graph.microsoft.com/beta/deviceManagement/deviceCategories/{0}\"}}", categoryId);
            var result = await graphClient.PutAsync(
                stringDeviceUrl, 
                new StringContent(payload, Encoding.UTF8, "application/json"));

            if (result.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

    }
}

public static class HttpClientExtensions
{
    public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent iContent)
    {
        var method = new HttpMethod("PATCH");
        var request = new HttpRequestMessage(method, new Uri(requestUri))
        {
            Content = iContent
        };

        HttpResponseMessage response = new HttpResponseMessage();
        response = await client.SendAsync(request);

        return response;
    }
}