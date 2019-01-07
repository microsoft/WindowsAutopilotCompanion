using CompanionApp.Model;
using System.Collections.Generic;

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
                new HomeMenuItem {Id = MenuItemType.ScanQR, Title="Scan QR Page" },
                //new HomeMenuItem {Id = MenuItemType.Browse, Title="List User" },
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
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}