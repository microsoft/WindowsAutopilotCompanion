using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompanionApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogonPage : ContentPage
	{
		public LogonPage ()
		{
			InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
	}
}