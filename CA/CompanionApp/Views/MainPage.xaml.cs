using CompanionApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompanionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();

        public MainPage()
        {
            InitializeComponent();
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Info:
                        MenuPages.Add(id, new NavigationPage(new InfoPage()));
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                    case (int)MenuItemType.DeviceSearch:
                        MenuPages.Add(id, new NavigationPage(new DeviceSearchPage()));
                        break;
                    case (int)MenuItemType.Logout:
                        MenuPages.Add(id, new NavigationPage(new Logout()));
                        break;
                }
            }
            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}
