using System.Collections.ObjectModel;

namespace ListaComprasApp
{
    public partial class MainPage : ContentPage
    {
        // Boa prática: inicializar a ObservableCollection e atribuir ItemsSource no construtor
        ObservableCollection<string> produtos = new();

        // Para detecção de toque longo
        System.Threading.CancellationTokenSource? longPressCts;
        const int LongPressThresholdMs = 600;

        // Mantém referência ao botão atualmente pressionado para feedback visual
        Button? currentPressedButton;

        public MainPage()
        {
            InitializeComponent();
            itemsListView.ItemsSource = produtos;
        }

        async private void OnAddClicked(object sender, EventArgs e)
        {
            string item = itemEntry.Text;

            if (!string.IsNullOrWhiteSpace(item))
            {
                produtos.Add(item);
                itemEntry.Text = string.Empty;
                //await DisplayAlert("Item Adicionado", $"Item '{item}' adicionado. Total items: {produtos.Count}", "OK");
            }
            else
            {
                await DisplayAlert("Erro", "Insira um item válido.", "OK");
            }
        }


        // Pressed when the user starts pressing the item
        private void OnItemPressed(object sender, EventArgs e)
        {
            // Cancel any previous (just in case)
            try
            {
                longPressCts?.Cancel();
                longPressCts?.Dispose();
                longPressCts = null;
            }
            catch { }

            var btn = sender as Button;
            if (btn == null)
                return;

            currentPressedButton = btn;
            // visual feedback
            btn.Opacity = 0.6;

            longPressCts = new System.Threading.CancellationTokenSource();
            var cts = longPressCts;

            // Get item reliably from CommandParameter (set in XAML)
            var item = btn.CommandParameter as string ?? btn.BindingContext as string;

            _ = Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(LongPressThresholdMs, cts.Token);
                    if (!cts.Token.IsCancellationRequested && item != null)
                    {
                        // Executes on the UI thread
                        await MainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            // Reset visual feedback before showing dialog
                            try { if (btn != null) btn.Opacity = 1.0; } catch { }

                            bool confirm = await DisplayAlert("Remover Item", $"Deseja remover o item '{item}'?", "Sim", "Não");
                            if (confirm)
                            {
                                produtos.Remove(item);
                                //await DisplayAlert("Item Removido", $"Item '{item}' removido. Total items: {produtos.Count}", "OK");
                            }
                        });
                    }
                }
                catch (TaskCanceledException)
                {
                    // ignore
                }
                finally
                {
                    // Ensure visual feedback reset
                    try { await MainThread.InvokeOnMainThreadAsync(() => { if (btn != null) btn.Opacity = 1.0; }); } catch { }
                }
            });
        }

        // Released when the user releases the item
        private void OnItemReleased(object sender, EventArgs e)
        {
            // Cancels the CTS to prevent long-press execution
            try
            {
                longPressCts?.Cancel();
                longPressCts?.Dispose();
                longPressCts = null;
            }
            catch { }

            // Reset visual feedback
            try
            {
                var btn = sender as Button ?? currentPressedButton;
                if (btn != null)
                    btn.Opacity = 1.0;
            }
            catch { }

            currentPressedButton = null;
        }
    }
}
