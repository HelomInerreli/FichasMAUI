namespace NavegacaoApp;

public partial class Pagina2 : ContentPage
{
	public Pagina2()
	{
		InitializeComponent();
	}

    async private void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}