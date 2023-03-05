using Music_Player.View;
using Music_Player.ViewModel;
using Plugin.Maui.Audio;

namespace Music_Player;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

				fonts.AddFont("Roboto-Black.ttf", "RobotoBlack");
				fonts.AddFont("Roboto-BlackItalic.ttf", "RobotoBlackItalic");

				fonts.AddFont("Roboto-Bold.ttf", "RobotoBold");
				fonts.AddFont("Roboto-BoldItalic.ttf", "RobotoBoldItalic");

				fonts.AddFont("Roboto-Italic.ttf", "RobotoItalic");
				fonts.AddFont("Roboto-Light.ttf", "RobotoLight");
				fonts.AddFont("Roboto-LightItalic.ttf", "RobotoLightItalic");

				fonts.AddFont("Roboto-Medium.ttf", "RobotoMedium");
				fonts.AddFont("Roboto-MediumItalic.ttf", "RobotoMediumItalic");

				fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular");

				fonts.AddFont("Roboto-Thin.ttf", "RobotoThin");
				fonts.AddFont("Roboto-ThinItalic.ttf", "RobotoThinItalic");
			});
		
		builder.Services.AddSingleton(AudioManager.Current);

        builder.Services.AddTransient<QueuePage>();
        builder.Services.AddTransient<CollectionsPage>();
        builder.Services.AddTransient<PersonalPage>();

        builder.Services.AddTransient<AllSongsPage>();
        builder.Services.AddSingleton<AllSongsPageViewModel>();

        builder.Services.AddTransient<PlaylistsPage>();
        builder.Services.AddSingleton<PlaylistsPageViewModel>();

        builder.Services.AddTransient<PlaylistInfoPage>();
        builder.Services.AddSingleton<PlaylistInfoPageViewModel>();

        builder.Services.AddTransient<AuthorsPage>();
        builder.Services.AddSingleton<AuthorsPageViewModel>();

        return builder.Build();
	}
}
