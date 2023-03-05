using Music_Player.ViewModel;

namespace Music_Player.View;

public partial class PlaylistsPage : ContentPage
{
	public PlaylistsPage()
	{
		InitializeComponent();
		BindingContext = new PlaylistsPageViewModel();
	}

    private void PlaylistClicked(object sender, EventArgs e) 
		=> (BindingContext as PlaylistsPageViewModel).ChoosePlaylist((sender as Button).Text);

    private void RemovePlaylistClicked(object sender, EventArgs e)
		=> (BindingContext as PlaylistsPageViewModel).RemovePlaylist((sender as ImageButton).CommandParameter.ToString());

    private void PlaylistInfoClicked(object sender, EventArgs e)
		=> (BindingContext as PlaylistsPageViewModel).OpenPlaylistPage((sender as ImageButton).CommandParameter.ToString());
}