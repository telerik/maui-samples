# Telerik Crypto Tracker

Telerik Crypto Tracker demo is a cross-platform .NET MAUI application targeting Android, iOS, macOS, and WinUI. It is designed to demonstrates a real-life crypto application that shows changes in cryptocurrency prices. 

It uses the [Telerik UI for .NET MAUI controls](https://www.telerik.com/maui-ui) to provide a beautiful design and high-performant UI. 

<p align="center"> <img src="Telerik-UI-For-MAUI-CryptoTracker-Image.png"/></p>

## How to build the solution

1. Install Telerik UI for .NET MAUI - you can download it from [here](https://www.telerik.com/try/ui-for-maui). 
1. Copy the path to "Packages" folder from the installation folder.
    - For Windows - **C:\Program Files (x86)\Progress\Telerik UI for .NET MAUI 0.4.0\Packages**
    - For Mac - **/Users/<Your User Name>/Documents/Progress/Telerik_UI_for_NET_MAUI_0.4.0/Packages**
1. Add this path to the [NuGet.Config](../NuGet.Config) file. It should look like this:

     `<add key="PackageSource" value="C:\Program Files (x86)\Progress\Telerik UI for .NET MAUI 0.4.0\Packages"/>`
1. Build the app like any other .NET MAUI solution. You can use [this](https://docs.telerik.com/devtools/maui/demos-and-sample-apps/crypto-app) help article for guidance.



