using CompanionApp.Model;
using CompanionApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompanionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;

        public MenuPage()
        {
            InitializeComponent();
            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Info, Title="Info" },
                new HomeMenuItem {Id = MenuItemType.DeviceSearch, Title="Device Search" },
                new HomeMenuItem {Id = MenuItemType.Sync, Title="Sync" },
                new HomeMenuItem {Id = MenuItemType.Logout, Title="Logout" },
                new HomeMenuItem {Id = MenuItemType.About, Title="About"}
            };

            ListViewMenu.ItemsSource = menuItems;
            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.BackgroundColor = Color.LightGray;
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                if (((HomeMenuItem)e.SelectedItem).Id == MenuItemType.Logout)
                {
                    ListViewMenu.ItemsSource = null;
                    ListViewMenu.IsVisible = false;
                    ListViewMenu.SeparatorVisibility = SeparatorVisibility.None;
                    RootPage.Master.IsVisible = false;
                }
                else if (((HomeMenuItem)e.SelectedItem).Id == MenuItemType.Sync)
                {
                    IIntuneDataStore dataStore = DependencyService.Get<IIntuneDataStore>();
                    Task task = Task.Run(async () => await dataStore.Sync());
                    RootPage.IsPresented = false;
                    return;
                }
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}