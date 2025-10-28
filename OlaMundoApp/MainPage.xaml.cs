namespace OlaMundoApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnClickedBtn(object sender, EventArgs e)
        {
            var nome = NameEntry.Text?.Trim();
            var dataHoraAtual = DateTime.Now.ToString("F");
            if (string.IsNullOrEmpty(nome))
                GreetingLabel.Text = "Olá! Bem-vindo à App MAUI!";
            else
                GreetingLabel.Text = $"Olá, {nome}! Bem-vindo à App MAUI!" ;
                HoraLabel.Text = $"{dataHoraAtual}";
        }
    }
}
