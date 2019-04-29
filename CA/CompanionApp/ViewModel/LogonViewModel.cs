using System;
using System.Windows.Input;
using Xamarin.Forms;
using CompanionApp.Services;

namespace CompanionApp.ViewModel
{
    public class LogonViewModel : BaseViewModel
    {
        public LogonViewModel()
        {
            Title = "Logon";

            LogonCommand = new Command(() => this.PerformLogon(tenant));
            DemoCommand = new Command(() => this.PerformDemo());
        }

        public ICommand LogonCommand { get; }
        public ICommand DemoCommand { get; }
        
        string results = string.Empty;
        public string Results
        {
            get { return results; }
            set { SetProperty(ref results, value); }
        }

        string tenant = string.Empty;
        public string Tenant
        {
            get { return tenant; }
            set { SetProperty(ref tenant, value); }
        }

        public async void PerformLogon(string tenant)
        {
            string applicationId = "4253c270-b756-4c48-9e28-8f202b6798ec";
            string authority = "https://graph.microsoft.com";
            string redirectUri = "urn:ietf:wg:oauth:2.0:oob";
            string authTenant = "common";
            if (!String.IsNullOrEmpty(tenant))
                authTenant = tenant;

            DependencyService.Register<IntuneDataStore>();

            IADALAuthenticator auth = DependencyService.Get<IADALAuthenticator>();
            AuthenticationResultCode code = await auth.Authenticate(authTenant, authority, applicationId, redirectUri);
            switch (code)
            {
                case AuthenticationResultCode.Succesfull:
                    Results = "Signed on as  " + ADALAuthentication.Instance.AuthResult.UserInfo.DisplayableId;
                    break;

                case AuthenticationResultCode.Canceled:
                    Results = "Logon cancelled";
                    break;

                case AuthenticationResultCode.Denied:
                    Results = "Access denied";
                    break;

                default:
                    Results = "Unexpected error: " + ADALAuthentication.Instance.Error;
                    break;
            }

            App.Current.MainPage = new Views.MainPage();
        }

        public async void PerformDemo()
        {
            DependencyService.Register<MockIntuneDataStore>();
            App.Current.MainPage = new Views.MainPage();
        }
    }
}
