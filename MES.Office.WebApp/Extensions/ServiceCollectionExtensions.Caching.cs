using MES.Office.Application.Interfaces.Caching;
using MES.Office.Application.Services.Caching;

namespace MES.Office.WebApp.Extensions
{
    /// <summary>
    /// Extension methods for configuring caching services in WebApp
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds caching services to the application
        /// </summary>
        public static IServiceCollection AddCachingServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add in-memory caching (L1 cache)
            services.AddMemoryCache(options =>
            {
                // SizeLimit = number of cache ENTRIES (not records)
                // Each entry can contain thousands of records
                // Rule of thumb: 100 entries per GB of available RAM
                // For a server with 8GB RAM, 5000 allows ~5GB for cache
                options.SizeLimit = null; // No limit - let memory pressure handle it
                options.CompactionPercentage = 0.25; // Compact 25% when memory pressure occurs
                options.ExpirationScanFrequency = TimeSpan.FromMinutes(5); // Scan for expired items every 5 minutes
            });

            // Add distributed caching (L2 cache) - Use in-memory for WebApp (testing/seeding)
            // Redis is not needed for WebApp as it's only used for database seeding/testing
            services.AddDistributedMemoryCache();

            // Register the hybrid cache service
            services.AddSingleton<ICacheService, HybridCacheService>();

            // Register cache metrics service
            services.AddSingleton<ICacheMetricsService, CacheMetricsService>();

            // Register cache invalidation service
            services.AddScoped<ICacheInvalidationService, CacheInvalidationService>();

            // Configure cache settings
            services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));

            return services;
        }
    }

    /// <summary>
    /// Cache configuration settings
    /// </summary>
    public class CacheSettings
    {
        public bool Enabled { get; set; } = true;
        public int DefaultExpirationMinutes { get; set; } = 5;
        public int MaxExpirationMinutes { get; set; } = 60;
        public bool UseRedis { get; set; } = false;
        public string RedisConnectionString { get; set; }
        public string RedisInstanceName { get; set; } = "MES_Office_";
        public int MemoryCacheSizeLimit { get; set; } = 1000;
        public double MemoryCacheCompactionPercentage { get; set; } = 0.25;
    }
}
