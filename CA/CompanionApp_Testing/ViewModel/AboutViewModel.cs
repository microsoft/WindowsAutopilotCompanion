using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CompanionApp_Testing.ViewModel
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
            //LogoutCommand = new Command(async () => await this.DataStore.LogOutUser());
        }

        public ICommand OpenWebCommand { get; }
        public ICommand LogoutCommand { get; }
    }
}
