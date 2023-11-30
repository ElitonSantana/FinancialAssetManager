using FinancialAssetManager.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialAssetManager.Infra.MongoConfiguration
{
    public class MongoSettings : IMongoSettings
    {
        public string DataBase { get; set; }
        public string ConnectionString { get; set; }
    }
}
