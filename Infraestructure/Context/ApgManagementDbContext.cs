using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Context
{
    public class ApgManagementDbContext: DbContext
    {
        public ApgManagementDbContext(DbContextOptions<ApgManagementDbContext> options) : base(options)
        {
        }

        public DbSet<Docking> Dockings { get; set; }
        public DbSet<Buque> Buques { get; set; }
    }
}
