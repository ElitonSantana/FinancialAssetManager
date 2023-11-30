using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialAssetManager.Domain.Interfaces.Repository
{
    public interface IMongoSettings
    {
        public string DataBase { get; set; }
        public string ConnectionString { get; set; }
    }
}
