using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

[assembly: Xamarin.Forms.Dependency(typeof(CompanionApp.Droid.ADALAuthenticator))]
namespace CompanionApp.Droid
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