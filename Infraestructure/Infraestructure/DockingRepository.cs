using Domain.Interfaces;
using Domain.Models;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Infraestructure
{
    public class DockingRepository : IDockingRepository
    {
        private readonly ApgManagementDbContext context;
        public DockingRepository(ApgManagementDbContext context)
        {
            this.context = context;
        }

        public async Task SaveAsync<T>(T item) where T : class
        {
            try
            {
                context.Set<T>().Add(item);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Docking?> GetAsync(int id)
        {
            return await context.Dockings.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Docking docking)
        {
            context.Dockings.Update(docking);
            await context.SaveChangesAsync();
        }
    }
}
