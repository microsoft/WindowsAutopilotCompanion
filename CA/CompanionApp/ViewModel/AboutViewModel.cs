using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CompanionApp.ViewModel
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://aka.ms/WindowsAutopilotDocs")));
        }

        public ICommand OpenWebCommand { get; }
    }
}
