using Music_Player.View;

namespace Music_Player;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(AllSongsPage), typeof(AllSongsPage));
        Routing.RegisterRoute(nameof(PlaylistsPage), typeof(PlaylistsPage));
        Routing.RegisterRoute(nameof(PlaylistInfoPage), typeof(PlaylistInfoPage));
        Routing.RegisterRoute(nameof(AuthorsPage), typeof(AuthorsPage));
    }
}
