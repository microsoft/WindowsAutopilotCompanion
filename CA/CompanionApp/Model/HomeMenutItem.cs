namespace CompanionApp.Model
{
    public enum MenuItemType
    {
        Info,
        DeviceSearch,
        Sync,
        About,
        Logout
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
