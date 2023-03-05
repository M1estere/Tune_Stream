using Music_Player.ViewModel;

namespace Music_Player.View;

public partial class CollectionsPage : ContentPage
{
	public CollectionsPage()
	{
		InitializeComponent();
        BindingContext = new CollectionsPageViewModel();
    }

    private void PlaylistClicked(object sender, EventArgs e) 
        => (BindingContext as CollectionsPageViewModel).ChoosePlaylist((sender as Button).Text);

    private void OpenPlaylistClicked(object sender, EventArgs e) 
        => (BindingContext as CollectionsPageViewModel).OpenPlaylistPage((sender as ImageButton).CommandParameter.ToString());
}