using System.Runtime.CompilerServices;

namespace NavegacaoApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        async private void OnGoPage1Clicked(object sender, EventArgs e)
        {
            string name = nameEntry.Text;

            if (string.IsNullOrWhiteSpace(name))
            {
                await DisplayAlert("Erro", "Por favor, insira o seu nome.", "OK");
                return;
            }

            await Navigation.PushAsync(new Pagina1(name));
        }

        async private void OnGoPage2Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pagina2());
        }
    }
}
