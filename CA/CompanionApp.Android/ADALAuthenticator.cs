using System.Threading.Tasks;
using Android.App;
using CompanionApp.Services;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(CompanionApp.Droid.ADALAuthenticator))]
namespace CompanionApp.Droid
{
    public class ADALAuthenticator : IADALAuthenticator
    {
        public Task<AuthenticationResultCode> Authenticate(string tenant, string resource, string clientId, string returnUri)
        {
            // IPlatformParameters PlatformParametersLogout = new PlatformParameters(this, true, PromptBehavior.SelectAccount);

            ADALAuthentication.Instance.platformParameters = new PlatformParameters((Activity)Forms.Context, true, PromptBehavior.SelectAccount);
            return ADALAuthentication.Instance.Authenticate(tenant, resource, clientId, returnUri);
        }

    }
}