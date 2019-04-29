using CompanionApp.Services;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;

namespace CompanionApp
{
    public interface IADALAuthenticator
    {
        // Implementataion based on sample code by Alexander Meijers, Mayur Tendulkar, and 
        // Vittorio Bertocci.  See http://www.appzinside.com/2016/02/22/implement-adal-for-cross-platform-xamarin-applications/
        // for details.

        Task<AuthenticationResultCode> Authenticate(string tenant, string resource, string clientId, string returnUri);
    }
}
