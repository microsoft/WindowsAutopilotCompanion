using CompanionApp.Model;
using CompanionApp.ViewModel;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompanionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersPage : ContentPage
    {
        UsersViewModel viewModel;
        string ztdid = string.Empty;
        public UsersPage(string ztdId)
        {
            InitializeComponent();

            BindingContext = viewModel = new UsersViewModel();
            this.ztdid = ztdId;
        }

        async void OnItemSelected(object sender, ItemTappedEventArgs args)
        {
            var item = args.Item as User;

            if (item == null)
                return;

            item.Ztdid = this.ztdid;
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