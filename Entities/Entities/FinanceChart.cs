using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FinancialAssetManager.Entities.Entities
{
    public class FinanceChart
    {
        [JsonProperty("chart")]
        public Chart Chart { get; set; }
    }
}
