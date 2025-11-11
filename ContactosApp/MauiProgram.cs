using Microsoft.Extensions.Logging;
using ContactosApp.Services;

namespace ContactosApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register ContactoService with a path in AppData
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "contactos.db3");
            builder.Services.AddSingleton<IContactoService>(sp => new ContactoService(dbPath));

#if DEBUG
			builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
