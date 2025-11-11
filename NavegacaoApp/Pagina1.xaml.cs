namespace NavegacaoApp;

public partial class Pagina1 : ContentPage
{
	public Pagina1(string name)
	{

		InitializeComponent();
        welcomeLabel.Text = $"Olá {name}, Bem-vindo a página 1";
    }

    async private void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}