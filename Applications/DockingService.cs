using Applications.Enums;
using Applications.Interfaces;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using DomainModels = Domain.Models;

namespace Applications
{
    public class DockingService : IDockingService
    {
        private readonly IDockingRepository dockingRepository;
        private readonly ILogger<DockingService> logger;

        public DockingService(IDockingRepository dockingRepository, ILogger<DockingService> logger)
        {
            this.dockingRepository = dockingRepository;
            this.logger = logger;
        }

        public async Task<bool> AuthorizeDocking(Models.Docking docking)
        {
            try
            {
                var domainBuque = new DomainModels.Buque()
                {
                    Empresa = docking.Buque.Empresa,
                    Nombre = docking.Buque.Nombre
                };
                var domainDocking = new DomainModels.Docking()
                {
                    Status = docking.Status.ToString(),
                    Buque = domainBuque,
                    Muelle = docking.Muelle,
                    FechaHora = docking.FechaHora
                };

                await dockingRepository.SaveAsync(domainDocking);
                docking.Id = domainDocking.Id;
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Ocurrió un error al ejecutar AuthorizeDocking en {DateTime.UtcNow}", DateTime.UtcNow);
                return false;
            }
            
        }

        public async Task<bool> CancelDocking(int idDocking)
        {
            try
            {
                var domainDocking = await dockingRepository.GetAsync(idDocking);
                if (domainDocking is null)
                    return false;

                domainDocking.Status = DockingStatus.CANCELLED.ToString();
                await dockingRepository.UpdateAsync(domainDocking);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Ocurrió un error al ejecutar CancelDocking en {DateTime.UtcNow}", DateTime.UtcNow);
                return false;
            }
            
        }

      
    }
}
