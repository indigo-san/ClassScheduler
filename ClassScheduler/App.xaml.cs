using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
using Reactive.Bindings;

using System.Threading;

using Application = Microsoft.Maui.Controls.Application;

namespace ClassScheduler
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			if (Device.Idiom == TargetIdiom.Phone)
				Shell.Current.CurrentItem = PhoneTabs;

            UIDispatcherScheduler.Initialize();
        }
    }
}
