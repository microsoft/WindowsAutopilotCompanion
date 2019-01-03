using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace CompanionApp.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            ADALAuthenticator aDALAuthenticator = new ADALAuthenticator();
            IPlatformParameters PlatformParameters = new PlatformParameters(PromptBehavior.SelectAccount, true);
            IPlatformParameters PlatformParametersLogout = new PlatformParameters(PromptBehavior.Auto, true);
            aDALAuthenticator.PlatformParameters = PlatformParameters;
            aDALAuthenticator.LogoutPlatformParameters = PlatformParametersLogout;
            LoadApplication(new CompanionApp.App(aDALAuthenticator));
        }
    }
}
