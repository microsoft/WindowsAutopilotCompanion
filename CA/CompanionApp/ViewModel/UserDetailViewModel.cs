using CompanionApp.Model;

namespace CompanionApp.ViewModel
{
    public class UserDetailViewModel : BaseViewModel
    {
        public User User { get; set; }
        public string DisplayName { get; set; }
        public string UserPrincipalName { get; set; }
        public string ZtdId { get; set; }
        public UserDetailViewModel(User user = null)
        {
            Title = user?.DisplayName;
            User = user;
            UserPrincipalName = user.UserPrincipalName;
            DisplayName = user.DisplayName;
            ZtdId = user.Ztdid;
        }

    }
}
