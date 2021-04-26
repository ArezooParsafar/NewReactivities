using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
    public static class MappingConfigurations
    {
        public static void AddCustomConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MappingConfigurations).Assembly);
        }
    }
}
