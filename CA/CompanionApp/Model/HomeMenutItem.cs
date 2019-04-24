namespace CompanionApp.Model
{
    public enum MenuItemType
    {
        Info,
        DeviceSearch,
        About,
        Logout
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
