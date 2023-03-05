using Music_Player.ViewModel;

namespace Music_Player.View;

public partial class AllSongsPage : ContentPage
{
	public AllSongsPage()
	{
		InitializeComponent();
		BindingContext = new AllSongsPageViewModel();
	}

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e) => (BindingContext as AllSongsPageViewModel).Search(searchBar.Text);

    private void ChooseButtonClicked(object sender, EventArgs e) => AllSongsPageViewModel.ChooseTrack((sender as Button).Text);

    private void AddNextClicked(object sender, EventArgs e) => AllSongsPageViewModel.AddNextToQueue((sender as Button).Text);
    private void AddToEndClicked(object sender, EventArgs e) => AllSongsPageViewModel.AddToCurrentQueue((sender as Button).Text);

    private void AddToFavsClicked(object sender, EventArgs e) 
        => (BindingContext as AllSongsPageViewModel).AddToFavourites( (sender as ImageButton).CommandParameter.ToString() );
}