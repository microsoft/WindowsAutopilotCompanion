using CompanionApp.Services;
using CompanionApp.Views;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CompanionApp
{
    public partial class App : Application
    {
        public App(IADALAuthenticator platformParameters)
        {
            try
            {
                InitializeComponent();

                DependencyService.Register<IntuneDataStore>();
                DependencyService.Resolve<IADALAuthenticator>().PlatformParameters = platformParameters.PlatformParameters;
                DependencyService.Resolve<IADALAuthenticator>().LogoutPlatformParameters = platformParameters.LogoutPlatformParameters;
                MainPage = new MainPage();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.Data);
                Debug.WriteLine(e.InnerException);
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
