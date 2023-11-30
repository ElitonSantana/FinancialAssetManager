using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialAssetManager.Entities.Entities
{
    public class CurrentTradingPeriod
    {
        [JsonProperty("pre")]
        public CurrentTradingPeriodItem Pre { get; set; }

        [JsonProperty("regular")]
        public CurrentTradingPeriodItem Regular { get; set; }

        [JsonProperty("post")]
        public CurrentTradingPeriodItem Post { get; set; }
    }
}
