using Domain.Models;

namespace Domain.Interfaces
{
    public interface IDockingRepository
    {
        Task SaveAsync<T>(T item) where T : class;

        Task<Docking?> GetAsync(int id);

        Task UpdateAsync(Docking docking);
    }
}
