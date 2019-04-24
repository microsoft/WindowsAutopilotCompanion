using CompanionApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompanionApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DevicePage : ContentPage
	{
        DeviceViewModel viewModel;

        public DevicePage(Model.Device device)
		{
			InitializeComponent();

            viewModel = new DeviceViewModel();
            viewModel.Device = device;
            BindingContext = this.viewModel;
        }

        private async void SaveChanges_Clicked(object sender, EventArgs e)
        {
            if (viewModel.Device.UserPrincipalName == String.Empty)
            {
                bool returnValue = await viewModel.DataStore.UnAssignUserAsync(new Guid(viewModel.Device.ZtdId));
                if (!returnValue)
                {
                    await DisplayAlert("User Unassigned Status", "User unassignement failed", "OK");
                }
            }
            else
            {
                bool returnValue = await viewModel.DataStore.AssignUserAsync(new Model.User() { DisplayName = viewModel.Device.AddressibleUserName, UserPrincipalName = viewModel.Device.UserPrincipalName }, new Guid(viewModel.Device.ZtdId));
                if (!returnValue)
                {
                    await DisplayAlert("User Assigned Status", "User assignement failed", "OK");
                }
            }

            await Navigation.PopAsync();
        }

        private async void ChooseUser_Clicked(object sender, EventArgs e)
        {
            UsersPage user = new UsersPage(viewModel.Device.ZtdId);
            await Navigation.PushModalAsync(user);
            await user.Completed.Task;
            if (user.User != null)
            {
                viewModel.Device.UserPrincipalName = user.User.UserPrincipalName;
                viewModel.Device.AddressibleUserName = user.User.DisplayName;
            }
        }

        private void RemoveUser_Clicked(object sender, EventArgs e)
        {
            viewModel.Device.UserPrincipalName = String.Empty;
            viewModel.Device.AddressibleUserName = String.Empty;
        }
    }
}