
using Applications.Models;

namespace Applications.Interfaces
{
    public interface IDockingService
    {
        Task<bool> AuthorizeDocking(Docking docking);
        Task<bool> CancelDocking(int idDocking);
    }
}
