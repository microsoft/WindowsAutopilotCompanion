using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace CompanionApp_Testing.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QrScanCodePage : ContentPage
    {
        public QrScanCodePage()
        {
            InitializeComponent();
            ListButton.IsVisible = false;
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var options = new MobileBarcodeScanningOptions
            {
                PossibleFormats = new List<BarcodeFormat>
                {
                    BarcodeFormat.QR_CODE
                }
            };

            var scanQRCode = new ZXingScannerPage(options);
            scanQRCode.DefaultOverlayTopText = "Align the code within the grame";
            scanQRCode.DefaultOverlayBottomText = string.Empty;
            scanQRCode.DefaultOverlayShowFlashButton = true;
            await Navigation.PushAsync(scanQRCode);
            scanQRCode.OnScanResult += (resultOfQrCode) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    LabelText1.Text = resultOfQrCode.Text;
                    await DisplayAlert("Scanned Barcode", LabelText1.Text, "OK");
                    ListButton.IsVisible = true;
                });
            };
        }

        private async void Button1_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(LabelText1.Text))
            {
                await Navigation.PushModalAsync(new NavigationPage(new UsersPage()));
            }
        }
    }
}