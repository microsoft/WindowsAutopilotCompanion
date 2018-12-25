using CompanionApp_Testing.Model;
using CompanionApp_Testing.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompanionApp_Testing.Views
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

            bool returnValue = await viewModel.DataStore.AssignUserAsync(new User() { DisplayName = viewModel.DisplayName, UserPrincipalName = UPN.Text }, Guid.NewGuid());
            if (returnValue)
            {
                await DisplayAlert("USer Assigned Status", "User assignement successfully", "OK");
            }
            else
            {
                await DisplayAlert("USer Assigned Status", "User assignement failed", "OK");
            }
        }
    }
}