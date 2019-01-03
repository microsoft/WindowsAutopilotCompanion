
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace CompanionApp.Droid
{
    [Activity(Label = "White Glove Companion App", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            ADALAuthenticator aDALAuthenticator = new ADALAuthenticator();
            IPlatformParameters PlatformParameters = new PlatformParameters(this, true, PromptBehavior.SelectAccount);
            IPlatformParameters PlatformParametersLogout = new PlatformParameters(this, true, PromptBehavior.SelectAccount);
            aDALAuthenticator.PlatformParameters = PlatformParameters;
            aDALAuthenticator.LogoutPlatformParameters = PlatformParametersLogout;
            LoadApplication(new App(aDALAuthenticator));
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
        }
    }
    
}
