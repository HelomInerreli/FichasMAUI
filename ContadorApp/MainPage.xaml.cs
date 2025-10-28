namespace ContadorApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            CounterLabel.Text = $"Contador: {count}";
        }

        private void PlusClicked(object? sender, EventArgs e)
        {
            count++;
            CounterLabel.Text = $"Contador: {count}";
        }

        private void MinusClicked(object? sender, EventArgs e)
        {
            if (count > 0)
                count--;
            else
                count = 0;

            CounterLabel.Text = $"Contador: {count}";
        }

        private void ResetClicked(object? sender, EventArgs e)
        {
            count = 0;
            CounterLabel.Text = $"Contador: {count}";
        }
    }
}
