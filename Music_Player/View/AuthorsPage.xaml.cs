using Music_Player.ViewModel;

namespace Music_Player.View;

public partial class AuthorsPage : ContentPage
{
	public AuthorsPage()
	{
		InitializeComponent();
		BindingContext = new AuthorsPageViewModel();
	}

    private void AuthorClicked(object sender, EventArgs e) 
		=> (BindingContext as AuthorsPageViewModel).StartAuthorPlaylist((sender as Button).Text);
}