namespace CompanionApp.Model
{
    public enum MenuItemType
    {
        Info,
        ScanQR,
        Browse,
        About,
        Logout,
        UserDetailsPage,
        UsersPage
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
