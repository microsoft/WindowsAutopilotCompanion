using CompanionApp_Testing.Model;
using CompanionApp_Testing.ViewModel;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompanionApp_Testing.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersPage : ContentPage
    {
        UsersViewModel viewModel;

        public UsersPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new UsersViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as User;
            if (item == null)
                return;

            await Navigation.PushAsync(new UserDetailsPage(new UserDetailViewModel(item)));

            // Manually deselect item.
            UsersListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Add", "Hello Manoj", "Cancel");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Users.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}