using ZXing.Mobile;

namespace CompanionApp.UWP
{
    public sealed partial class MainPage
    {
        MobileBarcodeScanner scanner;

        public MainPage()
        {
            this.InitializeComponent();

            scanner = new MobileBarcodeScanner(this.Dispatcher);
            scanner.Dispatcher = this.Dispatcher;

            LoadApplication(new CompanionApp.App());
        }
    }
}
