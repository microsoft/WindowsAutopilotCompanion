using CompanionApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompanionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Logout : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        public Logout()
        {
            InitializeComponent();

            LogoutViewModel logoutViewModel = new LogoutViewModel();
            logoutViewModel.DataStore.LogOutUser();
            Navigation.RemovePage(this);
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopToRootAsync();
            await Navigation.PushModalAsync(new MainPage());
        }
        
    }
}