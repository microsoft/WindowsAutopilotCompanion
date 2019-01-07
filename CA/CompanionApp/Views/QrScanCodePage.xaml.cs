using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Common;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace CompanionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QrScanCodePage : ContentPage
    {
        string ztdid = string.Empty;
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

            var scanQRCode = new ZXingScannerPage(options)
            {
                DefaultOverlayTopText = "Align the code within the frame",
                DefaultOverlayBottomText = string.Empty,
                DefaultOverlayShowFlashButton = true
            };
            await Navigation.PushAsync(scanQRCode);
            scanQRCode.OnScanResult += (resultOfQrCode) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    LabelText1.Text = resultOfQrCode.Text;
                    // parse json here
                    ztdid = GetDeviceId(resultOfQrCode.Text);

                    if (!string.IsNullOrEmpty(ztdid))
                    {
                        ListButton.IsVisible = true;
                    }
                    else
                    {
                        await DisplayAlert("Device Unique Id", "No Unique ID Found. Please scan the valid QR Code", "OK");
                    }
                });
            };
        }


        private async void Button1_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new UsersPage(ztdid)));
        }

        private string GetDeviceId(string jsonBlob)
        {
            string deviceUniqueId = string.Empty;

            try
            {
                // Make it valid json as it carries escape characters
                // "{ \"BlockSequence\": { \"Id\": \"fff91cde-79a4-4653-9d04-8714577d9603\",\"ZtdId\": \"e13be37b-1913-4983-9bc4-0d6ccfe0153b\",\"PKID\": \"\",\"SerialNumber\": \"\",\"HardwareId\": \"\",\"SequenceBlockNumber\": 1,\"TotalBlocks\": 1 }}"
                JToken validJson = JToken.Parse(jsonBlob);

                // this identify is there is a ZtdId
                deviceUniqueId = (string)validJson["ZtdId"];
            }
            catch (Exception)
            {
                deviceUniqueId = string.Empty;
            }
            return deviceUniqueId;
        }
    }
}