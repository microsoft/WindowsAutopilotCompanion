using CompanionApp.Model;
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
            bool returnValue = await viewModel.DataStore.UpdateDeviceAsync(viewModel.Device);
            if (!returnValue)
            {
                await DisplayAlert("Device settings update", "Device settings update failed.", "OK");
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
                viewModel.Device.AddressableUserName = user.User.DisplayName;
            }
        }

        private void RemoveUser_Clicked(object sender, EventArgs e)
        {
            viewModel.Device.UserPrincipalName = String.Empty;
            viewModel.Device.AddressableUserName = String.Empty;
        }
    }
}