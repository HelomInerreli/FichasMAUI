using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using ContactosApp.Services;

namespace ContactosApp
{
    public partial class MainPage : ContentPage
    {
        ContactosApp.Services.IContactoService? contactoService;
        ObservableCollection<Contacto> contacts = new ObservableCollection<Contacto>();
        List<Contacto> allContacts = new List<Contacto>();

        public MainPage()
        {
            InitializeComponent();

            // get service from DI via MauiApp (available once app built)
            try
            {
                var services = Microsoft.Maui.Controls.Application.Current?.Handler?.MauiContext?.Services
                               ?? MauiApplication.Current?.Services;

                if (services != null)
                {
                    contactoService = services.GetService<ContactosApp.Services.IContactoService>();
                }
            }
            catch
            {
                // ignore during design-time
            }

            contactsCollection.ItemsSource = contacts;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadContactsAsync();
        }

        async Task LoadContactsAsync()
        {
            if (contactoService is null)
                return;

            var all = await contactoService.GetAllAsync();
            allContacts = all.ToList();
            contacts.Clear();
            foreach (var c in allContacts)
                contacts.Add(c);
        }

        async private void OnAddContactClicked(object sender, EventArgs e)
        {
            if (contactoService is null)
            {
                await DisplayAlert("Erro", "Serviço de contactos indisponível.", "OK");
                return;
            }

            var nome = nomeEntry.Text?.Trim();
            var tel = telefoneEntry.Text?.Trim();
            var email = emailEntry.Text?.Trim();

            if (string.IsNullOrWhiteSpace(nome))
            {
                await DisplayAlert("Validação", "Nome é obrigatório.", "OK");
                return;
            }

            var novo = new Contacto { Nome = nome, Telefone = tel ?? string.Empty, Email = email ?? string.Empty };
            await contactoService.AddAsync(novo);

            // reload or add to collections (reload to get Id)
            await LoadContactsAsync();

            // clear inputs
            nomeEntry.Text = string.Empty;
            telefoneEntry.Text = string.Empty;
            emailEntry.Text = string.Empty;
        }

        async private void OnDeleteContactClicked(object sender, EventArgs e)
        {
            if (contactoService is null)
                return;

            var btn = sender as Button;
            if (btn == null)
                return;

            if (btn.CommandParameter is int id)
            {
                var toDelete = allContacts.FirstOrDefault(x => x.Id == id);
                if (toDelete == null)
                    return;

                bool confirm = await DisplayAlert("Eliminar", $"Deseja eliminar '{toDelete.Nome}'?", "Sim", "Não");
                if (!confirm)
                    return;

                await contactoService.DeleteAsync(id);

                // update lists
                allContacts.Remove(toDelete);
                var existing = contacts.FirstOrDefault(x => x.Id == id);
                if (existing != null)
                    contacts.Remove(existing);
            }
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            var text = e.NewTextValue?.Trim();
            ApplyFilter(text);
        }

        void ApplyFilter(string? filter)
        {
            IEnumerable<Contacto> filtered = allContacts;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                filtered = allContacts.Where(c => c.Nome != null && c.Nome.Contains(filter, StringComparison.OrdinalIgnoreCase));
            }

            contacts.Clear();
            foreach (var c in filtered)
                contacts.Add(c);
        }
    }
}
