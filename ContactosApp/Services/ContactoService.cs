using SQLite;
using System.Collections.ObjectModel;

namespace ContactosApp.Services
{
    public class ContactoService : IContactoService
    {
        readonly string dbPath;
        SQLiteAsyncConnection? database;

        public ContactoService(string dbPath)
        {
            this.dbPath = dbPath;
        }

        public async Task InitializeAsync()
        {
            if (database != null)
                return;

            database = new SQLiteAsyncConnection(dbPath);
            await database.CreateTableAsync<Contacto>();
        }

        public async Task<ObservableCollection<Contacto>> GetAllAsync()
        {
            await InitializeAsync();
            var items = await database.Table<Contacto>().ToListAsync();
            return new ObservableCollection<Contacto>(items);
        }

        public async Task<Contacto?> GetByIdAsync(int id)
        {
            await InitializeAsync();
            return await database.Table<Contacto>().Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> AddAsync(Contacto contacto)
        {
            await InitializeAsync();
            return await database.InsertAsync(contacto);
        }

        public async Task<int> UpdateAsync(Contacto contacto)
        {
            await InitializeAsync();
            return await database.UpdateAsync(contacto);
        }

        public async Task<int> DeleteAsync(int id)
        {
            await InitializeAsync();
            return await database.DeleteAsync<Contacto>(id);
        }
    }
}
