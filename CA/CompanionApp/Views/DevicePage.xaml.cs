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
            int i = 0;
            foreach (var item in device.CategoryList)
            {
                if (item.Id == device.ManagedDeviceCategoryId)
                    this.DeviceCategory.SelectedIndex = i;
                i++;
            }
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

            DeviceCategory newCategory = (DeviceCategory)this.DeviceCategory.SelectedItem;
            if (viewModel.Device.ManagedDeviceCategory != newCategory.DisplayName)
            {
                viewModel.Device.ManagedDeviceCategory = newCategory.DisplayName;
                bool returnValue = await viewModel.DataStore.AssignCategory(viewModel.Device);
                if (!returnValue)
                {
                    await DisplayAlert("Category Assigned Status", "Category assignement failed", "OK");
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

        private void DeviceCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeviceCategory newCat = (DeviceCategory)DeviceCategory.SelectedItem;
            viewModel.Device.ManagedDeviceCategoryId = newCat.Id;
        }
    }
}