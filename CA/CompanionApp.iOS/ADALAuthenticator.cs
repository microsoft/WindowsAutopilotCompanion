using System.Threading.Tasks;
using CompanionApp.Services;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(CompanionApp.iOS.ADALAuthenticator))]
namespace CompanionApp.iOS
{
    public class ADALAuthenticator : IADALAuthenticator
    {
        public Task<AuthenticationResultCode> Authenticate(string resource, string clientId, string returnUri)
        {
            // IPlatformParameters PlatformParametersLogout = new PlatformParameters(controller, true, PromptBehavior.Auto);

            ADALAuthentication.Instance.platformParameters = new PlatformParameters(UIApplication.SharedApplication.KeyWindow.RootViewController, true, PromptBehavior.SelectAccount);
            return ADALAuthentication.Instance.Authenticate(resource, clientId, returnUri);
        }
    }
}