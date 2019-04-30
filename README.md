# Windows Autopilot Companion

This repository contains a sample app that can be used to modify the settings of a device as part of a Windows Autopilot white glove process (introduced in Windows 10 1903).  This demonstrates how to make just-in-time configuration adjustments, before pre-provisioning the device with needed apps and settings.

## Capabilities

The app supports simple configuration adjustments:

- Add, remove, or change the user assigned to the Windows Autopilot device.
- Configure the device category assigned to the device.  (Note that this category will be set on the Azure AD device object when the device enrolls in Intune.)

Expected future additions (once supported by Windows Autopilot and Intune):

- Editing of the group tag (order ID value) and computer name.

This application leverages Xamarin in order to create a cross-platform app.  This will run on Windows 10, Android, and iOS devices.  For pre-built binaries, see the "Drops" folder.  (These are provided as-is.)

## Using

To use this sample app, you first need to authenticate to Microsoft Intune.  This can be done in one of two ways:

- If you are making changes to the Intune tenant associated with your Azure AD account, you can just click the "Logon" button to be prompted for your credentials.
- If you are making changes to another tenant that you have been granted access to, either via the guest access process (described here) or via Partner Center, specify the tenant ID (e.g. contoso.onmicrosoft.com) before clicking "Logon".

Once connected, choose "Device Search" from the menu.  Search for devices using the serial number of the device (all devices with serial numbers starting with the value specified will be returned).  Or alternatively, click the "Scan QR Code" button to retrieve the device associated with that QR code (retrieved using the ZtdId value embedded in the QR code).

# Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.microsoft.com.

When you submit a pull request, a CLA-bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., label, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
