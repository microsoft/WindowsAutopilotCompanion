using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;

namespace CompanionApp
{
    public interface IADALAuthenticator
    {
        IPlatformParameters PlatformParameters { get; set; }
        IPlatformParameters LogoutPlatformParameters { get; set; }
    }
}
