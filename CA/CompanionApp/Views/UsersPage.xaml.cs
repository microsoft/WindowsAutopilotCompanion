using CompanionApp.Model;
using CompanionApp.ViewModel;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompanionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersPage : ContentPage
    {
        UsersViewModel viewModel;
        string ztdid = string.Empty;
        public TaskCompletionSource<bool> Completed
        {
            get; set;
        }

        public UsersPage(string ztdId)
        {
            InitializeComponent();

            BindingContext = viewModel = new UsersViewModel();
            this.ztdid = ztdId;

            Completed = new System.Threading.Tasks.TaskCompletionSource<bool>();
        }

        async void OnItemSelected(object sender, ItemTappedEventArgs args)
        {
            var item = args.Item as User;

            if (item == null)
            {
                this.User = null;
                return;
            }
            this.User = item;
            await Navigation.PopModalAsync();
            this.Completed.SetResult(true);

            //item.Ztdid = this.ztdid;
            //await Navigation.PushAsync(new UserDetailsPage(new UserDetailViewModel(item)));

            //// Manually deselect item.
            //UsersListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Users.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        public User User { get; set; }
    }
}