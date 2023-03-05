using Music_Player.ViewModel;
using Plugin.Maui.Audio;

namespace Music_Player.View;

public partial class QueuePage : ContentPage
{
	public QueuePage(IAudioManager audioManager)
	{
		InitializeComponent();
		BindingContext = new QueuePageViewModel(audioManager);
    }

    private void DeleteButtonClicked(object sender, EventArgs e)
        => (BindingContext as QueuePageViewModel).RemoveTrack((sender as ImageButton).CommandParameter.ToString());

    private void ToFavouriteClicked(object sender, EventArgs e)
        => (BindingContext as QueuePageViewModel).AddToFavourites((sender as ImageButton).CommandParameter.ToString());

    private void ChooseButtonClicked(object sender, EventArgs e) => (BindingContext as QueuePageViewModel).ChooseTrack((sender as Button).Text);

    private void SliderCompleted(object sender, EventArgs e) => QueuePageViewModel.SetTimeSpan((sender as Slider).Value * QueuePageViewModel.GetDuration());

    private void ReorderCompleted(object sender, EventArgs e) => (BindingContext as QueuePageViewModel).UpdateQueue();
}