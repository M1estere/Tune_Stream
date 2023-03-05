using Music_Player.ViewModel;

namespace Music_Player.View;

public partial class PersonalPage : ContentPage
{
	public PersonalPage()
	{
		InitializeComponent();
        BindingContext = new PersonalPageViewModel();
    }

    private void ChooseButtonClicked(object sender, EventArgs e) => (BindingContext as PersonalPageViewModel).ChooseTrack((sender as Button).Text);

    private void RemoveButtonClicked(object sender, EventArgs e) 
        => (BindingContext as PersonalPageViewModel).RemoveTrackFromFavourites((sender as ImageButton).CommandParameter.ToString());
}