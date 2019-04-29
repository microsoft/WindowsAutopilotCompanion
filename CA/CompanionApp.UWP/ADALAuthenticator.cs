using System.Threading.Tasks;
using CompanionApp.Services;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

[assembly: Xamarin.Forms.Dependency(typeof(CompanionApp.UWP.ADALAuthenticator))]
namespace CompanionApp.UWP
{
    public class ADALAuthenticator : IADALAuthenticator
    {
        public Task<AuthenticationResultCode> Authenticate(string tenant, string resource, string clientId, string returnUri)
        {
            // IPlatformParameters PlatformParametersLogout = new PlatformParameters(PromptBehavior.Auto, true);

            ADALAuthentication.Instance.platformParameters = new PlatformParameters(PromptBehavior.SelectAccount, true);
            return ADALAuthentication.Instance.Authenticate(tenant, resource, clientId, returnUri);
        }
    }
}