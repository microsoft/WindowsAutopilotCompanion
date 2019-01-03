using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
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
                    //LabelText1.Text = resultOfQrCode.Text;
                    // parse json here
                    ztdid = GetDeviceId(resultOfQrCode.Text);
                    await DisplayAlert("Scanned Barcode", resultOfQrCode.Text, "OK");

                    if (!string.IsNullOrEmpty(ztdid))
                    {
                        await DisplayAlert("Device Unique Id", ztdid, "OK");
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

            // remove the "Data to encode" as the string that QR code spits is as below
            //"Data to encode{ \"BlockSequence\": { \"Id\": \"fff91cde-79a4-4653-9d04-8714577d9603\",\"ZtdId\": \"e13be37b-1913-4983-9bc4-0d6ccfe0153b\",\"PKID\": \"\",\"SerialNumber\": \"\",\"HardwareId\": \"\",\"SequenceBlockNumber\": 1,\"TotalBlocks\": 1 }}"
            string removeEncoding = jsonBlob.Substring(14);

            try
            {
                // Make it valid json as it carries escape characters
                // "{ \"BlockSequence\": { \"Id\": \"fff91cde-79a4-4653-9d04-8714577d9603\",\"ZtdId\": \"e13be37b-1913-4983-9bc4-0d6ccfe0153b\",\"PKID\": \"\",\"SerialNumber\": \"\",\"HardwareId\": \"\",\"SequenceBlockNumber\": 1,\"TotalBlocks\": 1 }}"
                JToken validJson = JToken.Parse(removeEncoding);

                // this return the array of values inside BlockSequence
                JObject innerJsonBlob = validJson["BlockSequence"].Value<JObject>();

                // this identify is there is a ZtdId
                deviceUniqueId = (string)innerJsonBlob.Properties().Where(x => x.Name.Contains("ZtdId")).Values().FirstOrDefault();
            }
            catch (Exception e)
            {
                deviceUniqueId = string.Empty;
            }
            return deviceUniqueId;
        }
    }
}