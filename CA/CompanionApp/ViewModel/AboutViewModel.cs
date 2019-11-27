using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CompanionApp.ViewModel
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Browser.OpenAsync("https://aka.ms/WindowsAutopilotDocs"));
        }

        public ICommand OpenWebCommand { get; }
    }
}
