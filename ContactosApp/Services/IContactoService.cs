using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ContactosApp.Services
{
    public interface IContactoService
    {
        Task InitializeAsync();
        Task<ObservableCollection<Contacto>> GetAllAsync();
        Task<Contacto?> GetByIdAsync(int id);
        Task<int> AddAsync(Contacto contacto);
        Task<int> UpdateAsync(Contacto contacto);
        Task<int> DeleteAsync(int id);
    }
}
