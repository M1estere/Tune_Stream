using Music_Player.Model;
using Music_Player.ViewModel;

namespace Music_Player.View;

public partial class PlaylistInfoPage : ContentPage
{
	public PlaylistInfoPage()
	{
		InitializeComponent();
		BindingContext = new PlaylistInfoPageViewModel();
    }

    private void ChooseButtonClicked(object sender, EventArgs e) => (BindingContext as PlaylistInfoPageViewModel).ChooseTrack((sender as Button).Text);

    private void AddToEndClicked(object sender, EventArgs e) => PlaylistInfoPageViewModel.AddToCurrentQueue((sender as Button).Text);

    private void AddNextClicked(object sender, EventArgs e) => PlaylistInfoPageViewModel.AddNextToQueue((sender as Button).Text);
}