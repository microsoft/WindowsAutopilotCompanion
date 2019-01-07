using CompanionApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompanionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Logout : ContentPage
    {
        public Logout()
        {
            InitializeComponent();

            LogoutViewModel logoutViewModel = new LogoutViewModel();
            logoutViewModel.DataStore.LogOutUser();
            App.Current.MainPage = new LogonPage();
        }
        
    }
}