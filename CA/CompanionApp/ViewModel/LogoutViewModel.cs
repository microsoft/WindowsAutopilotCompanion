using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CompanionApp.ViewModel
{
    public class LogoutViewModel : BaseViewModel
    {
        public LogoutViewModel()
        {
            Title = "About";

            LogoutCommand = new Command(async () => await this.DataStore.LogOutUser());
        }

        public ICommand LogoutCommand { get; }
    }
}
