using FinancialAssetManager.Domain.Interfaces.Repository;
using FinancialAssetManager.Domain.Interfaces.Services;
using FinancialAssetManager.Domain.Services;
using FinancialAssetManager.Infra.MongoConfiguration;
using FinancialAssetManager.Infra.Repository;

namespace FinancialAssetManager
{
    public static class ServiceCollectionExtension
    {
       
        /// <summary>
        /// Application Services 
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMongoSettings, MongoSettings>();
            services.AddSingleton<IAssetManagerService, AssetManagerService>();
        }

        /// <summary>
        /// Repositorys Application
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureRepositorys(IServiceCollection services)
        {
            services.AddSingleton<IAssetManagerRepository, AssetManagerRepository>();
        }
    }
}
