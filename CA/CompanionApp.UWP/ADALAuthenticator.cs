using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

[assembly: Xamarin.Forms.Dependency(typeof(CompanionApp.UWP.ADALAuthenticator))]
namespace CompanionApp.UWP
{
    public class ADALAuthenticator : IADALAuthenticator
    {
        public IPlatformParameters PlatformParameters
        {
            get;
            set;
        }

        public IPlatformParameters LogoutPlatformParameters
        {
            get;
            set;
        }
    }
}