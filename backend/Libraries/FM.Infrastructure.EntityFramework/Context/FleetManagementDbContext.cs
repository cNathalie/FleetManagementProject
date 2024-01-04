using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FM.Infrastructure.EntityFramework.Context;

public partial class FleetManagementDbContext : GeneratedDbContext
{
    private readonly IConfiguration _configuration;
    private readonly ILoggerFactory _loggerFactory;
    public FleetManagementDbContext(IConfiguration configuration, ILoggerFactory loggerFactory)
    {
        _configuration = configuration;
        _loggerFactory = loggerFactory;
    }

    public FleetManagementDbContext(DbContextOptions<GeneratedDbContext> options, IConfiguration configuration, ILoggerFactory loggerFactory)
        : base(options)
    {
        _configuration = configuration;
        _loggerFactory = loggerFactory;
    }


    // USING APPSETTING.JSON
    // ---------------------
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var runningMode = _configuration[key: "Mode"];
        if (!string.IsNullOrEmpty(runningMode))
        {
            if (runningMode.ToUpper().Equals("DEVELOPMENT"))
            {
                // When in dev mode, include sensitive app data in logging
                optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging();
            }
            else
            {
                optionsBuilder.UseLoggerFactory(_loggerFactory);
            }

        }

        // When running in a docker container, use the docker connection string
        if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
        {
            optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:Docker"]);
        }
        // When not in a docker container, use the active connection string
        else
        {
            var activeConnectionString = _configuration[key: "ActiveConnectionString"];
            if (!string.IsNullOrEmpty(activeConnectionString))
            {
                optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:" + activeConnectionString]);
            }
        }

    }



    // CASCADING SOFT DELETE
    // ---------------------
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entitiesToDelete = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Deleted && e.Metadata.GetProperties().Any(x => x.Name == "IsDeleted"))
            .ToList();

        foreach (var entityEntry in entitiesToDelete)
        {
            SoftDelete(entityEntry);
            await CascadeSoftDelete(entityEntry);
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    private void SoftDelete(EntityEntry entityEntry)
    {
        entityEntry.State = EntityState.Unchanged;
        entityEntry.CurrentValues["IsDeleted"] = true;

    }

    private async Task CascadeSoftDelete(EntityEntry entityEntry)
    {
        // Get all 'navigations' that are dependent on this entity (= relations between principal/parent and dependent/child)
        foreach (var collection in entityEntry.Collections)
        {
            await collection.LoadAsync(); // For each collection navigation, load the connected entities
            var dependentEntities = collection.CurrentValue; // Get all the entities in an IEnumerable
            if (dependentEntities != null)
            {
                foreach (var dependentEntity in dependentEntities) // For every dependent entity ...
                {
                    var isDeletedProperty = dependentEntity.GetType().GetProperty("IsDeleted");
                    if (isDeletedProperty != null && isDeletedProperty.PropertyType == typeof(bool)) // ... that has an IsDeleted property...
                    {
                        isDeletedProperty.SetValue(dependentEntity, true); //.. the property is set to true = SoftDelete
                    }
                }
            }
        }
    }
}
