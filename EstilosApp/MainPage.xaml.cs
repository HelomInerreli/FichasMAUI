using Microsoft.Maui.Storage;

namespace EstilosApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // set switch state from saved preference or current theme
            bool isDark = Preferences.Default.Get("isDark", Application.Current.UserAppTheme == AppTheme.Dark);
            ThemeSwitch.IsToggled = isDark;
        }

        void OnToggled(object sender, ToggledEventArgs e)
        {
            Application.Current.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;
            Preferences.Default.Set("isDark", e.Value);
        }
    }
}
