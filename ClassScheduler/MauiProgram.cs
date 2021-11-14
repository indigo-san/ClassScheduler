using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ClassScheduler.Services;

namespace ClassScheduler
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				});

			builder.Services
				.AddSingleton<IClassSchedulerService, MemoryClassSchedulerService>()
				.AddSingleton<ISubjectDataStore, MemorySubjectDataStore>()
				.AddSingleton<IReportDataStore, MemoryReportDataStore>()
				.AddSingleton<IClassDataStore, MemoryClassDataStore>();

			return builder.Build();
		}
	}
}