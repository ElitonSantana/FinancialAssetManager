using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace FinancialAssetManager.Entities.Entities
{
    public class FinanceChartResult
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
        [JsonProperty("timestamp")]
        public List<long> Timestamp { get; set; }
        [JsonProperty("indicators")]
        public Indicators Indicators { get; set; }
    }
}
