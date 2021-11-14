using System;

using ClassScheduler.Services;
using ClassScheduler.ViewModels;

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace ClassScheduler.Pages
{
	public partial class ClassesPage : ContentPage
	{
		public ClassesPage()
		{
			InitializeComponent();
			BindingContext = new ClassesViewModel();
		}
	}
}