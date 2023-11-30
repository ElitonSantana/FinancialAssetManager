using FinancialAssetManager.Domain.Interfaces.Repository;
using FinancialAssetManager.Domain.Options;
using FinancialAssetManager.Entities.Entities;
using FinancialAssetManager.Infra.MongoConfiguration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialAssetManager.Infra.Repository
{
    public class AssetManagerRepository : IAssetManagerRepository
    {
        private readonly IMongoCollection<Chart> _mongoCollection;
        private readonly MongoSettings _mongoSettings;
        public AssetManagerRepository(IOptions<MongoSettings> mongoSettings)
        {
            _mongoSettings = mongoSettings.Value;

            string connectionUri = _mongoSettings.ConnectionString;
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            var client = new MongoClient(settings);

            var mongoDataBase = client.GetDatabase(_mongoSettings.DataBase);
            _mongoCollection = mongoDataBase.GetCollection<Chart>("FinancialChart");
        }

        public async Task<List<Chart>> GetAsync()
        {
            return await _mongoCollection.Find(x => true).ToListAsync();
        }

        public async Task<Chart> GetAsyncById(string _id)
        {
            var result = await _mongoCollection.Find(x => true).ToListAsync();

            return result.Where(x=> x._id == _id).FirstOrDefault();
        }

        public async Task<Chart> GetAsync(string Symbol)
        {
            var result = await _mongoCollection.Find(x => true).ToListAsync();

            foreach (var financeChart in result)
            {
                foreach (var financeChartResult in financeChart.Result)
                {
                    if (financeChartResult.Meta.Symbol.ToUpper() == Symbol.ToUpper())
                        return financeChart;
                }
            }

            return null;
        }

        public async Task CreateAsync(Chart Chart)
        {
            Chart.CreationDate = DateTime.Now;
            await _mongoCollection.InsertOneAsync(Chart);
        }

        public async Task UpdateAsync(string _id, Chart Chart)
        {
            Chart._id = _id;
            Chart.ModifiedDate = DateTime.Now;
            await _mongoCollection.ReplaceOneAsync(x => x._id == _id, Chart);
        }

        public async Task DeleteAsync(string _id)
        {
            await _mongoCollection.DeleteOneAsync(x => x._id == _id);
        }
    }
}
