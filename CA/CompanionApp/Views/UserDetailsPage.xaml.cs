using CompanionApp.Model;
using CompanionApp.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompanionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDetailsPage : ContentPage
    {
        UserDetailViewModel viewModel;

        public UserDetailsPage(UserDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }
        public async void AddItem_Clicked(object sender, EventArgs e)
        {
            bool returnValue = await viewModel.DataStore.AssignUserAsync(new User() { DisplayName = viewModel.DisplayName, UserPrincipalName = viewModel.UserPrincipalName }, new Guid(viewModel.ZtdId));
            // Fix the condition
            if (returnValue)
            {
                await DisplayAlert("User Assigned Status", "User assignement successfull", "OK");
            }
            else
            {
                await DisplayAlert("User Assigned Status", "User assignement unsuccessfull", "OK");
            }
        }

        public async void AddItem_Clicked1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QrScanCodePage());
        }
    }
}