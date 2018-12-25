namespace CompanionApp_Testing.Model
{
    public enum MenuItemType
    {
        ScanQR,
        Browse,
        About,
        Logout
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
