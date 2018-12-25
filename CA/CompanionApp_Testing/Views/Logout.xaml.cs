
using CompanionApp_Testing.ViewModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompanionApp_Testing.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Logout : ContentPage
    {
        public Logout()
        {
            InitializeComponent();

            LogoutViewModel logoutViewModel = new LogoutViewModel();
            logoutViewModel.DataStore.LogOutUser();
            Navigation.RemovePage(this);
        }
    }
}